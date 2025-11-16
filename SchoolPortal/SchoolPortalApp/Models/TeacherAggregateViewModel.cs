using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolPortal.Entities.Models;

namespace SchoolPortalApp.Models
{
    public class TeacherAggregateViewModel
    {
        public TeacherMaster Master { get; set; } = new TeacherMaster
        {
            DOB = DateTime.UtcNow.Date,
            IsActive = true,
            IsDeleted = false,
            Status = "INC",
            StatusMessage = "In Process...."
        };

        public List<TeacherDocumentDetails> Documents { get; set; } = new List<TeacherDocumentDetails>();
        public List<TeacherQualificationDetails> Qualifications { get; set; } = new List<TeacherQualificationDetails>();
        public List<IFormFile> DocumentFiles { get; set; } = new List<IFormFile>();
        public IFormFile? ImageFile { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> States { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Cities { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Genders { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> MaritalStatuses { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> QualificationItems { get; set; } = Array.Empty<SelectListItem>();
    }
}
