using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models
{
    public class BookCategoryViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;
    }
}