using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class ClassViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Class Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Exam Assessment")]
        public bool? ExamAssessment { get; set; }

        [Display(Name = "Is Grade Point Applicable")]
        public bool? IsGradePointApplicable { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        [Required]
        [Display(Name = "School")]
        public Guid SchoolId { get; set; }

        public IEnumerable<SelectListItem> Schools { get; set; } = Array.Empty<SelectListItem>();
    }
}
