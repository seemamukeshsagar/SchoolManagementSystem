#nullable enable

using System;

namespace SchoolPortalApp.Models
{
    public class TeacherListItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string SchoolName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public DateTime? DOJ { get; set; }
        public Guid? Gender { get; set; }
        public Guid? MaritalStatusId { get; set; }
        public string Image { get; set; } = string.Empty;
        public string MobilePhone { get; set; } = string.Empty;

        public string GenderName { get; set; } = string.Empty;
        public string MaritalStatusName { get; set; } = string.Empty;
    }
}
