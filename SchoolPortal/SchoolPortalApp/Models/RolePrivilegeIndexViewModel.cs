using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models.RolePrivilege
{
    public class RolePrivilegeIndexViewModel
    {
        public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
        public string SelectedRoleId { get; set; }
    }

    public class RolePrivilegeUpdateRequestModel
    {
        public Guid RoleId { get; set; }
        public List<Guid> PrivilegeIds { get; set; } = new List<Guid>();
    }
}