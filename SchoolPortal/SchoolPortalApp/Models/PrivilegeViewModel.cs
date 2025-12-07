#nullable enable

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class PrivilegeViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Privilege Name")]
        public string PrivilegeName { get; set; } = string.Empty;

        [Display(Name = "Parent Privilege")]
        public Guid? PrivilegeParentId { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        public IEnumerable<SelectListItem> ParentPrivileges { get; set; } = Array.Empty<SelectListItem>();
    }
}