#nullable enable

using System;

namespace SchoolPortalApp.Models
{
	public class FeesCategoryMasterListItemViewModel
	{
		public Guid Id { get; set; }
		public string FeesCatgoryName { get; set; } = string.Empty;
		public string? Description { get; set; }
		public bool IsActive { get; set; }
	}
}
