using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class VehicleMasterViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Vehicle Number")]
        public string VehicleNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Vehicle Model")]
        public string VehicleModel { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Vehicle Make")]
        public string VehicleMake { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Vehicle Type")]
        public Guid VehicleTypeId { get; set; }

        [Required]
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; } = string.Empty;

        [Display(Name = "Insurance Company")]
        public string? InsuranceCompany { get; set; }

        [Display(Name = "Insurance Premium")]
        public decimal? InsurancePremium { get; set; }

        [Display(Name = "Seating Capacity")]
        public int? SeatingCapacity { get; set; }

        [Required]
        [Display(Name = "Company")]
        public Guid CompanyId { get; set; }

        [Required]
        [Display(Name = "School")]
        public Guid SchoolId { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        public IEnumerable<SelectListItem> Companies { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Schools { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> VehicleTypes { get; set; } = Array.Empty<SelectListItem>();
    }
}