#nullable enable

using System;
using System.Collections.Generic;

namespace SchoolPortalApp.ViewModels
{
    public class RoleMasterListItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RoleName { get => Name; set => Name = value; } // Alias for Name to match expected usage
        public string? Description { get; set; }
        public bool IsSystemRole { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public List<string> Privileges { get; set; } = new List<string>();
        
        // Additional properties for role management
        public int UserCount { get; set; }
        public DateTime? LastUsedDate { get; set; }
        public string Status => IsActive ? "Active" : "Inactive";
    }
}
