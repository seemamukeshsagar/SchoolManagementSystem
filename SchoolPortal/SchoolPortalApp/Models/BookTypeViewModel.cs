using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models
{
    public class BookTypeViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Book Type Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;
    }
}
