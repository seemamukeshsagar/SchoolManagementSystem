using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Linq;
using Schoolortal.Entities.Models;
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
            _logger.LogInformation("POST Login initiated for user: {UserName}", model?.UserName);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid login model state for user: {UserName}", model?.UserName);
                return View(model);
            }

            try
            {
                var username = model.UserName?.Trim() ?? string.Empty;
                var password = model.Password ?? string.Empty;

                var userDetails = _loginService.AuthenticateUser(username, password);
                if (userDetails == null)
                {
                    _logger.LogWarning("Authentication failed for user: {UserName}", username);
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    return View(model);
                }

                _logger.LogInformation("Authentication successful for {UserName}.", username);

                // Prepare key session values
                var fullName = userDetails.FullName ?? $"{userDetails.FirstName} {userDetails.LastName}".Trim();
                var role = userDetails.RoleName ?? "Guest";
                var designation = userDetails.DesignationName ?? "Not Specified";
                var privileges = string.Join(",", userDetails.Privileges ?? Enumerable.Empty<string>());

                // Store in session
                var session = HttpContext.Session;
                session.SetString("UserId", userDetails.Id.ToString());
                session.SetString("UserName", username);
                session.SetString("FullName", fullName);
                session.SetString("Privileges", privileges);
                session.SetString("UserRole", role);
                session.SetString("Designation", designation);

                // Build claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userDetails.Id.ToString()),
                    new Claim(ClaimTypes.Name, fullName),
                    new Claim("UserName", username),
                    new Claim("Designation", designation),
                    new Claim(ClaimTypes.Role, role),
                    new Claim("UserRole", role),
                    new Claim("UserDesignation", designation)
                };

                // Add privilege claims
                if (userDetails.Privileges != null)
                {
                    claims.AddRange(userDetails.Privileges.Select(p => new Claim(ClaimTypes.Role, p)));
                }

                // Sign-in user
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                    new AuthenticationProperties { IsPersistent = true, AllowRefresh = true });

                // Redirect to target
                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login for user: {UserName}", model?.UserName);
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

