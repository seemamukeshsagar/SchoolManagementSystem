// SchoolPortalApp/Models/ClassSubjectViewModel.cs
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class ClassSubjectViewModel
    {
        public Guid Id { get; set; }
        
        [Required]
        [Display(Name = "Class")]
        public Guid ClassMasterId { get; set; }
        
        [Required]
        [Display(Name = "Subject")]
        public Guid SubjectId { get; set; }
        
        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
        
        // For dropdown lists
        public List<SelectListItem> Classes { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Subjects { get; set; } = new List<SelectListItem>();
        
        // For display only
        public string? ClassName { get; set; }
        public string? SubjectName { get; set; }
    }

    public class ClassSubjectListItemViewModel
    {
        public Guid Id { get; set; }
        public string? ClassName { get; set; }
        public string? SubjectName { get; set; }
        public bool IsActive { get; set; }
    }
}