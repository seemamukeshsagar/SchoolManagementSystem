#nullable enable

using System;
using System.Collections.Generic;

namespace SchoolPortalApp.ViewModels
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

		// Audit fields
		public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
	}
}