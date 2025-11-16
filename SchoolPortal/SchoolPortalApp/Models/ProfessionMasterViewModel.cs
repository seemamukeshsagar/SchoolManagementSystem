using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models
{
	public class ProfessionMasterViewModel
	{
		public Guid Id { get; set; }

		[Required]
		[Display(Name = "Profession Name")]
		[StringLength(100)]
		public string Name { get; set; } = string.Empty;

		[Display(Name = "Is Active")]
		public bool IsActive { get; set; } = true;
	}
}
