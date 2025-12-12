using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models.Attendance
{
    public class AttendanceViewModel
    {
        public Guid Id { get; set; }
        
        [Display(Name = "Employee")]
        [Required(ErrorMessage = "Employee is required")]
        public Guid EmployeeId { get; set; }
        
        [Display(Name = "Attendance Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Attendance date is required")]
        public DateTime AttendanceDate { get; set; }
        
        [Display(Name = "Mark Attendance")]
        public bool AttendanceMarked { get; set; }
        
        [Display(Name = "Leave Type")]
        public Guid? LeaveTypeId { get; set; }
        
        [Display(Name = "Is Half Day")]
        public bool? IsHalfDay { get; set; }
        
        [Display(Name = "Attendance Time")]
        public string AttendanceTime { get; set; }
        
        // For dropdowns
        public List<SelectListItem> Employees { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> LeaveTypes { get; set; } = new List<SelectListItem>();
    }
}
