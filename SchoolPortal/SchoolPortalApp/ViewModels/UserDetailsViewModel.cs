using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortalApp.ViewModels
{
    public class UserDetailsViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Guid DesignationId { get; set; }
        public string DesignationName { get; set; } = string.Empty;
        public Guid? UserRoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public List<string> Privileges { get; set; } = new();
        public bool? IsSuperUser { get; set; }
        public Guid? CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public Guid? SchoolId { get; set; }
        public string SchoolName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
