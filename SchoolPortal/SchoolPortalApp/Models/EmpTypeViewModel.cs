#nullable enable

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class EmpTypeViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Type Name")]
        public string TypeName { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Company")]
        public Guid CompanyId { get; set; }

        [Required]
        [Display(Name = "School")]
        public Guid SchoolId { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        public IEnumerable<SelectListItem> Companies { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Schools { get; set; } = Array.Empty<SelectListItem>();
    }
}

