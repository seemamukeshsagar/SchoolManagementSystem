#nullable enable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class ItemMasterViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Item Type")]
        [Required]
        public Guid ItemTypeMasterId { get; set; }

        public bool IsActive { get; set; } = true;

        public Guid CompanyId { get; set; }
        public Guid SchoolId { get; set; }

        public IEnumerable<SelectListItem> ItemTypes { get; set; } = Array.Empty<SelectListItem>();
    }
}