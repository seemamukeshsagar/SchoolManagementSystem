using System;

namespace SchoolPortal.Entities.ViewModels
{
    public class SystemParameterListItemViewModel
    {
        public Guid Id { get; set; }
        public string ParameterName { get; set; } = string.Empty;
        public string? ParameterValue { get; set; }
        public bool IsActive { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string SchoolName { get; set; } = string.Empty;
    }
}
