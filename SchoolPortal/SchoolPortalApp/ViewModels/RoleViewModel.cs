using System;
using System.Collections.Generic;

namespace SchoolPortalApp.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsSystemRole { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDefault { get; set; } = false;
        public DateTime LastModified { get; set; }
        public int UsersCount { get; set; }
        public List<PermissionViewModel> Permissions { get; set; } = new List<PermissionViewModel>();
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
        public bool IsActive { get; set; }
    }
}
