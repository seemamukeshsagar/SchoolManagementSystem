using System;

namespace SchoolPortalApp.Models
{
    public class TeacherSectionDetailsListItemViewModel
    {
        public Guid Id { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string SectionName { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public bool IsClassTeacher { get; set; }
        public bool IsActive { get; set; }
    }
}