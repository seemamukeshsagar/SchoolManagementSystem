#nullable enable

using System;

namespace SchoolPortalApp.Models
{
    public class PrivilegeDetailsViewModel
    {
        public Guid Id { get; set; }
        public string PrivilegeName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string ParentPrivilegeName { get; set; } = string.Empty;
    }
}