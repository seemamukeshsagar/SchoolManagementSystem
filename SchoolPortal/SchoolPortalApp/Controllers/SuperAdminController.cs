using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace SchoolPortalApp.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : Controller
    {
        private readonly ILogger<SuperAdminController> _logger;

        public SuperAdminController(ILogger<SuperAdminController> logger)
        {
            _logger = logger;
        }

        #region Dashboard Actions
        public IActionResult Dashboard()
        {
            return View("Dashboard/Dashboard");
        }

        public IActionResult SystemHealth()
        {
            return View("Dashboard/SystemHealth");
        }

        public IActionResult QuickActions()
        {
            return View("Dashboard/QuickActions");
        }

        public IActionResult Alerts()
        {
            return View("Dashboard/Alerts");
        }
        #endregion

        #region System Administration
        public IActionResult SystemSettings()
        {
            return View("SystemAdmin/SystemSettings");
        }

        public IActionResult Database()
        {
            return View("SystemAdmin/Database");
        }

        public IActionResult BackupRestore()
        {
            return View("SystemAdmin/BackupRestore");
        }
        #endregion

        #region User Management
        public IActionResult Users()
        {
            return View("UserManagement/Users");
        }

        public IActionResult Roles()
        {
            return View("UserManagement/Roles");
        }

        public IActionResult Permissions()
        {
            return View("UserManagement/Permissions");
        }
        #endregion

        #region Security
        public IActionResult AuditLogs()
        {
            return View("Security/AuditLogs");
        }

        public IActionResult SecuritySettings()
        {
            return View("Security/SecuritySettings");
        }
        #endregion

        #region Error Handling
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error/DatabaseError");
        }
        #endregion
    }
}
