using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolPortal.Entities.Models;

namespace SchoolPortalApp.Models
{
	public class CleanerAggregateViewModel
	{
		public CleanerMaster Master { get; set; } = new CleanerMaster
		{
			IsActive = true,
			IsDeleted = false,
			Status = "INC",
			StatusMessage = "In Process....",
			CreatedDate = DateTime.UtcNow
		};

		public List<CleanerDocumentDetails> Documents { get; set; } = new List<CleanerDocumentDetails>();
		public List<CleanerQualificationDetails> Qualifications { get; set; } = new List<CleanerQualificationDetails>();
		public List<IFormFile> DocumentFiles { get; set; } = new List<IFormFile>();
		public IFormFile? ImageFile { get; set; }

		public IEnumerable<SelectListItem> QualificationItems { get; set; } = Array.Empty<SelectListItem>();
	}
}
