#nullable enable

using System;

namespace SchoolPortalApp.Models
{
    public class SupplierListItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? EmailId { get; set; }
        public bool IsActive { get; set; }
        public string? ZipCode { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string SchoolName { get; set; } = string.Empty;
    }
}