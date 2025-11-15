using System;

namespace SchoolPortalApp.Models
{
    public class CategoryMasterListItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
