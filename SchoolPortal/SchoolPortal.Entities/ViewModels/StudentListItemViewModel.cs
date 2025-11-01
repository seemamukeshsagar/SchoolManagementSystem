using System;

namespace SchoolPortal.Entities.ViewModels
{
    public class StudentListItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public string SchoolName { get; set; } = string.Empty;
    }
}
