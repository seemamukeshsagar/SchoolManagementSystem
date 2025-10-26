using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SchoolPortal.Services;
using Schoolortal.Entities.Models;
using SchoolPortalApp.Models;

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
        public IActionResult Login(LoginViewModel model, string? returnUrl = null)
        {
            _logger.LogInformation("POST Login method called. UserName: {UserName}", model?.UserName);
            
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("ModelState is invalid");
                    return View(model);
                }

                _logger.LogInformation("Authenticating user: {UserName}", model.UserName);
                var userDetails = _loginService.AuthenticateUser(model.UserName, model.Password);
                
                if (userDetails != null)
                {
                    _logger.LogInformation("Authentication successful for user: {UserName}. Privileges: {Privileges}", 
                        userDetails.UserName, string.Join(", ", userDetails.Privileges));
                    
                    // Store user details in session (you may want to use proper authentication/session management)
                    HttpContext.Session.SetString("UserId", userDetails.Id.ToString());
                    HttpContext.Session.SetString("UserName", userDetails.UserName);
                    HttpContext.Session.SetString("FullName", userDetails.FullName);
                    HttpContext.Session.SetString("Privileges", string.Join(",", userDetails.Privileges));
                    
                    // Redirect to home or returnUrl if provided
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

                _logger.LogWarning("Authentication failed for user: {UserName}", model.UserName);
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

