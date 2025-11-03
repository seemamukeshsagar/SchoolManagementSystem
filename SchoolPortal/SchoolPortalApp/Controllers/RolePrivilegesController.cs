using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using SchoolPortalApp.Models;
using SchoolPortalApp.Models.RolePrivilege;

namespace SchoolPortalApp.Controllers
{
    [Route("RolePrivileges")]
    public class RolePrivilegesController : Controller
    {
        private readonly IRolePrivilegeService _rolePrivilegeService;
        private readonly IRoleMasterService _roleService;
        private readonly ILogger<RolePrivilegesController> _logger;

        public RolePrivilegesController(
            IRolePrivilegeService rolePrivilegeService,
            IRoleMasterService roleService,
            ILogger<RolePrivilegesController> logger)
        {
            _rolePrivilegeService = rolePrivilegeService;
            _roleService = roleService;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var roles = _roleService.GetAll()
                    .Where(r => r.IsActive && !r.IsDeleted)
                    .Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name
                    })
                    .ToList();

                var model = new RolePrivilegeIndexViewModel
                {
                    Roles = roles
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading role privileges index");
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
            }
        }

        [HttpGet]
        [Route("GetPrivileges/{roleId}")]
        public async Task<IActionResult> GetPrivileges(Guid roleId)
        {
            try
            {
                var privileges = await _rolePrivilegeService.GetPrivilegesForRoleAssignmentAsync(roleId);
                return PartialView("_PrivilegeList", privileges);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting privileges for role {roleId}");
                return StatusCode(500, "An error occurred while loading privileges.");
            }
        }

        [HttpPost]
        [Route("UpdatePrivileges")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePrivileges([FromBody] RolePrivilegeUpdateRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userIdStr = HttpContext.Session.GetString("UserId");
                if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
                {
                    return Unauthorized("User not authenticated");
                }

                var updateModel = new RolePrivilegeUpdateModel
                {
                    RoleId = model.RoleId,
                    PrivilegeIds = model.PrivilegeIds ?? new List<Guid>(),
                    ModifiedBy = userId
                };

                var result = await _rolePrivilegeService.UpdateRolePrivilegesAsync(updateModel);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating privileges for role {model?.RoleId}");
                return StatusCode(500, "An error occurred while updating privileges.");
            }
        }
    }
}