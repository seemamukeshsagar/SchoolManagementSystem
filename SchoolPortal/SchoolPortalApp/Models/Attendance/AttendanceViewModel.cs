using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models.Attendance
{
    public class AttendanceViewModel
    {
        public Guid Id { get; set; } = Guid.Empty;
        
        [Display(Name = "Employee")]
        [Required(ErrorMessage = "Employee is required")]
        public Guid EmployeeId { get; set; } = Guid.Empty;
        
        [Display(Name = "Attendance Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Attendance date is required")]
        public DateTime AttendanceDate { get; set; } = DateTime.Today;
        
        [Display(Name = "Mark Attendance")]
        public bool AttendanceMarked { get; set; } = true;
        
        [Display(Name = "Leave Type")]
        public Guid? LeaveTypeId { get; set; }
        
        [Display(Name = "Is Half Day")]
        public bool IsHalfDay { get; set; }
        
        [Display(Name = "Attendance Time")]
        [Required]
        public string AttendanceTime { get; set; } = DateTime.Now.ToString("HH:mm");
        
        // For dropdowns
        public List<SelectListItem> Employees { get; set; } = new();
        public List<SelectListItem> LeaveTypes { get; set; } = new();
    }
}
