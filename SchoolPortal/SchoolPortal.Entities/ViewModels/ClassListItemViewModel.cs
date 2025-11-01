using System;

namespace SchoolPortal.Entities.ViewModels
{
    public class ClassListItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ExamAssessment { get; set; }
        public bool IsActive { get; set; }
        public string SchoolName { get; set; } = string.Empty;
    }
}
