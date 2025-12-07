#nullable enable

using System;

namespace SchoolPortalApp.Models
{
    public class CategoryMasterDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string Status { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
}
