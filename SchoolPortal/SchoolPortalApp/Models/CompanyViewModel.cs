#nullable enable

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class CompanyViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Display(Name = "Country")]
        public Guid CountryId { get; set; }

        [Display(Name = "State")]
        public Guid StateId { get; set; }

        [Display(Name = "City")]
        public Guid CityId { get; set; }

        [Display(Name = "Zip Code")]
        public string? ZipCode { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Establishment Year")]
        public string? EstablishmentYear { get; set; }

        [Display(Name = "Jurisdiction Area (City)")]
        public Guid JudistrictionArea { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> States { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Cities { get; set; } = Array.Empty<SelectListItem>();
    }
}
