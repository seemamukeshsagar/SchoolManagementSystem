using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace SchoolPortalApp.Models.Attendance
{
    public class StudentAttendanceViewModel
    {
        public Guid Id { get; set; }
        
        [Required]
        [Display(Name = "Student")]
        public Guid StudentGUID { get; set; }
        public List<SelectListItem> Students { get; set; } = new List<SelectListItem>();
        
        [Required]
        [Display(Name = "Class")]
        public Guid ClassId { get; set; }
        public List<SelectListItem> Classes { get; set; } = new List<SelectListItem>();
        
        [Required]
        [Display(Name = "Section")]
        public Guid SectionId { get; set; }
        public List<SelectListItem> Sections { get; set; } = new List<SelectListItem>();
        
        [Display(Name = "Month")]
        public int? Month { get; set; }
        
        [Display(Name = "Year")]
        public int? Year { get; set; }
        
        [Required]
        [Display(Name = "Attendance Date")]
        [DataType(DataType.Date)]
        public DateTime AttendenceDate { get; set; } = DateTime.Today;
        
        [Required]
        [Display(Name = "Present")]
        public bool AttendenceStatus { get; set; } = true;
        
        [Display(Name = "Reason")]
        public Guid AttendanceReasonId { get; set; }
        public List<SelectListItem> AttendanceReasons { get; set; } = new List<SelectListItem>();
        
        [Display(Name = "Time")]
        public string AttendenceTime { get; set; }
        
        public string Status { get; set; }
        public string StatusMessage { get; set; }
    }
}