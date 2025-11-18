using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models
{
    public class TimeTableSetupDetailsViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "School Start Time")]
        [DataType(DataType.Time)]
        public TimeSpan SchoolStartTime { get; set; }

        [Required]
        [Display(Name = "School End Time")]
        [DataType(DataType.Time)]
        public TimeSpan SchoolEndTime { get; set; }

        [Required]
        [Display(Name = "First Period Start Time")]
        [DataType(DataType.Time)]
        public TimeSpan PeriodStartTime { get; set; }

        [Required]
        [Display(Name = "Total Periods")]
        public int TotalPeriods { get; set; }

        [Required]
        [Display(Name = "Period Duration (minutes)")]
        public int PeriodDuration { get; set; }

        [Required]
        [Display(Name = "Recess Duration (minutes)")]
        public int RecessDuration { get; set; }

        [Required]
        [Display(Name = "Recess After Period")]
        public int RecessAfterPeriod { get; set; }

        [Display(Name = "Fruit Recess Duration (minutes)")]
        public int? FruitRecessDuration { get; set; }

        [Display(Name = "Fruit Recess After Period")]
        public int? FruitRecessAfterPeriod { get; set; }

        [Required]
        [Display(Name = "Session Id")]
        public Guid SessionId { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        public Guid SchoolId { get; set; }
    }

    public class TimeTableSetupDetailsListItemViewModel
    {
        public Guid Id { get; set; }
        public TimeSpan SchoolStartTime { get; set; }
        public TimeSpan SchoolEndTime { get; set; }
        public int TotalPeriods { get; set; }
        public int PeriodDuration { get; set; }
        public bool IsActive { get; set; }
    }
}