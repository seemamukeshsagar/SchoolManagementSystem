#nullable enable

using System;

namespace SchoolPortalApp.Models
{
    public class DesigMasterListItemViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string SchoolName { get; set; } = string.Empty;
    }
}