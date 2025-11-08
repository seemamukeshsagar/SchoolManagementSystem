using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Linq;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortalApp.Controllers
{
	[Route("Account")]
	public class AccountController : Controller
	{
		private readonly ILoginService _loginService;
		private readonly ILogger<AccountController> _logger;

		public AccountController(ILoginService loginService, ILogger<AccountController> logger)
		{
			_loginService = loginService;
			_logger = logger;
		}

		[HttpPost]
		[Route("Logout")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			try
			{
				await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
				HttpContext.Session.Clear();
			}
			catch (System.Exception ex)
			{
				_logger.LogError(ex, "Error during logout");
			}
			return RedirectToAction("Login");
		}

		[HttpGet]
		[Route("Login")]
		public IActionResult Login()
		{
			_logger.LogInformation("GET Login method called");
			return View(new LoginViewModel());
		}

		[HttpPost]
		[Route("Login")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
		{
			if (model == null)
			{
				_logger.LogWarning("Login called with null model");
				return View(new LoginViewModel());
			}

			_logger.LogInformation("POST Login method called. UserName: {UserName}", model?.UserName);
			
			try
			{
				if (!ModelState.IsValid)
				{
					_logger.LogWarning("ModelState is invalid");
					return View(model);
				}

				if (model == null)
				{
					_logger.LogWarning("Login attempt with null model");
					ModelState.AddModelError(string.Empty, "Invalid login attempt.");
					return View(new LoginViewModel());
				}

				_logger.LogInformation("Authenticating user: {UserName}", model.UserName ?? string.Empty);
				var userDetails = await _loginService.AuthenticateUserAsync(
					model.UserName ?? string.Empty, 
					model.Password ?? string.Empty
				);

				if (userDetails != null)
				{
					_logger.LogInformation("Authentication successful for user: {UserName}. Privileges: {Privileges}", 
						userDetails.UserName ?? string.Empty, string.Join(", ", userDetails.Privileges ?? Enumerable.Empty<string>()));
					
					// Store user details in session (you may want to use proper authentication/session management)
					HttpContext.Session.SetString("UserId", userDetails.Id.ToString());
					HttpContext.Session.SetString("UserName", userDetails.UserName ?? string.Empty);
					HttpContext.Session.SetString("FullName", userDetails.FullName ?? string.Empty);
					HttpContext.Session.SetString("Privileges", string.Join(",", userDetails.Privileges ?? Enumerable.Empty<string>()));
					HttpContext.Session.SetString("SchoolId", userDetails.SchoolId?.ToString() ?? string.Empty);
					HttpContext.Session.SetString("CompanyId", userDetails.CompanyId?.ToString() ?? string.Empty);

					// Sign-in with cookie authentication so User.Identity.IsAuthenticated is true
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.NameIdentifier, userDetails.Id.ToString()),
						new Claim(ClaimTypes.Name, userDetails.FullName ?? userDetails.UserName ?? string.Empty),
						new Claim("UserName", userDetails.UserName ?? string.Empty)
					};
					// Add role/privilege claims if needed
					foreach (var p in userDetails.Privileges ?? Enumerable.Empty<string>())
					{
						claims.Add(new Claim(ClaimTypes.Role, p));
					}

					var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
					var principal = new ClaimsPrincipal(identity);
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
					{
						IsPersistent = true,
						AllowRefresh = true
					});
					
					// Redirect to home or returnUrl if provided
					if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
					{
						return Redirect(returnUrl);
					}
					return RedirectToAction("Index", "Home");
				}

				_logger.LogWarning("Authentication failed for user: {UserName}", model.UserName ?? string.Empty);
				ModelState.AddModelError(string.Empty, "Invalid username or password.");
				return View(model);
			}
			catch (System.Exception ex)
			{
				_logger.LogError(ex, "Error in Login POST method");
				ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
				return View(model);
			}
		}

		[HttpGet]
		[Route("ChangePassword")]
		public IActionResult ChangePassword()
		{
			_logger.LogInformation("GET ChangePassword method called");
			return View(new ChangePasswordViewModel());
		}

		[HttpPost]
		[Route("ChangePassword")]
		[ValidateAntiForgeryToken]
		public IActionResult ChangePassword(ChangePasswordViewModel model)
		{
			_logger.LogInformation("POST ChangePassword method called");
			try
			{
				if (!ModelState.IsValid)
				{
					return View(model);
				}

				var userName = HttpContext.Session.GetString("UserName");
				if (string.IsNullOrWhiteSpace(userName))
				{
					ModelState.AddModelError(string.Empty, "Please login to change password.");
					return RedirectToAction("Login");
				}

				var result = _loginService.ChangePassword(userName, model.OldPassword, model.NewPassword);
				if (string.Equals(result, "Password changed successfully", StringComparison.OrdinalIgnoreCase))
				{
					TempData["SuccessMessage"] = result;
					return RedirectToAction("Index", "Home");
				}

				ModelState.AddModelError(string.Empty, result);
				return View(model);
			}
			catch (System.Exception ex)
			{
				_logger.LogError(ex, "Error in ChangePassword POST method");
				ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
				return View(model);
			}
		}
	}
}