#nullable enable

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models
{
    public class DeptImportViewModel
    {
        [Required(ErrorMessage = "Please select an Excel file")]
        [Display(Name = "Excel File")]
        public IFormFile? ExcelFile { get; set; }
    }
}