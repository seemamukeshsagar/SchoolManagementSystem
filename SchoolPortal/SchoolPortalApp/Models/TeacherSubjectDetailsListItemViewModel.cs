#nullable enable

using System;

namespace SchoolPortalApp.Models
{
    public class TeacherSubjectDetailsListItemViewModel
    {
        public Guid Id { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
