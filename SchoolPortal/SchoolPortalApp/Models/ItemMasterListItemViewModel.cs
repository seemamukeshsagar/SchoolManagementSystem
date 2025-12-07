#nullable enable

using System;

namespace SchoolPortalApp.Models
{
    public class ItemMasterListItemViewModel
    {
        public Guid Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string ItemTypeName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}