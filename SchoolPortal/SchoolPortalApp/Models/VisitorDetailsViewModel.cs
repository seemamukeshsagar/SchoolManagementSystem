#nullable enable

using System;

namespace SchoolPortalApp.Models
{
    public class VisitorDetailsViewModel
    {
        public Guid Id { get; set; }
        public string VehicleNumber { get; set; } = string.Empty;
        public string VehicleName { get; set; } = string.Empty;
        public DateTime DateOfEntry { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public TimeSpan ExitTime { get; set; }
        public string Purpose { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string SchoolName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}