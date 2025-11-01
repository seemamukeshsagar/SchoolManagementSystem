using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SSchoolPortal.Entities.ViewModels
{
    public class SchoolContactViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "School")]
        [Required]
        public Guid SchoolId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Phone")]
        public string? Phone { get; set; }

        [Display(Name = "Mobile Phone")]
        public string? MobilePhone { get; set; }

        [Display(Name = "Address Line 1")]
        public string? AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string? AddressLine2 { get; set; }

        [Display(Name = "Country")]
        public Guid CountryId { get; set; }

        [Display(Name = "State")]
        public Guid StateId { get; set; }

        [Display(Name = "City")]
        public Guid CityId { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        public IEnumerable<SelectListItem> Schools { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Countries { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> States { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Cities { get; set; } = Array.Empty<SelectListItem>();
    }
}
