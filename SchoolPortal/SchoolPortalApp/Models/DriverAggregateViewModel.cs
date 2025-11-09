using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolPortal.Entities.Models;

namespace SchoolPortalApp.Models
{
	public class DriverAggregateViewModel
	{
		public DriverMaster Master { get; set; } = new DriverMaster
		{
			IsActive = true,
			IsDeleted = false,
			Status = "INC",
			StatusMessage = "In Process....",
			CreatedDate = DateTime.UtcNow
		};

		public List<DriverDocumentDetails> Documents { get; set; } = new List<DriverDocumentDetails>();
		public List<DriverQualificationDetails> Qualifications { get; set; } = new List<DriverQualificationDetails>();
		public List<IFormFile> DocumentFiles { get; set; } = new List<IFormFile>();

		public IEnumerable<SelectListItem> Countries { get; set; } = Array.Empty<SelectListItem>();
		public IEnumerable<SelectListItem> States { get; set; } = Array.Empty<SelectListItem>();
		public IEnumerable<SelectListItem> Cities { get; set; } = Array.Empty<SelectListItem>();
		public IEnumerable<SelectListItem> QualificationItems { get; set; } = Array.Empty<SelectListItem>();
	}
}
