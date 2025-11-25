// SchoolPortal.Web/Models/NonTeaching/NonTeachingViewModel.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Web.Models.NonTeaching
{
    public class NonTeachingViewModel
{
    public Guid Id { get; set; }
    
    [Display(Name = "First Name")]
    [Required(ErrorMessage = "First Name is required")]
    public  string FirstName { get; set; }
    
    [Display(Name = "Middle Name")]
    public string MiddleName { get; set; }
    
    [Display(Name = "Last Name")]
    [Required(ErrorMessage = "Last Name is required")]
    public  string LastName { get; set; }
    
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public  string Email { get; set; }
    
    [Phone]
    [Required(ErrorMessage = "Phone is required")]
    public  string Phone { get; set; }
    
    [Phone]
    [Display(Name = "Mobile")]
    [Required(ErrorMessage = "Mobile phone is required")]
    public  string MobilePhone { get; set; }
    
    [Required(ErrorMessage = "Designation is required")]
    public  string Designation { get; set; }

    [Required(ErrorMessage = "Department is required")]
    public  string Department { get; set; }

    [Required(ErrorMessage = "Qualification is required")]
    public  string Qualification { get; set; }
    public  bool IsActive { get; set; }
    public  bool IsDeleted { get; set; }
    
    [Display(Name = "Employee Code")]
    [Required(ErrorMessage = "Employee code is required")]
    public  string EmployeeCode { get; set; }
    
    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    [Required(ErrorMessage = "Date of birth is required")]
    public  DateTime? DOB { get; set; }
    
    [DataType(DataType.Date)]
    [Display(Name = "Date of Joining")]
    [Required(ErrorMessage = "Date of joining is required")]
    public  DateTime? DOJ { get; set; }
    
    [DataType(DataType.Date)]
    [Display(Name = "Date of Leaving")]
    public  DateTime? DateOfLeaving { get; set; }
    
    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }

    [Required(ErrorMessage = "City is required")]
    public  Guid? CityId { get; set; }

    [Required(ErrorMessage = "State is required")]
    public  Guid? StateId { get; set; }

    [Required(ErrorMessage = "Country is required")]
    public  Guid? CountryId { get; set; }
    
    [Display(Name = "ZIP Code")]
    [Required(ErrorMessage = "ZIP code is required")]
    public  string ZipCode { get; set; }
    
    [Required(ErrorMessage = "Gender is required")]
    public  string Gender { get; set; }
    public  Guid? MaritalStatusId { get; set; }
    
    [Display(Name = "Profile Image")]
    public IFormFile ImageFile { get; set; }
    public byte[] Image { get; set; }
    
    
    [DataType(DataType.Currency)]
    public  decimal? Salary { get; set; }
    
    [Display(Name = "Bank Account Number")]
    [Required(ErrorMessage = "Bank account number is required")]
    public  string BankAccountNumber { get; set; }
    
    [Display(Name = "Bank Name")]
    [Required(ErrorMessage = "Bank name is required")]
    public  string BankName { get; set; }
    
    [Display(Name = "IFSC Code")]
    [Required(ErrorMessage = "IFSC code is required")]
    public  string IFSCCode { get; set; }
    
    public  string PAN { get; set; }
    
    [Display(Name = "Aadhaar Number")]
    [Required(ErrorMessage = "Aadhaar number is required")]
    public  string AadharNumber { get; set; }
    
    [Display(Name = "Emergency Contact Name")]
    [Required(ErrorMessage = "Emergency contact name is required")]
    public  string EmergencyContactName { get; set; }
    
    [Display(Name = "Emergency Contact Number")]
    public  string EmergencyContactNumber { get; set; }
    
    [Display(Name = "Emergency Contact Relation")]
    public  string EmergencyContactRelation { get; set; }
    
    public  Guid CompanyId { get; set; }
    public  Guid SchoolId { get; set; }
    public  Guid CreatedBy { get; set; } 
    public  DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public  Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;
    
    // Navigation properties for related data
    public List<NonTeachingDocumentDetails> Documents { get; set; } = new List<NonTeachingDocumentDetails>();
    public List<NonTeachingQualificationDetails> Qualifications { get; set; } = new List<NonTeachingQualificationDetails>();
}
}