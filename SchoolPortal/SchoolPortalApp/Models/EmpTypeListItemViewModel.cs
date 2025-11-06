using System;

namespace SchoolPortalApp.Models
{
    public class EmpTypeListItemViewModel
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string SchoolName { get; set; } = string.Empty;
    }
}

