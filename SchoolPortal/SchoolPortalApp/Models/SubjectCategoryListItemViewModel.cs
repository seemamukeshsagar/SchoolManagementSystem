using System;

namespace SchoolPortalApp.Models
{
    public class SubjectCategoryListItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string SchoolName { get; set; } = string.Empty;
    }
}
