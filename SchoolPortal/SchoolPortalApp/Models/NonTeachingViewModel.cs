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
    public  string FirstName { get; set; }
    
    [Display(Name = "Middle Name")]
    public string MiddleName { get; set; }
    
    [Display(Name = "Last Name")]
    public  string LastName { get; set; }
    
    [EmailAddress]
    public  string Email { get; set; }
    
    [Phone]
    public  string Phone { get; set; }
    
    [Phone]
    [Display(Name = "Mobile")]
    public  string MobilePhone { get; set; }
    
    public  string Designation { get; set; }
    public  string Department { get; set; }
    public  bool IsActive { get; set; }
    public  bool IsDeleted { get; set; }
    
    [Display(Name = "Employee Code")]
    public  string EmployeeCode { get; set; }
    
    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    public  DateTime? DOB { get; set; }
    
    [DataType(DataType.Date)]
    [Display(Name = "Date of Joining")]
    public  DateTime? DOJ { get; set; }
    
    [DataType(DataType.Date)]
    [Display(Name = "Date of Leaving")]
    public  DateTime? DateOfLeaving { get; set; }
    
    public string Address { get; set; }
    public  Guid? CityId { get; set; }
    public  Guid? StateId { get; set; }
    public  Guid? CountryId { get; set; }
    
    [Display(Name = "ZIP Code")]
    public  string ZipCode { get; set; }
    
    public  string Gender { get; set; }
    public  Guid? MaritalStatusId { get; set; }
    
    [Display(Name = "Profile Image")]
    public IFormFile ImageFile { get; set; }
    public byte[] Image { get; set; }
    
    public  string Qualification { get; set; }
    
    [DataType(DataType.Currency)]
    public  decimal? Salary { get; set; }
    
    [Display(Name = "Bank Account Number")]
    public  string BankAccountNumber { get; set; }
    
    [Display(Name = "Bank Name")]
    public  string BankName { get; set; }
    
    [Display(Name = "IFSC Code")]
    public  string IFSCCode { get; set; }
    
    public  string PAN { get; set; }
    
    [Display(Name = "Aadhaar Number")]
    public  string AadharNumber { get; set; }
    
    [Display(Name = "Emergency Contact Name")]
    public  string EmergencyContactName { get; set; }
    
    [Display(Name = "Emergency Contact Number")]
    public  string EmergencyContactNumber { get; set; }
    
    [Display(Name = "Emergency Contact Relation")]
    public  string EmergencyContactRelation { get; set; }
    
    public  Guid CompanyId { get; set; }
    public  Guid SchoolId { get; set; }
    public  Guid CreatedBy { get; set; }
    public  DateTime CreatedDate { get; set; }
    public  Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    
    // Navigation properties for related data
    public List<NonTeachingDocumentDetails> Documents { get; set; } = new List<NonTeachingDocumentDetails>();
    public List<NonTeachingQualificationDetails> Qualifications { get; set; } = new List<NonTeachingQualificationDetails>();
}
}