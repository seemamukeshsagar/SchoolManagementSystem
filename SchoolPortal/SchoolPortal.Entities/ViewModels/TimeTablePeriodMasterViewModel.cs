using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models
{
    public class TimeTablePeriodMasterViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Period Number")]
        public string PeriodNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

        [Required]
        [Display(Name = "Session Id")]
        public Guid SessionId { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        public Guid SchoolId { get; set; }
    }

    public class TimeTablePeriodMasterListItemViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string PeriodNumber { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsActive { get; set; }
    }
}