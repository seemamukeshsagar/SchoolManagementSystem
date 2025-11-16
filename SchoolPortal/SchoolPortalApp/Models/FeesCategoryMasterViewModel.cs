using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models
{
	public class FeesCategoryMasterViewModel
	{
		public Guid Id { get; set; }

		[Required]
		[Display(Name = "Fees Category Name")]
		[StringLength(100)]
		public string FeesCatgoryName { get; set; } = string.Empty;

		[Display(Name = "Description")]
		[StringLength(500)]
		public string? Description { get; set; }

		[Display(Name = "Is Active")]
		public bool IsActive { get; set; } = true;
	}
}
