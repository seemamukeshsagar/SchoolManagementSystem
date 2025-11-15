using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Models
{
    public class EmpViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "DOB is required.")]
        public DateTime DOB { get; set; } = DateTime.UtcNow.Date;
        public DateTime? DOJ { get; set; }
        public DateTime? ProbationStartDate { get; set; }
        public int? ProbationPeriod { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public string PANNumber { get; set; } = string.Empty;
        public string ESICNumber { get; set; } = string.Empty;
        public string PFNumeber { get; set; } = string.Empty;

        public string CurrentAddress1 { get; set; } = string.Empty;
        public string CurrentAddress2 { get; set; } = string.Empty;
        public Guid? CurrentCityId { get; set; }
        public Guid? CurrentStateId { get; set; }
        public Guid? CurrentCountryId { get; set; }
        public string CurrentZipCode { get; set; } = string.Empty;

        public string PermanentAddress1 { get; set; } = string.Empty;
        public string PermanentAddress2 { get; set; } = string.Empty;
        public Guid? PermanentCityId { get; set; }
        public Guid? PermanentStateId { get; set; }
        public Guid? PermanentCountryId { get; set; }
        public string PermanentZipCode { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;

        public Guid? DepartmentId { get; set; }
        public Guid? DesignationId { get; set; }
        public Guid? PaymentModeId { get; set; }
        public Guid? EmployeeTypeId { get; set; }
        public Guid? CategoryId { get; set; }
        public string BankAccountNumber { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public Guid? GenderId { get; set; }
        public Guid? BloodGroupId { get; set; }
        public Guid? GradeId { get; set; }
        public string Image { get; set; } = string.Empty;
        public Guid? EmployeeOldId { get; set; }
        public string FathersName { get; set; } = string.Empty;
        public string MothersName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LicenceNumber { get; set; } = string.Empty;
        public DateTime? LicenceIssueDate { get; set; }
        public DateTime? LicenceValidUpto { get; set; }
        public string LicenceDescription { get; set; } = string.Empty;
        public string LicenceImage { get; set; } = string.Empty;
        public string LicenceType { get; set; } = string.Empty;
        public string Salutation { get; set; } = string.Empty;
        public DateTime? DateOfLeaving { get; set; }
        public string MaritalStatus { get; set; } = string.Empty;
        public string YearsOfExperience { get; set; } = string.Empty;
        public string PrevioudSchoolCompany { get; set; } = string.Empty;
        public string AadhaarNumber { get; set; } = string.Empty;
        public int? MathUpToClass { get; set; }
        public int? EnglishUptoClass { get; set; }
        public int? SSTUptoClass { get; set; }

        public Guid CompanyId { get; set; }
        public Guid SchoolId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        public string Status { get; set; } = "Active";
        public string StatusMessage { get; set; } = string.Empty;

        // Dropdowns
        public List<SelectListItem> Departments { get; set; } = new();
        public List<SelectListItem> Designations { get; set; } = new();
        public List<SelectListItem> Genders { get; set; } = new();

        public List<SelectListItem> PaymentModes { get; set; } = new();
        public List<SelectListItem> EmployeeTypes { get; set; } = new();
        public List<SelectListItem> EmployeeCategories { get; set; } = new();
        public List<SelectListItem> Grades { get; set; } = new();
        public List<SelectListItem> BloodGroups { get; set; } = new();
        public List<SelectListItem> MaritalStatuses { get; set; } = new();

        public List<SelectListItem> CurrentCountries { get; set; } = new();
        public List<SelectListItem> CurrentStates { get; set; } = new();
        public List<SelectListItem> CurrentCities { get; set; } = new();

        public List<SelectListItem> PermanentCountries { get; set; } = new();
        public List<SelectListItem> PermanentStates { get; set; } = new();
        public List<SelectListItem> PermanentCities { get; set; } = new();

        public List<SelectListItem> LicenceTypes { get; set; } = new();

        // Uploads
        public IFormFile? ImageFile { get; set; }
        public IFormFile? LicenceImageFile { get; set; }
    }

    public class EmpListItemViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string DesignationName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    public class EmpDetailsViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string DesignationName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string Status { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
}
