using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models.Attendance
{
	public class AttendanceDetailsViewModel
	{
		public Guid Id { get; set; }
		public Guid EmployeeId { get; set; }
		public string? EmployeeName { get; set; }
		
		[Display(Name = "Date")]
		[DataType(DataType.Date)]
		public DateTime AttendanceDate { get; set; }
		
		[Display(Name = "Attendance Marked")]
		public bool AttendanceMarked { get; set; }
		
		[Display(Name = "Leave Type")]
		public string? LeaveType { get; set; }
		
		public Guid AttendenceLeaveTypeId { get; set; }
		
		[Display(Name = "Is Half Day")]
		public bool? IsHalfDay { get; set; }
		
		[Display(Name = "Attendance Time")]
		public string? AttendanceTime { get; set; }
		
		[Display(Name = "Status")]
		public string? Status { get; set; }
		
		[Display(Name = "Status Message")]
		public string? StatusMessage { get; set; }
		
		[Display(Name = "Created Date")]
		[DataType(DataType.DateTime)]
		public DateTime CreatedDate { get; set; }
		
		[Display(Name = "Created By")]
		public string? CreatedByName { get; set; }
		
		[Display(Name = "Modified Date")]
		[DataType(DataType.DateTime)]
		public DateTime? ModifiedDate { get; set; }
		
		[Display(Name = "Modified By")]
		public string? ModifiedByName { get; set; }
		
		public Guid? ModifiedBy { get; set; }
		
		// For dropdowns
		public List<SelectListItem> Employees { get; set; } = new List<SelectListItem>();
		public List<SelectListItem> LeaveTypes { get; set; } = new List<SelectListItem>();
	}
}