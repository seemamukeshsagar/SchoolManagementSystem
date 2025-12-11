using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SchoolPortalApp.Models
{
    public class StudentImportModel
    {
        [Required(ErrorMessage = "Please select an Excel file")]
        [Display(Name = "Excel File")]
        public IFormFile ExcelFile { get; set; }
    }

    public class StudentImportResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int TotalRecords { get; set; }
        public int SuccessCount { get; set; }
        public int ErrorCount { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
