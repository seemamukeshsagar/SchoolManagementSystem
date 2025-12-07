#nullable enable

using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models
{
    public class VehicleTypeMasterViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Vehicle Type")]
        public string VehicleType { get; set; } = string.Empty;

        [Display(Name = "Company")]
        public Guid? CompanyId { get; set; }

        [Display(Name = "School")]
        public Guid? SchoolId { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;
    }
}