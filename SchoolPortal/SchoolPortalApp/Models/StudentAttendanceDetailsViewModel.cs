using System;
using System.ComponentModel.DataAnnotations;
namespace SchoolPortalApp.Models.Attendance
{
    public class StudentAttendanceDetailsViewModel
    {
        public Guid Id { get; set; }
        public Guid StudentGUID { get; set; }
        public string StudentName { get; set; }
        public Guid ClassId { get; set; }
        public string ClassName { get; set; }
        public Guid SectionId { get; set; }
        public string SectionName { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        
        [Display(Name = "Attendance Date")]
        [DataType(DataType.Date)]
        public DateTime AttendenceDate { get; set; }
        
        [Display(Name = "Attendance Status")]
        public bool AttendenceStatus { get; set; }
        
        [Display(Name = "Reason")]
        public Guid AttendanceReasonId { get; set; }
        public string AttendanceReason { get; set; }
        
        [Display(Name = "Time")]
        public string AttendenceTime { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }
    }
}