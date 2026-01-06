using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;
using SchoolPortal.Services;

namespace SchoolPortalApp.Controllers
{
	[Route("Authentication")]
	public class AuthenticationController : BaseController
	{
		private readonly ILoginService _loginService;
		private new readonly ILogger<AuthenticationController> _logger;
		private readonly IAuditLogger _auditLogger;

		public AuthenticationController(ILoginService loginService, ILogger<AuthenticationController> logger, IAuditLogger auditLogger)
		{
			_loginService = loginService;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_auditLogger = auditLogger ?? throw new ArgumentNullException(nameof(auditLogger));
		}

		[HttpPost]
		[Route("Logout")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.Session.Clear();
                // Audit log the logout
                if (!string.IsNullOrEmpty(userId))
                {
                    await _auditLogger.LogAsync(
                        "UserLogout",
                        "User logged out successfully",
                        userId,
                        ipAddress
                    );
                }
                _logger.LogInformation("User {UserId} logged out from {IP}", userId, ipAddress);
			}
			catch (System.Exception ex)
			{
				_logger.LogError(ex, "Error during logout");
                // Still log the error to audit log
                await _auditLogger.LogAsync(
                    "LogoutError",
                    $"Error during logout: {ex.Message}",
                    User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Unknown",
                    HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown"
                );
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
			
			var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

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
					
					// Store user details in session
					HttpContext.Session.SetString("UserId", userDetails.Id.ToString());
					HttpContext.Session.SetString("UserName", userDetails.UserName ?? string.Empty);
					HttpContext.Session.SetString("FullName", userDetails.FullName ?? string.Empty);
					HttpContext.Session.SetString("Privileges", string.Join(",", userDetails.Privileges ?? Enumerable.Empty<string>()));
					HttpContext.Session.SetString("SchoolId", userDetails.SchoolId?.ToString() ?? string.Empty);
					HttpContext.Session.SetString("CompanyId", userDetails.CompanyId?.ToString() ?? string.Empty);
					HttpContext.Session.SetString("RoleName", userDetails.RoleName?.ToString() ?? string.Empty);

					// Create claims identity and sign in
					var identity = await CreateClaimsIdentity(userDetails);
					var principal = new ClaimsPrincipal(identity);
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
					{
						IsPersistent = true,
						AllowRefresh = true
					});
					
					// Audit log successful login
                    await _auditLogger.LogAsync(
                        "UserLogin",
                        "User logged in successfully",
                        userDetails.Id.ToString(),
                        ipAddress
                    );
                    _logger.LogInformation("User {UserId} logged in from {IP}", userDetails.Id, ipAddress);

					// Redirect to home or returnUrl if provided
					if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
					{
						return Redirect(returnUrl);
					}
					return RedirectToAction("Index", "Home");
				}

				// Audit log failed login attempt
                await _auditLogger.LogAsync(
                    "LoginFailed",
                    $"Failed login attempt for username: {model.UserName}",
                    "Anonymous",
                    ipAddress
                );
				
				_logger.LogWarning("Authentication failed for user: {UserName}", model.UserName ?? string.Empty);
				ModelState.AddModelError(string.Empty, "Invalid username or password.");
				return View(model);
			}
			catch (System.Exception ex)
			{
				// Audit log login error
                await _auditLogger.LogAsync(
                    "LoginError",
                    $"Error during login: {ex.Message}",
                    "Anonymous",
                    ipAddress
                );
				_logger.LogError(ex, "Error in Login POST method");
				ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
				return View(model);
			}
		}

		private async Task<ClaimsIdentity> CreateClaimsIdentity(UserDetails user)
		{
			var claims = new List<Claim>
			{
				new(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new(ClaimTypes.Name, user.UserName ?? string.Empty),
				new(ClaimTypes.Email, user.EmailAddress ?? string.Empty),
			};

			if (!string.IsNullOrEmpty(user.UserRoleId.ToString()))
			{
				claims.Add(new(ClaimTypes.Role, user.RoleName));

				// Add permissions/privileges as claims
				foreach (var permission in user.Privileges ?? Enumerable.Empty<string>())
				{
					claims.Add(new("permission", permission));
				}
			}

			return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
		}

		[HttpGet]
		[Route("ChangePassword")]
		public IActionResult ChangePassword()
		{
			_logger.LogInformation("GET ChangePassword method called");
			return View(new ChangePasswordViewModel());
		}

		[AllowAnonymous]
		[HttpGet]
		[Route("AccessDenied")]
		public IActionResult AccessDenied(string? returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View();
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