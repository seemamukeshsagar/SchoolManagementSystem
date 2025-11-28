using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolPortalApp.Controllers
{
	public abstract class BaseController : Controller
	{
		protected Guid? CurrentSchoolId
		{
			get
			{
				var value = HttpContext?.Session?.GetString("SchoolId");
				if (string.IsNullOrWhiteSpace(value)) return null;
				return Guid.TryParse(value, out var id) ? id : (Guid?)null;
			}
		}

		protected Guid? CurrentUserId
		{
			get
			{
				var value = HttpContext?.Session?.GetString("UserId");
				if (string.IsNullOrWhiteSpace(value)) return null;
				return Guid.TryParse(value, out var id) ? id : (Guid?)null;
			}
		}

		protected Guid? CurrentCompanyId
		{
			get
			{
				var value = HttpContext?.Session?.GetString("CompanyId");
				if (string.IsNullOrWhiteSpace(value)) return null;
				return Guid.TryParse(value, out var id) ? id : (Guid?)null;
			}
		}
		
		protected bool IsAjaxRequest()
        {
            return Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        protected IActionResult AjaxView(string viewName, object model = null)
        {
            if (IsAjaxRequest())
            {
                return PartialView(viewName, model);
            }
            return View(viewName, model);
        }

        protected IActionResult AjaxView(object model = null)
        {
            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            return AjaxView(actionName, model);
        }
	}
}
