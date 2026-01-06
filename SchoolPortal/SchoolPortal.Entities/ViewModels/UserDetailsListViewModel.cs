#nullable enable

using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models
{
    public class UserDetailsListViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        
        // ID fields
        public Guid DesignationId { get; set; }
        public Guid? UserRoleId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? SchoolId { get; set; }
        
        // Name fields (will be populated in the service)
        public string FullName => $"{FirstName} {LastName}".Trim();
        public string RoleName { get; set; } = string.Empty;
        public string DesignationName { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string SchoolName { get; set; } = string.Empty;
        
        // Additional properties for role-based access
        public List<string> Privileges { get; set; } = new List<string>();
        
        // Navigation properties
        public List<RolePrivilegeDto> RolePrivileges { get; set; } = new List<RolePrivilegeDto>();
    }
}

public class RolePrivilegeDto
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public Guid PrivilegeId { get; set; }
    public string PrivilegeName { get; set; } = string.Empty;
    public bool CanView { get; set; }
    public bool CanAdd { get; set; }
    public bool CanEdit { get; set; }
    public bool CanDelete { get; set; }
    public bool CanPrint { get; set; }
    public bool CanExport { get; set; }
    public bool CanImport { get; set; }
    public bool IsActive { get; set; } = true;
    public string Name { get; set; } = string.Empty;
}
