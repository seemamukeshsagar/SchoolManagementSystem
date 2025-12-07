#nullable enable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class RoleMasterViewModel
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Role Name is required")]
        [StringLength(100, ErrorMessage = "Role Name cannot exceed 100 characters")]
        [Display(Name = "Role Name")]
        public string? RoleName { get; set; }
        
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }
        
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;
        
        // For dropdowns
        public List<SelectListItem> StatusList { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "true", Text = "Active" },
            new SelectListItem { Value = "false", Text = "Inactive" }
        };
    }

    public class RoleMasterListItemViewModel
    {
        public Guid Id { get; set; }
        public string? RoleName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string Status => IsActive ? "Active" : "Inactive";
    }

    public class RoleMasterDetailsViewModel
    {
        public Guid Id { get; set; }
        public string? RoleName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string Status => IsActive ? "Active" : "Inactive";
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}