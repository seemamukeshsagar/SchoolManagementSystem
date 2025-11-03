using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class RolePrivilegeService : IRolePrivilegeService
    {
        private readonly ConnectionManager _connectionManager;
        private readonly IRoleMasterService _roleService;
        private readonly IPrivilegeService _privilegeService;

        public RolePrivilegeService(
            ConnectionManager connectionManager,
            IRoleMasterService roleService,
            IPrivilegeService privilegeService)
        {
            _connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _privilegeService = privilegeService ?? throw new ArgumentNullException(nameof(privilegeService));
        }

        public async Task<IEnumerable<RolePrivilegeViewModel>> GetRolePrivilegesByRoleIdAsync(Guid roleId)
        {
            var result = new List<RolePrivilegeViewModel>();
            var role = await Task.Run(() => _roleService.GetById(roleId));
            
            if (role?.Name == null)
                return result;

            using (var p = new Proc(_connectionManager, "sp_RolePrivilege_GetByRoleId"))
            {
                p["@RoleId"] = roleId;
                var dt = new DataTable();
                p.Exec(dt);

                foreach (DataRow row in dt.Rows)
                {
                    result.Add(new RolePrivilegeViewModel
                    {
                        Id = (Guid)row["Id"],
                        RoleId = roleId,
                        RoleName = role.Name,
                        PrivilegeId = (Guid)row["PrivilegeId"],
                        PrivilegeName = row["PrivilegeName"]?.ToString() ?? string.Empty,
                        IsActive = row["IsActive"] != DBNull.Value && Convert.ToBoolean(row["IsActive"]),
                        IsAssigned = true
                    });
                }
            }

            return result;
        }

        public async Task<bool> UpdateRolePrivilegesAsync(RolePrivilegeUpdateModel model)
        {
            if (model == null || model.PrivilegeIds == null)
                return false;

            try
            {
                using (var p = new Proc(_connectionManager, "sp_RolePrivilege_Update"))
                {
                    // Create a DataTable for the privilege IDs
                    var dt = new DataTable();
                    dt.Columns.Add("Id", typeof(Guid));
                    
                    foreach (var id in model.PrivilegeIds.Distinct())
                    {
                        dt.Rows.Add(id);
                    }

                    p["@RoleId"] = model.RoleId;
                    p["@PrivilegeIds"] = dt;
                    p["@ModifiedBy"] = model.ModifiedBy;
                    p["@ModifiedDate"] = DateTime.UtcNow;

                    p.Exec();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error updating role privileges: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<PrivilegeAssignmentModel>> GetPrivilegesForRoleAssignmentAsync(Guid roleId)
        {
            var result = new List<PrivilegeAssignmentModel>();
            
            // Get all privileges
            var allPrivileges = (await Task.Run(() => _privilegeService.GetAll()))
                ?.Where(p => p != null && p.IsActive && !p.IsDeleted)
                ?.ToList() ?? new List<Privileges>();

            // Get assigned privileges for the role
            var assignedPrivileges = (await GetRolePrivilegesByRoleIdAsync(roleId))
                ?.Where(rp => rp != null && rp.IsActive)
                ?.Select(rp => rp.PrivilegeId)
                ?.ToHashSet() ?? new HashSet<Guid>();

            // Build the hierarchy
            var rootPrivileges = allPrivileges.Where(p => p?.PrivilegeParentId == null);
            
            foreach (var privilege in rootPrivileges)
            {
                // Add the parent privilege
                if (privilege?.PrivilegeName != null)
                {
                    result.Add(new PrivilegeAssignmentModel
                    {
                        Id = privilege.Id,
                        Name = privilege.PrivilegeName,
                        IsAssigned = assignedPrivileges.Contains(privilege.Id),
                        ParentId = null,
                        ParentName = null
                    });
                }

                // Add child privileges
                var childPrivileges = allPrivileges.Where(p => p?.PrivilegeParentId == privilege?.Id);
                foreach (var child in childPrivileges)
                {
                    if (child?.PrivilegeName != null)
                    {
                        result.Add(new PrivilegeAssignmentModel
                        {
                            Id = child.Id,
                            Name = child.PrivilegeName,
                            IsAssigned = assignedPrivileges.Contains(child.Id),
                            ParentId = privilege?.Id ?? Guid.Empty,
                            ParentName = privilege?.PrivilegeName
                        });
                    }
                }
            }

            return result;
        }
    }
}