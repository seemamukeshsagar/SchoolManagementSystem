#nullable enable

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class ClassRoomViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Class Room Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        [Required]
        [Display(Name = "School")]
        public Guid SchoolId { get; set; }

        public IEnumerable<SelectListItem> Schools { get; set; } = Array.Empty<SelectListItem>();
    }
}
