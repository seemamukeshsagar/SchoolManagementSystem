using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models.Attendance
{
    public class StudentAttendanceDetailsViewModel
    {
        public Guid Id { get; set; }
        
        [Required]
        public Guid StudentGUID { get; set; }
        
        [Required]
        public string StudentName { get; set; } = string.Empty;
        
        [Required]
        public Guid ClassId { get; set; }
        
        [Required]
        public string ClassName { get; set; } = string.Empty;
        
        [Required]
        public Guid SectionId { get; set; }
        
        [Required]
        public string SectionName { get; set; } = string.Empty;
        
        public int? Month { get; set; }
        public int? Year { get; set; }
        
        [Display(Name = "Attendance Date")]
        [DataType(DataType.Date)]
        public DateTime AttendenceDate { get; set; } = DateTime.Today;
        
        [Display(Name = "Attendance Status")]
        public bool AttendenceStatus { get; set; }
        
        [Display(Name = "Reason")]
        public Guid AttendanceReasonId { get; set; }
        
        [Required]
        public string AttendanceReason { get; set; } = string.Empty;
        
        [Display(Name = "Time")]
        [Required]
        public string AttendenceTime { get; set; } = DateTime.Now.ToString("HH:mm");
        
        [Required]
        public string Status { get; set; } = "Pending";
        
        [Required]
        public string StatusMessage { get; set; } = "Attendance record created";
    }
}