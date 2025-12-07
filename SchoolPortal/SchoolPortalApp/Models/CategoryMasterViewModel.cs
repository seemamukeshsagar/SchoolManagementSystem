#nullable enable

using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models
{
    public class CategoryMasterViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;
    }
}
