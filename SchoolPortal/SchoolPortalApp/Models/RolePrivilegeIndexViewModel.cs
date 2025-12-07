#nullable enable

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolPortal.Services.IServices;

namespace SchoolPortalApp.Models.RolePrivilege
{
    public class RolePrivilegeIndexViewModel
    {
        public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
        public string? SelectedRoleId { get; set; }
    }

    public class RolePrivilegeDetailsViewModel
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public List<RolePrivilegeViewModel> AssignedPrivileges { get; set; } = new List<RolePrivilegeViewModel>();
    }

    public class RolePrivilegeUpdateRequestModel
    {
        public Guid RoleId { get; set; }
        public List<Guid> PrivilegeIds { get; set; } = new List<Guid>();
    }
}