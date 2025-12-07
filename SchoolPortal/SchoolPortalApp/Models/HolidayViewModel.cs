#nullable enable

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SchoolPortalApp.Models
{
    public class HolidayViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Description { get; set; }

        public Guid TypeId { get; set; }

        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; } = DateTime.UtcNow.Date;

        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; } = DateTime.UtcNow.Date;

        public Guid Year { get; set; }

        public bool? IsStaffApplicable { get; set; }

        public Guid SessionId { get; set; }

        public bool IsActive { get; set; } = true;

        public Guid SchoolId { get; set; }

        public IEnumerable<SelectListItem>? HolidayTypes { get; set; }
        public IEnumerable<SelectListItem>? Sessions { get; set; }
    }

    public class HolidayListItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsActive { get; set; }
    }
}
