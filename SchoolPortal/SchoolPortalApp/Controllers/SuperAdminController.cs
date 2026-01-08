using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.ViewModels;
using SchoolPortal.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortalApp.Controllers
{
	//[Authorize(Roles = "SuperAdmin,SuperAdministrator,Super Administrator")]
	public class SuperAdminController : Controller
	{
		private readonly ILogger<SuperAdminController> _logger;
		private readonly IUserDetailsService _userService;
		private readonly IRoleMasterService _roleService;

		public SuperAdminController(
			ILogger<SuperAdminController> logger,
			IUserDetailsService userService,
			IRoleMasterService roleService)
		{
			_logger = logger;
			_userService = userService ?? throw new ArgumentNullException(nameof(userService));
			_roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
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
		public async Task<IActionResult> Users()
		{
			try
			{
				// Get all users with their role information
				var users = _userService.GetAll();
				
				// Map to UserViewModel
				var model = users.Select(u => new UserViewModel
				{
					Id = u.Id.ToString(),
					UserName = u.UserName,
					FirstName = u.FirstName,
					LastName = u.LastName,
					Email = u.EmailAddress,
					IsActive = u.IsActive,
					RoleName = u.RoleName
					// Map the RoleName from UserDetailsListViewModel to a list of RoleViewModel
					// Roles = !string.IsNullOrEmpty(u.RoleName)
					// 	? new List<RoleViewModel>
					// 	{
					// 		new RoleViewModel
					// 		{
					// 			Id = u.UserRoleId?.ToString() ?? string.Empty,
					// 			Name = u.RoleName
					// 		}
					// 	}
					// 	: new List<RoleViewModel>()
				}).ToList();

				return View("UserManagement/Users", model);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving users");
				TempData["ErrorMessage"] = "An error occurred while retrieving users.";
				return View("UserManagement/Users", new List<UserViewModel>());
			}
		}

		public async Task<IActionResult> Roles()
		{
			try
			{
				var roles = _roleService.GetAll() ?? new List<RoleMaster>();
				var model = roles.Select(r => new RoleMasterListItemViewModel
				{
					Id = r.Id,
					RoleName = r.Name,
					Description = r.Description,
					IsActive = r.IsActive
				}).ToList();

				return View("UserManagement/Roles", model);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving roles");
				TempData["ErrorMessage"] = "An error occurred while retrieving roles.";
				return View("UserManagement/Roles", new List<RoleMasterListItemViewModel>());
			}
		}

		public async Task<IActionResult> Permissions()
		{
			try
			{
				var roles = _roleService.GetAll() ?? new List<RoleMaster>();
				var model = roles.Select(r => new RoleMasterListItemViewModel
				{
					Id = r.Id,
					RoleName = r.Name,
					Description = r.Description,
					IsActive = r.IsActive
				}).ToList();

				return View("UserManagement/Permissions", model);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error loading permissions");
				TempData["ErrorMessage"] = "An error occurred while loading permissions.";
				return View("UserManagement/Permissions", new List<RoleMasterListItemViewModel>());
			}
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
