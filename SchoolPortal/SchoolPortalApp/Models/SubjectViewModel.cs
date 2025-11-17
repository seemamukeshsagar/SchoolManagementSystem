using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
	public class SubjectViewModel
	{
		public Guid Id { get; set; }

		[Required]
		[Display(Name = "Subject Name")]
		public string SubjectName { get; set; } = string.Empty;

		[Required]
		[Display(Name = "Class")]
		public Guid ClassId { get; set; }

		[Display(Name = "Is Scholastic")]
		public bool IsScholastic { get; set; } = false;

		[Display(Name = "Is Active")]
		public bool IsActive { get; set; } = true;

		[Required]
		[Display(Name = "School")]
		public Guid SchoolId { get; set; }

		public IEnumerable<SelectListItem> Classes { get; set; } = Array.Empty<SelectListItem>();

		public IEnumerable<SelectListItem> Schools { get; set; } = Array.Empty<SelectListItem>();
	}
}
