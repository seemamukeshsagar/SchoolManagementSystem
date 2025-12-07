#nullable enable

using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class InventoryMasterViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Inventory Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Item")]
        public Guid ItemId { get; set; }

        [Required]
        [Display(Name = "Location")]
        public Guid LocationId { get; set; }

        [Display(Name = "Quantity")]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Display(Name = "Cost Per Item")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal CostPerItem { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        public Guid CompanyId { get; set; }
        public Guid SchoolId { get; set; }

        public List<SelectListItem> Items { get; set; } = new();
        public List<SelectListItem> Locations { get; set; } = new();
    }
}