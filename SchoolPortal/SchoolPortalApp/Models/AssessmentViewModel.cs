#nullable enable

using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models
{
	public class AssessmentViewModel
	{
		public Guid Id { get; set; }

		[Required]
		[Display(Name = "Assessment Name")]
		[StringLength(200)]
		public string Name { get; set; } = string.Empty;

		[Display(Name = "Description")]
		[StringLength(1000)]
		public string? Description { get; set; }

		[Display(Name = "Percentage Weightage")]
		[Range(0, 100, ErrorMessage = "Percentage must be between 0 and 100.")]
		public decimal? PercentageWeightage { get; set; }

		[Display(Name = "From Period")]
		[DataType(DataType.Date)]
		public DateTime? FromPeriod { get; set; }

		[Display(Name = "To Period")]
		[DataType(DataType.Date)]
		public DateTime? ToPeriod { get; set; }

		[Display(Name = "Is Active")]
		public bool IsActive { get; set; } = true;
	}
}
