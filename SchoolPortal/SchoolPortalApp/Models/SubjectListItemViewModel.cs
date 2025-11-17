using System;

namespace SchoolPortalApp.Models
{
    public class SubjectListItemViewModel
    {
        public Guid Id { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public Guid ClassId { get; set; }
        public string? ClassName { get; set; }
        public bool IsScholastic { get; set; }
        public bool IsActive { get; set; }
        public string SchoolName { get; set; } = string.Empty;
    }
}
