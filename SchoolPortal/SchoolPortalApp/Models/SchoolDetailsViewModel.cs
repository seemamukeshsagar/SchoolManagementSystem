using System;

namespace SchoolPortalApp.Models
{
    public class SchoolDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public string? ZipCode { get; set; }
        public string? Phone { get; set; }
        public string? EstablishmentYear { get; set; }
        public string? Mobile { get; set; }
        public string JudistrictionCityName { get; set; } = string.Empty;
        public string JudistrictionStateName { get; set; } = string.Empty;
        public string JudistrictionCountryName { get; set; } = string.Empty;
    }
}
