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
		private readonly IBackupService _backupService;
		private readonly ICacheService _cacheService;
		private readonly IMaintenanceService _maintenanceService;
		private readonly ISecurityService _securityService;

		public SuperAdminController(
			ILogger<SuperAdminController> logger,
			IUserDetailsService userService,
			IRoleMasterService roleService,
			IBackupService backupService,
			ICacheService cacheService,
			IMaintenanceService maintenanceService,
			ISecurityService securityService)
		{
			_logger = logger;
			_userService = userService ?? throw new ArgumentNullException(nameof(userService));
			_roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
			_backupService = backupService ?? throw new ArgumentNullException(nameof(backupService));
			_cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
			_maintenanceService = maintenanceService ?? throw new ArgumentNullException(nameof(maintenanceService));
			_securityService = securityService ?? throw new ArgumentNullException(nameof(securityService));
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

		#region Backup Actions
		[HttpPost]
		public async Task<IActionResult> RunBackup(string backupName = null, bool includeMedia = true)
		{
			try
			{
				var success = await _backupService.CreateBackupAsync(backupName, includeMedia);
				
				if (success)
				{
					TempData["SuccessMessage"] = "Database backup completed successfully.";
					_logger.LogInformation("Manual database backup completed successfully");
				}
				else
				{
					TempData["ErrorMessage"] = "Failed to create database backup. Please check the logs for details.";
					_logger.LogError("Manual database backup failed");
				}
				
				return RedirectToAction("QuickActions");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error during manual backup process");
				TempData["ErrorMessage"] = "An error occurred during backup process.";
				return RedirectToAction("QuickActions");
			}
		}

		[HttpPost]
		public async Task<IActionResult> RestoreBackup(string backupPath)
		{
			try
			{
				var success = await _backupService.RestoreBackupAsync(backupPath);
				
				if (success)
				{
					TempData["SuccessMessage"] = "Database restore completed successfully.";
					_logger.LogInformation($"Database restore completed from: {backupPath}");
				}
				else
				{
					TempData["ErrorMessage"] = "Failed to restore database backup. Please check the logs for details.";
					_logger.LogError($"Database restore failed from: {backupPath}");
				}
				
				return RedirectToAction("BackupRestore");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error during database restore from: {backupPath}");
				TempData["ErrorMessage"] = "An error occurred during restore process.";
				return RedirectToAction("BackupRestore");
			}
		}

		[HttpPost]
		public async Task<IActionResult> DeleteBackup(string backupPath)
		{
			try
			{
				var success = await _backupService.DeleteBackupAsync(backupPath);
				
				if (success)
				{
					TempData["SuccessMessage"] = "Backup file deleted successfully.";
					_logger.LogInformation($"Backup file deleted: {backupPath}");
				}
				else
				{
					TempData["ErrorMessage"] = "Failed to delete backup file.";
					_logger.LogError($"Failed to delete backup file: {backupPath}");
				}
				
				return RedirectToAction("BackupRestore");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error deleting backup file: {backupPath}");
				TempData["ErrorMessage"] = "An error occurred while deleting backup file.";
				return RedirectToAction("BackupRestore");
			}
		}

		public async Task<IActionResult> GetBackupList()
		{
			try
			{
				var backups = await _backupService.GetAvailableBackupsAsync();
				return Json(backups);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving backup list");
				return Json(new { error = "Failed to retrieve backup list" });
			}
		}
		#endregion

		#region Cache Actions
		[HttpPost]
		public async Task<IActionResult> ClearCache()
		{
			try
			{
				var success = await _cacheService.ClearSystemCacheAsync();
				
				if (success)
				{
					TempData["SuccessMessage"] = "System cache cleared successfully.";
					_logger.LogInformation("System cache cleared successfully");
				}
				else
				{
					TempData["ErrorMessage"] = "Failed to clear system cache. Please check the logs for details.";
					_logger.LogError("System cache clearing failed");
				}
				
				return RedirectToAction("QuickActions");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error during cache clearing process");
				TempData["ErrorMessage"] = "An error occurred during cache clearing process.";
				return RedirectToAction("QuickActions");
			}
		}

		[HttpPost]
		public async Task<IActionResult> ClearTempFiles()
		{
			try
			{
				var success = await _cacheService.ClearTemporaryFilesAsync();
				
				if (success)
				{
					TempData["SuccessMessage"] = "Temporary files cleared successfully.";
					_logger.LogInformation("Temporary files cleared successfully");
				}
				else
				{
					TempData["ErrorMessage"] = "Failed to clear temporary files. Please check the logs for details.";
					_logger.LogError("Temporary files clearing failed");
				}
				
				return RedirectToAction("QuickActions");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error during temporary files clearing process");
				TempData["ErrorMessage"] = "An error occurred during temporary files clearing process.";
				return RedirectToAction("QuickActions");
			}
		}

		public async Task<IActionResult> GetCacheInfo()
		{
			try
			{
				var cacheInfo = await _cacheService.GetCacheInfoAsync();
				return Json(cacheInfo);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving cache information");
				return Json(new { error = "Failed to retrieve cache information" });
			}
		}
		#endregion

		#region Maintenance Actions
		[HttpPost]
		public async Task<IActionResult> RunMaintenance()
		{
			try
			{
				var success = await _maintenanceService.RunSystemMaintenanceAsync();
				
				if (success)
				{
					TempData["SuccessMessage"] = "System maintenance completed successfully.";
					_logger.LogInformation("System maintenance completed successfully");
				}
				else
				{
					TempData["ErrorMessage"] = "System maintenance completed with some issues. Please check the logs for details.";
					_logger.LogWarning("System maintenance completed with issues");
				}
				
				return RedirectToAction("QuickActions");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error during system maintenance");
				TempData["ErrorMessage"] = "An error occurred during system maintenance.";
				return RedirectToAction("QuickActions");
			}
		}

		[HttpPost]
		public async Task<IActionResult> OptimizeDatabase()
		{
			try
			{
				var success = await _maintenanceService.OptimizeDatabaseAsync();
				
				if (success)
				{
					TempData["SuccessMessage"] = "Database optimization completed successfully.";
					_logger.LogInformation("Database optimization completed successfully");
				}
				else
				{
					TempData["ErrorMessage"] = "Database optimization failed. Please check the logs for details.";
					_logger.LogError("Database optimization failed");
				}
				
				return RedirectToAction("QuickActions");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error during database optimization");
				TempData["ErrorMessage"] = "An error occurred during database optimization.";
				return RedirectToAction("QuickActions");
			}
		}

		[HttpPost]
		public async Task<IActionResult> CleanUpOrphanedRecords()
		{
			try
			{
				var success = await _maintenanceService.CleanUpOrphanedRecordsAsync();
				
				if (success)
				{
					TempData["SuccessMessage"] = "Orphaned records cleanup completed successfully.";
					_logger.LogInformation("Orphaned records cleanup completed successfully");
				}
				else
				{
					TempData["ErrorMessage"] = "Orphaned records cleanup failed. Please check the logs for details.";
					_logger.LogError("Orphaned records cleanup failed");
				}
				
				return RedirectToAction("QuickActions");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error during orphaned records cleanup");
				TempData["ErrorMessage"] = "An error occurred during orphaned records cleanup.";
				return RedirectToAction("QuickActions");
			}
		}

		public async Task<IActionResult> GetMaintenanceReport()
		{
			try
			{
				var report = await _maintenanceService.GetMaintenanceReportAsync();
				return Json(report);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving maintenance report");
				return Json(new { error = "Failed to retrieve maintenance report" });
			}
		}
		#endregion

		#region Security Actions
		[HttpPost]
		public async Task<IActionResult> StartSecurityScan()
		{
			try
			{
				var result = await _securityService.RunSecurityAuditAsync();
				
				if (result.OverallSuccess)
				{
					if (result.CriticalIssues == 0 && result.WarningIssues == 0)
					{
						TempData["SuccessMessage"] = $"Security scan completed successfully. No issues found.";
					}
					else
					{
						TempData["SuccessMessage"] = $"Security scan completed. Found {result.CriticalIssues} critical, {result.WarningIssues} warning, and {result.InfoIssues} info issues.";
					}
					_logger.LogInformation($"Security scan completed successfully: Critical={result.CriticalIssues}, Warnings={result.WarningIssues}, Info={result.InfoIssues}");
				}
				else
				{
					TempData["ErrorMessage"] = $"Security scan completed with {result.CriticalIssues} critical issues. Immediate attention required.";
					_logger.LogWarning($"Security scan completed with critical issues: {result.CriticalIssues}");
				}
				
				return RedirectToAction("QuickActions");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error during security scan");
				TempData["ErrorMessage"] = "An error occurred during security scan.";
				return RedirectToAction("QuickActions");
			}
		}

		public async Task<IActionResult> GetSecurityReport()
		{
			try
			{
				var report = await _securityService.GetSecurityReportAsync();
				return Json(report);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving security report");
				return Json(new { error = "Failed to retrieve security report" });
			}
		}

		[HttpPost]
		public async Task<IActionResult> CheckUserPermissions()
		{
			try
			{
				var result = await _securityService.CheckUserPermissionsAsync();
				
				if (result)
				{
					TempData["SuccessMessage"] = "User permissions check completed successfully.";
				}
				else
				{
					TempData["ErrorMessage"] = "User permissions check found issues.";
				}
				
				return RedirectToAction("QuickActions");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error during user permissions check");
				TempData["ErrorMessage"] = "An error occurred during user permissions check.";
				return RedirectToAction("QuickActions");
			}
		}

		[HttpPost]
		public async Task<IActionResult> CheckDatabaseSecurity()
		{
			try
			{
				var result = await _securityService.CheckDatabaseSecurityAsync();
				
				if (result)
				{
					TempData["SuccessMessage"] = "Database security check completed successfully.";
				}
				else
				{
					TempData["ErrorMessage"] = "Database security check found issues.";
				}
				
				return RedirectToAction("QuickActions");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error during database security check");
				TempData["ErrorMessage"] = "An error occurred during database security check.";
				return RedirectToAction("QuickActions");
			}
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
				var model = roles.Select(r => new RoleViewModel
				{
					Id = r.Id.ToString(),
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
				return View("UserManagement/Roles", new List<RoleViewModel>());
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
