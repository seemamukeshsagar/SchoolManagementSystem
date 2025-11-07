using System;

namespace SchoolPortalApp.Models
{
    public class VisitorListItemViewModel
    {
        public Guid Id { get; set; }
        public string VehicleNumber { get; set; } = string.Empty;
        public string VehicleName { get; set; } = string.Empty;
        public DateTime DateOfEntry { get; set; }
        public string Purpose { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}