using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class VehicleExpenseDetailsViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Vehicle")]
        public Guid VehicleId { get; set; }

        [Required]
        [Display(Name = "Vehicle Type")]
        public Guid VehicleTypeId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Expense Date")]
        [DataType(DataType.Date)]
        public DateTime? ExpenseDate { get; set; }

        [Display(Name = "Expense Amount")]
        public decimal? ExpenseAmount { get; set; }

        [Required]
        [Display(Name = "Company")]
        public Guid CompanyId { get; set; }

        [Required]
        [Display(Name = "School")]
        public Guid SchoolId { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        public IEnumerable<SelectListItem> Vehicles { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> VehicleTypes { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Companies { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Schools { get; set; } = Array.Empty<SelectListItem>();
    }
}