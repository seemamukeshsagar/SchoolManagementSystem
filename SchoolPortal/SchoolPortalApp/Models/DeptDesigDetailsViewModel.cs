#nullable enable

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class DeptDesigDetailsViewModel
    {
        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid DesignationId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid SchoolId { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; } = "INC";
        public string StatusMessage { get; set; } = "In Process....";

        // For dropdowns
        public List<SelectListItem> Departments { get; set; } = new();
        public List<SelectListItem> Designations { get; set; } = new();
        public List<SelectListItem> Companies { get; set; } = new();
        public List<SelectListItem> Schools { get; set; } = new();
    }

    public class DeptDesigDetailsListItemViewModel
    {
        public Guid Id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string DesignationName { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string SchoolName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string Status { get; set; } = "INC";
        public string StatusMessage { get; set; } = "In Process....";
    }
}