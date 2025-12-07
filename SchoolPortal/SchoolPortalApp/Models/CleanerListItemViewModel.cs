#nullable enable

using System;

namespace SchoolPortalApp.Models
{
    public class CleanerListItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string SchoolName { get; set; } = string.Empty;
    }
}
