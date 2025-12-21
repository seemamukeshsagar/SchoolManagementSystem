using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models.Attendance
{
    public class StudentAttendanceListItemViewModel
    {
        public Guid Id { get; set; }
        
        [Required]
        public string StudentName { get; set; } = string.Empty;
        
        [Required]
        public string ClassName { get; set; } = string.Empty;
        
        [Required]
        public string SectionName { get; set; } = string.Empty;
        
        public DateTime AttendenceDate { get; set; }
        
        public bool AttendenceStatus { get; set; }
        
        [Required]
        public string Status { get; set; } = "Pending";
    }
}