using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class SubjectCategoryViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Parent Category")]
        public Guid? ParentId { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public Guid SubjectId { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        [Required]
        [Display(Name = "School")]
        public Guid SchoolId { get; set; }

        public IEnumerable<SelectListItem> Subjects { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Parents { get; set; } = Array.Empty<SelectListItem>();
    }
}
