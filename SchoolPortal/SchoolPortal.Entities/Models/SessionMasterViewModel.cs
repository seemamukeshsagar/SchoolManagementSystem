using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models
{
    public class SessionMasterViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Session Name / Value")]
        [StringLength(100)]
        public string Value { get; set; } = string.Empty;

        [Display(Name = "Description")]
        [StringLength(250)]
        public string? Description { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        public Guid SchoolId { get; set; }
    }

    public class SessionMasterListItemViewModel
    {
        public Guid Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}