using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models.Attendance
{
    public class AttendanceListItemViewModel
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        
        [Display(Name = "Employee")]
        public string EmployeeName { get; set; }
        
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime AttendanceDate { get; set; }
        
        [Display(Name = "Status")]
        public string Status { get; set; }
        
        [Display(Name = "Leave Type")]
        public string LeaveType { get; set; }
        
        [Display(Name = "Half Day")]
        public bool? IsHalfDay { get; set; }
        
        [Display(Name = "Time")]
        public string AttendanceTime { get; set; }
        
        [Display(Name = "Marked")]
        public bool AttendanceMarked { get; set; }
    }
}
