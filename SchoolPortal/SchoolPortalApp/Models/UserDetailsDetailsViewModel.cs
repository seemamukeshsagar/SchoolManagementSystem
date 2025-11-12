using System;

namespace SchoolPortalApp.Models
{
    public class UserDetailsDetailsViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string DesignationName { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string SchoolName { get; set; } = string.Empty;
        public bool IsSuperUser { get; set; }
        public bool IsActive { get; set; }
    }
}