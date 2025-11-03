using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IRolePrivilegeService
    {
        Task<IEnumerable<RolePrivilegeViewModel>> GetRolePrivilegesByRoleIdAsync(Guid roleId);
        Task<bool> UpdateRolePrivilegesAsync(RolePrivilegeUpdateModel model);
        Task<IEnumerable<PrivilegeAssignmentModel>> GetPrivilegesForRoleAssignmentAsync(Guid roleId);
    }

    public class RolePrivilegeViewModel
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public required string RoleName { get; set; } = string.Empty;
        public Guid PrivilegeId { get; set; }
        public required string PrivilegeName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsAssigned { get; set; }
    }

    public class RolePrivilegeUpdateModel
    {
        public Guid RoleId { get; set; }
        public required List<Guid> PrivilegeIds { get; set; } = new();
        public Guid ModifiedBy { get; set; }
    }

    public class PrivilegeAssignmentModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public bool IsAssigned { get; set; }
        public Guid? ParentId { get; set; }
        public string? ParentName { get; set; }
    }
}