using System;

namespace SchoolPortalApp.Models
{
	public class AssessmentListItemViewModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public decimal? PercentageWeightage { get; set; }
		public DateTime? FromPeriod { get; set; }
		public DateTime? ToPeriod { get; set; }
		public bool IsActive { get; set; }
	}
}
