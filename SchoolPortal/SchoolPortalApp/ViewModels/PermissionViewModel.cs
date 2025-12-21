using System;
using System.Collections.Generic;

namespace SchoolPortalApp.ViewModels
{
    public class PermissionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Group { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsSystemPermission { get; set; }
        public List<RoleViewModel> Roles { get; set; } = new List<RoleViewModel>();
    }
}
