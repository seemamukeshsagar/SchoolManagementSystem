using System;

namespace SchoolPortalApp.Models
{
    public class CompanyListItemViewModel
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public string? ZipCode { get; set; }
        public string? EstablishmentYear { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string JurisdictionAreaName { get; set; } = string.Empty;
    }
}
