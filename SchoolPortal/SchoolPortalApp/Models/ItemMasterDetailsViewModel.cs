using System;

namespace SchoolPortalApp.Models
{
    public class ItemMasterDetailsViewModel
    {
        public Guid Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string ItemTypeName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}