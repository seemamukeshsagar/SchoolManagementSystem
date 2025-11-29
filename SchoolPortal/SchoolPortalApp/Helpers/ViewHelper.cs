// Create this file at: SchoolPortalApp/Helpers/ViewHelper.cs
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SchoolPortalApp.Helpers
{
    public static class ViewHelper
    {
        public static bool IsActive(ViewContext viewContext, string controller, string action = "Index")
        {
            var routeData = viewContext.RouteData.Values;
            var routeController = routeData["controller"]?.ToString();
            var routeAction = routeData["action"]?.ToString();
            
            return string.Equals(routeController, controller, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(routeAction, action, StringComparison.OrdinalIgnoreCase);
        }
    }
}