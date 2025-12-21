using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models.Attendance
{
    public class AttendanceListItemViewModel
    {
        public Guid Id { get; set; } = Guid.Empty;
        
        [Required]
        public Guid EmployeeId { get; set; } = Guid.Empty;
        
        [Display(Name = "Employee")]
        [Required]
        public string EmployeeName { get; set; } = string.Empty;
        
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime AttendanceDate { get; set; } = DateTime.Today;
        
        [Display(Name = "Status")]
        [Required]
        public string Status { get; set; } = "Present";
        
        [Display(Name = "Leave Type")]
        [Required]
        public string LeaveType { get; set; } = "N/A";
        
        [Display(Name = "Half Day")]
        public bool IsHalfDay { get; set; }
        
        [Display(Name = "Time")]
        [Required]
        public string AttendanceTime { get; set; } = DateTime.Now.ToString("HH:mm");
        
        [Display(Name = "Marked")]
        public bool AttendanceMarked { get; set; }
    }
}
