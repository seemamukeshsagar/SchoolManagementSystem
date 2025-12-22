using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SchoolPortalApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ILogger _logger;

        protected BaseController(ILogger<BaseController> logger = null)
        {
            _logger = logger;
        }

        protected Guid? CurrentSchoolId
        {
            get
            {
                try
                {
                    var value = HttpContext?.Session?.GetString("SchoolId");
                    if (string.IsNullOrWhiteSpace(value)) 
                    {
                        _logger?.LogWarning("SchoolId not found in session");
                        return null;
                    }
                    if (Guid.TryParse(value, out var id)) 
                        return id;
                    
                    _logger?.LogWarning($"Invalid SchoolId format in session: {value}");
                    return null;
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Error getting SchoolId from session");
                    return null;
                }
            }
        }

        protected Guid? CurrentUserId
        {
            get
            {
                try
                {
                    var value = HttpContext?.Session?.GetString("UserId");
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        _logger?.LogWarning("UserId not found in session");
                        return null;
                    }
                    if (Guid.TryParse(value, out var id))
                        return id;
                    
                    _logger?.LogWarning($"Invalid UserId format in session: {value}");
                    return null;
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Error getting UserId from session");
                    return null;
                }
            }
        }

        protected Guid? CurrentCompanyId
        {
            get
            {
                try
                {
                    var value = HttpContext?.Session?.GetString("CompanyId");
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        _logger?.LogWarning("CompanyId not found in session");
                        return null;
                    }
                    if (Guid.TryParse(value, out var id))
                        return id;
                    
                    _logger?.LogWarning($"Invalid CompanyId format in session: {value}");
                    return null;
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Error getting CompanyId from session");
                    return null;
                }
            }
        }
        
        protected bool IsAjaxRequest()
        {
            try
            {
                return string.Equals(
                    Request?.Headers["X-Requested-With"],
                    "XMLHttpRequest",
                    StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error checking if request is AJAX");
                return false;
            }
        }

        protected IActionResult AjaxView(string viewName, object? model = null)
        {
            try
            {
                if (string.IsNullOrEmpty(viewName))
                {
                    viewName = ControllerContext.RouteData.Values["action"]?.ToString() ?? string.Empty;
                    if (string.IsNullOrEmpty(viewName))
                    {
                        _logger?.LogError("Could not determine view name for AjaxView");
                        return View(model);
                    }
                }

                if (IsAjaxRequest())
                {
                    return PartialView(viewName, model);
                }
                return View(viewName, model);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error in AjaxView");
                return View(viewName, model);
            }
        }

        protected IActionResult AjaxView(object? model = null)
        {
            try
            {
                var viewName = ControllerContext.RouteData.Values["action"]?.ToString();
                if (string.IsNullOrEmpty(viewName))
                {
                    _logger?.LogError("Could not determine view name for parameterless AjaxView");
                    return View(model);
                }
                return AjaxView(viewName, model);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error in parameterless AjaxView");
                return View(model);
            }
        }

        protected void SetSessionValue(string key, string value)
        {
            try
            {
                if (HttpContext?.Session != null)
                {
                    HttpContext.Session.SetString(key, value);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Error setting session value for key: {key}");
            }
        }

        protected string? GetSessionValue(string key)
        {
            try
            {
                return HttpContext?.Session?.GetString(key);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Error getting session value for key: {key}");
                return null;
            }
        }
    }
}