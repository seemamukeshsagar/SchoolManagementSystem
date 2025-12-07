#nullable enable

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class VendorViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Vendor Name")]
        public string VendorName { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Address Line 1")]
        public string? Address1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string? Address2 { get; set; }

        [Display(Name = "Country")]
        public Guid CountryId { get; set; }

        [Display(Name = "State")]
        public Guid StateId { get; set; }

        [Display(Name = "City")]
        public Guid CityId { get; set; }

        [Display(Name = "Zip Code")]
        public string? ZipCode { get; set; }

        [Display(Name = "Contact Number")]
        public string? ContactNumber { get; set; }

        [Display(Name = "Mobile Number")]
        public string? MobileNumber { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string? EmailId { get; set; }

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
        public IEnumerable<SelectListItem> Countries { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> States { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Cities { get; set; } = Array.Empty<SelectListItem>();
    }
}