using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class DesigMasterViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Code")]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Designation Name")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        [Required]
        [Display(Name = "School")]
        public Guid SchoolId { get; set; }

        public IEnumerable<SelectListItem> Schools { get; set; } = Array.Empty<SelectListItem>();
    }
}