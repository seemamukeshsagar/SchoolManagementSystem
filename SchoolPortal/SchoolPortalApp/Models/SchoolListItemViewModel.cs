#nullable enable

using System;

namespace SchoolPortalApp.Models
{
    public class SchoolListItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? EstablishmentYear { get; set; }
        public bool HasContact { get; set; }
    }
}
