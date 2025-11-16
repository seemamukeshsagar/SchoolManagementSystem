using System;

namespace SchoolPortalApp.Models
{
	public class QualificationMasterListItemViewModel
	{
		public Guid Id { get; set; }
		public string QualificationName { get; set; } = string.Empty;
		public bool IsTeachingQualification { get; set; }
		public bool IsActive { get; set; }
	}
}
