#nullable enable

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class VisitorViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Vehicle Number")]
        public string VehicleNumber { get; set; } = string.Empty;

        [Display(Name = "Vehicle Name")]
        public string? VehicleName { get; set; }

        [Display(Name = "Date Of Entry")]
        [DataType(DataType.Date)]
        public DateTime DateOfEntry { get; set; } = DateTime.Today;

        [Display(Name = "Arrival Time")]
        [DataType(DataType.Time)]
        public TimeSpan ArrivalTime { get; set; } = DateTime.Now.TimeOfDay;

        [Display(Name = "Exit Time")]
        [DataType(DataType.Time)]
        public TimeSpan ExitTime { get; set; } = DateTime.Now.TimeOfDay;

        [Display(Name = "Purpose")]
        public string? Purpose { get; set; }

        [Display(Name = "Contact Person")]
        public string? ContactPerson { get; set; }

        [Display(Name = "Address 1")]
        public string? Address1 { get; set; }

        [Display(Name = "Address 2")]
        public string? Address2 { get; set; }

        [Display(Name = "Country")]
        public Guid CountryId { get; set; }

        [Display(Name = "State")]
        public Guid StateId { get; set; }

        [Display(Name = "City")]
        public Guid CityId { get; set; }

        [Display(Name = "Zip Code")]
        public string? ZipCode { get; set; }

        [Display(Name = "Company")]
        public Guid? CompanyId { get; set; }

        [Display(Name = "School")]
        public Guid? SchoolId { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        public IEnumerable<SelectListItem> Countries { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> States { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Cities { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Companies { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Schools { get; set; } = Array.Empty<SelectListItem>();
    }
}