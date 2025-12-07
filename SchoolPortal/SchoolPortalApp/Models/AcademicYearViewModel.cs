using System;

namespace SchoolPortalApp.Models
{
	public class AcademicYearViewModel
	{
		public Guid Id { get; set; }
		public required string AcademicYearName { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool IsCurrent { get; set; }
		public bool IsActive { get; set; }
		public string? Status { get; set; }
	}
}