using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models
{
    public class TimeTableViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Class")]
        public Guid ClassId { get; set; }
        public string ClassName { get; set; }

        [Required]
        [Display(Name = "Section")]
        public Guid SectionId { get; set; }
        public string SectionName { get; set; }

        [Required]
        [Display(Name = "Academic Year")]
        public Guid AcademicYearId { get; set; }
        public string AcademicYearName { get; set; }

        [Display(Name = "Effective From")]
        [DataType(DataType.Date)]
        public DateTime? EffectiveFrom { get; set; }

        [Display(Name = "Effective To")]
        [DataType(DataType.Date)]
        public DateTime? EffectiveTo { get; set; }

        public bool IsActive { get; set; }
        public List<TimeTableDayViewModel> Days { get; set; } = new List<TimeTableDayViewModel>();

        // Add these properties for dropdowns
        public IEnumerable<SelectListItem> Classes { get; set; }
        public IEnumerable<SelectListItem> Sections { get; set; }
        public IEnumerable<SelectListItem> AcademicYears { get; set; }
    }

    public class TimeTableDayViewModel
    {
        public int DayId { get; set; }
        public string DayName { get; set; }
        public List<TimeTablePeriodViewModel> Periods { get; set; } = new List<TimeTablePeriodViewModel>();
    }

    public class TimeTablePeriodViewModel
    {
        public Guid Id { get; set; }
        public int PeriodNumber { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Guid? SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Guid? TeacherId { get; set; }
        public string TeacherName { get; set; }
        public bool IsBreak { get; set; }
        public string BreakName { get; set; }
    }

    public class TimeTableFilterViewModel
    {
        public Guid? ClassId { get; set; }
        public Guid? SectionId { get; set; }
        public Guid? AcademicYearId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
