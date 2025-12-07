#nullable enable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class DeptMasterViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Department Code")]
        public string DeptCode { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Department Name")]
        public string DeptName { get; set; } = string.Empty;

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        [Required]
        [Display(Name = "School")]
        public Guid SchoolId { get; set; }

        public IEnumerable<SelectListItem> Schools { get; set; } = Array.Empty<SelectListItem>();
    }
}