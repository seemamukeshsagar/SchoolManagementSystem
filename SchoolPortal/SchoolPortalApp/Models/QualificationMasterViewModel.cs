#nullable enable

using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models
{
	public class QualificationMasterViewModel
	{
		public Guid Id { get; set; }

		[Required]
		[Display(Name = "Qualification Name")]
		[StringLength(100)]
		public string QualificationName { get; set; } = string.Empty;

		[Display(Name = "Is Teaching Qualification")]
		public bool IsTeachingQualification { get; set; }

		[Display(Name = "Is Active")]
		public bool IsActive { get; set; } = true;
	}
}
