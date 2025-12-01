using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models.TimeTable
{
    public class TimeTableViewModel
    {
        // Filter properties
        [Required]
        [Display(Name = "Class")]
        public Guid ClassId { get; set; }
        public string ClassName { get; set; }
        public List<SelectListItem> Classes { get; set; } = new List<SelectListItem>();

        [Required]
        [Display(Name = "Section")]
        public Guid SectionId { get; set; }
        public string SectionName { get; set; }
        public List<SelectListItem> Sections { get; set; } = new List<SelectListItem>();

        [Required]
        [Display(Name = "Academic Year")]
        public Guid AcademicYearId { get; set; }
        public string AcademicYearName { get; set; }
        public List<SelectListItem> AcademicYears { get; set; } = new List<SelectListItem>();

        [Display(Name = "Effective From")]
        [DataType(DataType.Date)]
        public DateTime? EffectiveFrom { get; set; }

        [Display(Name = "Effective To")]
        [DataType(DataType.Date)]
        public DateTime? EffectiveTo { get; set; }

        public bool IsActive { get; set; }
        public List<TimeTableDayViewModel> Days { get; set; } = new List<TimeTableDayViewModel>();
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
}