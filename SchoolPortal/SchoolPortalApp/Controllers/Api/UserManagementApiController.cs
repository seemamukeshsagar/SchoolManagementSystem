using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using SchoolPortalApp.DTOs.UserManagement;
using SchoolPortalApp.Helpers;
using SchoolPortalApp.ViewModels;

namespace SchoolPortalApp.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin")]
    [EnableRateLimiting("api")]
    public class UserManagementApiController : ControllerBase
    {
        private readonly IUserDetailsService _userService;
        private readonly IRoleMasterService _roleService;
        private readonly IPrivilegeService _privilegeService;
        private readonly ILogger<UserManagementApiController> _logger;
        private readonly UserHelper _userHelper;

        public UserManagementApiController(
            IUserDetailsService userService,
            IRoleMasterService roleService,
            IPrivilegeService privilegeService,
            ILogger<UserManagementApiController> logger,
            UserHelper userHelper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _privilegeService = privilegeService ?? throw new ArgumentNullException(nameof(privilegeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userHelper = userHelper ?? throw new ArgumentNullException(nameof(userHelper));
        }

        #region User Endpoints

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = _userService.GetAll();
            var result = users.Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.EmailAddress,
                FirstName = u.FirstName,
                LastName = u.LastName,
                IsActive = u.IsActive,
                RoleIds = !string.IsNullOrEmpty(u.UserRoleId.ToString()) && Guid.TryParse(u.UserRoleId.ToString(), out var roleId) 
                    ? new List<Guid> { roleId } 
                    : new List<Guid>()
            }).ToList();

            return Ok(result);
        }

        [HttpPost("users")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = _userService.GetByUsernameOrEmail(userDto.UserName, userDto.Email);
            if (existingUser != null)
                return Conflict("User with this username or email already exists");

            var user = new UserDetails
            {
                Id = Guid.NewGuid(),
                UserName = userDto.UserName,
                EmailAddress = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                IsActive = userDto.IsActive,
                CreatedDate = DateTime.UtcNow
            };

            _userService.Create(user);
            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, userDto);
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserDto userDto)
        {
            if (id != userDto.Id)
                return BadRequest("ID mismatch");

            var existingUser = _userService.GetById(id);
            if (existingUser == null)
                return NotFound("User not found");

            existingUser.UserName = userDto.UserName;
            existingUser.EmailAddress = userDto.Email;
            existingUser.FirstName = userDto.FirstName;
            existingUser.LastName = userDto.LastName;
            existingUser.IsActive = userDto.IsActive;

            _userService.Update(existingUser);
            return NoContent();
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound("User not found");

            _userService.Delete(id);
            return NoContent();
        }

        #endregion

        #region Role Endpoints

        [HttpGet("roles")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetRoles()
        {
            var roles = _roleService.GetAll();
            var result = roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                IsSystemRole = r.IsSystemRole
            }).ToList();

            return Ok(result);
        }

        [HttpPost("roles")]
        public async Task<IActionResult> CreateRole([FromBody] RoleDto roleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingRole = _roleService.GetAll().FirstOrDefault(r => r.Name.Equals(roleDto.Name, StringComparison.OrdinalIgnoreCase));
            if (existingRole != null)
                return Conflict("Role with this name already exists");

            var role = new RoleMaster
            {
                Id = Guid.NewGuid(),
                Name = roleDto.Name,
                Description = roleDto.Description,
                //IsSystemRole = false,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            _roleService.Create(role);
            return CreatedAtAction(nameof(GetRoles), new { id = role.Id }, roleDto);
        }

        [HttpPut("roles/{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] RoleDto roleDto)
        {
            if (id != roleDto.Id)
                return BadRequest("ID mismatch");

            var existingRole = _roleService.GetById(id);
            if (existingRole == null)
                return NotFound("Role not found");

            //if (existingRole.IsSystemRole)
            //    return BadRequest("System roles cannot be modified");

            existingRole.Name = roleDto.Name;
            existingRole.Description = roleDto.Description;

            _roleService.Update(existingRole);
            return NoContent();
        }

        [HttpDelete("roles/{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var role = _roleService.GetById(id);
            if (role == null)
                return NotFound("Role not found");

            //if (role.IsSystemRole)
            //    return BadRequest("System roles cannot be deleted");

            //if (_userService.GetUsersByRoleId(id).Any())
            //    return BadRequest("Cannot delete role that is assigned to users");

            _roleService.Delete(id);
            return NoContent();
        }

        #endregion

        #region Permission Endpoints

        [HttpGet("permissions")]
        public async Task<IActionResult> GetAllPermissions()
        {
            try
            {
                var privileges = _privilegeService.GetAll()
                    .Where(p => p != null && p.IsActive && !p.IsDeleted)
                    .OrderBy(p => p.PrivilegeName)
                    .Select(p => new PermissionDto
                    {
                        Name = p.PrivilegeName ?? string.Empty,
                        Description = p.PrivilegeName?.Replace("_", " ") ?? string.Empty,
                        Category = GetPermissionCategory(p.PrivilegeName),
                        IsGranted = false
                    })
                    .ToList();

                return Ok(privileges);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching permissions from database");
                return StatusCode(500, "An error occurred while fetching permissions");
            }
        }

        [HttpGet("roles/{roleId}/permissions")]
        public async Task<IActionResult> GetRolePermissions(Guid roleId)
        {
            try
            {
                var role = await _roleService.GetByIdAsync(roleId);
                if (role == null)
                    return NotFound("Role not found");

                var allPermissions = await GetAllPermissions();
                var assignedPrivileges = await _roleService.GetRolePrivilegesAsync(roleId);
                var assignedPermissionNames = assignedPrivileges
                    .Where(p => p != null && p.IsActive)
                    .Select(p => p.PrivilegeName)
                    .Where(name => !string.IsNullOrEmpty(name))
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                if (allPermissions is List<PermissionDto> permissionList)
                {
                    foreach (var permission in permissionList)
                    {
                        permission.IsGranted = !string.IsNullOrEmpty(permission.Name) && 
                                            assignedPermissionNames.Contains(permission.Name);
                    }
                }

                return allPermissions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching role permissions for role {RoleId}", roleId);
                return StatusCode(500, "An error occurred while fetching role permissions");
            }
        }

        [HttpPost("roles/{roleId}/permissions")]
        public async Task<IActionResult> UpdateRolePermissions(Guid roleId, [FromBody] UpdatePermissionsRequest request)
        {
            if (roleId != request.RoleId)
                return BadRequest("Role ID mismatch");

            try
            {
                var role = await _roleService.GetByIdAsync(roleId);
                if (role == null)
                    return NotFound("Role not found");

                if (role.IsSystemRole)
                    return BadRequest("Cannot modify permissions for system roles");

                // Get all valid privilege names from the database
                var allPrivileges = _privilegeService.GetAll()
                    .Where(p => p != null && p.IsActive && !p.IsDeleted)
                    .Select(p => p.PrivilegeName)
                    .Where(name => !string.IsNullOrEmpty(name))
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                // Filter permissions to only include those that exist in the database
                var validPermissions = request.Permissions
                    .Where(p => !string.IsNullOrEmpty(p) && allPrivileges.Contains(p))
                    .ToList();

                var permissionsDict = validPermissions.ToDictionary(p => p, _ => true);
                await _roleService.UpdateRolePermissionsAsync(roleId, permissionsDict);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating permissions for role {RoleId}", roleId);
                return StatusCode(500, "An error occurred while updating permissions");
            }
        }

        #endregion

        private string GetPermissionCategory(string? permissionName)
        {
            if (string.IsNullOrEmpty(permissionName))
                return "Other";

            var parts = permissionName.Split('_', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
                return "Other";

            // Get the first part of the permission name as the category
            var category = parts[0];
            
            // Add space before capital letters after the first one for better display
            category = string.Concat(
                category.Select((c, i) => i > 0 && char.IsUpper(c) ? " " + c.ToString() : c.ToString())
            );

            return category;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CheckPermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _permission;
        public CheckPermissionAttribute(string permission)
        {
            _permission = permission;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var user = context.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Check if user is in SuperAdmin role (bypass permission check)
            if (user.IsInRole("SuperAdmin"))
            {
                return;
            }

            // Check for the specific permission
            var hasPermission = user.HasClaim(c => 
                string.Equals(c.Type, "permission", StringComparison.OrdinalIgnoreCase) && 
                string.Equals(c.Value, _permission, StringComparison.OrdinalIgnoreCase));
                
            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}