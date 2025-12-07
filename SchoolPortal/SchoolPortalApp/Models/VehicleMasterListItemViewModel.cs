#nullable enable

using System;

namespace SchoolPortalApp.Models
{
    public class VehicleMasterListItemViewModel
    {
        public Guid Id { get; set; }
        public string VehicleNumber { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;
        public string VehicleMake { get; set; } = string.Empty;
        public string VehicleTypeName { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string SchoolName { get; set; } = string.Empty;
    }
}