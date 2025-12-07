#nullable enable

// SchoolPortalApp/Models/ClassSectionDetailViewModels.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class ClassSectionDetailViewModel
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Class is required")]
        [Display(Name = "Class")]
        public Guid ClassMasterId { get; set; }
        
        [Required(ErrorMessage = "Section is required")]
        [Display(Name = "Section")]
        public Guid SectionMasterId { get; set; }
        
        [Required(ErrorMessage = "Location is required")]
        [Display(Name = "Location")]
        public Guid LocationId { get; set; }
        
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        
        public Guid CompanyId { get; set; }
        public Guid SchoolId { get; set; }
        
        // For dropdowns
        public List<SelectListItem> Classes { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Sections { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Locations { get; set; } = new List<SelectListItem>();
    }

    public class ClassSectionDetailListItemViewModel
    {
        public Guid Id { get; set; }
        public string? ClassName { get; set; }
        public string? SectionName { get; set; }
        public string? LocationName { get; set; }
        public bool IsActive { get; set; }
        public string? Status { get; set; }
    }
}