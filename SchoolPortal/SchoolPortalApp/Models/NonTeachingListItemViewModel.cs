#nullable enable

// SchoolPortalApp/Models/NonTeachingListItemViewModel.cs
using System;

namespace SchoolPortalApp.Models
{
    public class NonTeachingListItemViewModel
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string EmployeeCode { get; set; } = string.Empty;
    }
}

