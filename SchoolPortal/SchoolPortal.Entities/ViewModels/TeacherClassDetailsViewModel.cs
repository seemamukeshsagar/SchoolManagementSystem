using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortal.Entities.ViewModels
{
    public class TeacherClassDetailsViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Teacher")]
        public Guid TeacherId { get; set; }
        public List<SelectListItem> Teachers { get; set; } = new();

        [Required]
        [Display(Name = "Class")]
        public Guid ClassId { get; set; }
        public List<SelectListItem> Classes { get; set; } = new();

        [Required]
        [Display(Name = "Section")]
        public Guid SectionId { get; set; }
        public List<SelectListItem> Sections { get; set; } = new();

        [Required]
        [Display(Name = "Subject")]
        public Guid SubjectId { get; set; }
        public List<SelectListItem> Subjects { get; set; } = new();

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        public Guid SchoolId { get; set; }
    }
}
