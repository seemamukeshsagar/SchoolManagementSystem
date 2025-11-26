using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace SchoolPortalApp.Models
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; } = DateTime.UtcNow.Date;

        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Phone")]
        public string? Phone { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        [Required]
        [Display(Name = "School")]
        public Guid SchoolId { get; set; }

        public string? SchoolName { get; set; }

        public IEnumerable<SelectListItem> Schools { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Roll Number")]
        public Guid RollNumber { get; set; }

        public string? Address { get; set; }

        [Display(Name = "City")]
        public Guid CityId { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; } = Array.Empty<SelectListItem>();
        public string? CityName { get; set; }

        [Display(Name = "State")]
        public Guid StateId { get; set; }
        public IEnumerable<SelectListItem> States { get; set; } = Array.Empty<SelectListItem>();
        public string? StateName { get; set; }

        [Display(Name = "Country")]
        public Guid CountryId { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; } = Array.Empty<SelectListItem>();
        public string? CountryName { get; set; }

        [Display(Name = "Zip Code")]
        public string? ZipCode { get; set; }

        [Display(Name = "Contact Number")]
        public string? ContactNumber { get; set; }

        [Display(Name = "Emergency Contact Number")]
        public string? EmergencyContactNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Joining")]
        public DateTime DOJ { get; set; } = DateTime.UtcNow.Date;

        [Display(Name = "Registration Number")]
        public string? RegistrationNumber { get; set; }

        [Display(Name = "Class")]
        public Guid ClassId { get; set; }
        public IEnumerable<SelectListItem> Classes { get; set; } = Array.Empty<SelectListItem>();
        public string? ClassName { get; set; }

        [Display(Name = "Section")]
        public Guid SectionId { get; set; }
        public IEnumerable<SelectListItem> Sections { get; set; } = Array.Empty<SelectListItem>();
        public string? SectionName { get; set; }

        [Display(Name = "Avail Transport")]
        public bool AvailTransport { get; set; } = false;

        public string? Image { get; set; }
        [Display(Name = "Upload Image")]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Array.Empty<SelectListItem>();

        public string? CategoryName { get; set; }

        [Display(Name = "Siblings If Any")]
        public bool? SiblingsIfAny { get; set; }

        [Display(Name = "Sibling Class")]
        public Guid? SiblingClassId { get; set; }
        public IEnumerable<SelectListItem> SiblingClasses { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Gender is required")]
        public Guid? Gender { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; } = new List<SelectListItem>();

        [Display(Name = "Disability (If Any)")]
        public string? DisabilityAny { get; set; }

        [Display(Name = "Medical Allergy (If Any)")]
        public string? MedicalAlleryAny { get; set; }

        [Display(Name = "Birth City")]
        public Guid BirthCityId { get; set; }
        public IEnumerable<SelectListItem> BirthCities { get; set; } = Array.Empty<SelectListItem>();
        public string? BirthCityName { get; set; }

        [Display(Name = "Birth State")]
        public Guid BirthStateId { get; set; }
        public IEnumerable<SelectListItem> BirthStates { get; set; } = Array.Empty<SelectListItem>();
        public string? BirthStateName { get; set; }

        [Display(Name = "Birth Country")]
        public Guid BirthCountryId { get; set; }
        public IEnumerable<SelectListItem> BirthCountries { get; set; } = Array.Empty<SelectListItem>();
        public string? BirthCountryName { get; set; }

        [Display(Name = "Previous School Attended")]
        public string? PreviousSchoolAttended { get; set; }

        [Display(Name = "Previous School Class")]
        public Guid? PreviousSchoolClassId { get; set; }
        public IEnumerable<SelectListItem> PreviousSchoolClasses { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Previous School Percentage")]
        public decimal? PreviousSchoolPercentage { get; set; }

        [Display(Name = "Previous School Rank")]
        public string? PreviousSchoolRank { get; set; }

        [Display(Name = "Previous School Board")]
        public Guid PreviousSchoolBoardId { get; set; }
        public IEnumerable<SelectListItem> PreviousSchoolBoards { get; set; } = Array.Empty<SelectListItem>();

        [DataType(DataType.Date)]
        [Display(Name = "Previous School From Date")]
        public DateTime? PreviousSchoolFromDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Previous School To Date")]
        public DateTime? PreviousSchoolToDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Withdrawn Date")]
        public DateTime? WithdrawnDate { get; set; }

        [Display(Name = "Withdrawn Reason")]
        public string? WithdrawnReason { get; set; }

        [Display(Name = "Blood Group")]
        public Guid BloodGroupId { get; set; }
        public IEnumerable<SelectListItem> BloodGroups { get; set; } = Array.Empty<SelectListItem>();
        public string? BloodGroupName { get; set; }

        [Display(Name = "Nationality")]
        public Guid Nationality { get; set; }
        public IEnumerable<SelectListItem> Nationalities { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Hobbies")]
        public string? Hobbies { get; set; }

        [Display(Name = "Religion")]
        public Guid ReligionId { get; set; }
        public IEnumerable<SelectListItem> Religions { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Route")]
        public Guid? RouteId { get; set; }
        public IEnumerable<SelectListItem> Routes { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Route Stop")]
        public Guid? RouteStopDetailsId { get; set; }
        public IEnumerable<SelectListItem> RouteStops { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Class Teacher")]
        public Guid? ClassTeacherId { get; set; }
        public IEnumerable<SelectListItem> ClassTeachers { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Route Pick And Drop")]
        public bool? RoutePickAndDrop { get; set; }

        [Display(Name = "Fees Discount Category")]
        public Guid? FeesDiscountCategoryMasterId { get; set; }
        public IEnumerable<SelectListItem> FeesDiscountCategories { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Tuition Fees")]
        public decimal? TutionFees { get; set; }

        [Display(Name = "Annual Fees")]
        public decimal? AnnualFees { get; set; }

        [Display(Name = "Transport Fees")]
        public decimal? TransportFees { get; set; }

        [Display(Name = "Use Transport Fees")]
        public bool UseTransportFees { get; set; } = false;

        [Display(Name = "Session")]
        public Guid? SessionId { get; set; }
        public IEnumerable<SelectListItem> Sessions { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Company")]
        public Guid CompanyId { get; set; }
        public string? CompanyName { get; set; }

        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }

        public Guid CreatedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.Date;

        public Guid? ModifiedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }

        public string? Status { get; set; }

        [Display(Name = "Status Message")]
        public string? StatusMessage { get; set; }

        [Display(Name = "House Allotted")]
        public Guid? HouseAllotted { get; set; }
        public IEnumerable<SelectListItem> Houses { get; set; } = Array.Empty<SelectListItem>();

        // Parent fields (single parent)
        [Display(Name = "Parent First Name")]
        public string? ParentFirstName { get; set; }

        [Display(Name = "Parent Last Name")]
        public string? ParentLastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Parent Date of Birth")]
        public DateTime? ParentDOB { get; set; }

        [Display(Name = "Relation Type")]
        public Guid? ParentRelationTypeId { get; set; }
        public IEnumerable<SelectListItem> ParentRelationTypes { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Qualification")]
        public Guid? ParentQualificationId { get; set; }
        public IEnumerable<SelectListItem> ParentQualifications { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Designation")]
        public Guid? ParentDesignationId { get; set; }
        public IEnumerable<SelectListItem> ParentDesignations { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Occupation")]
        public string? ParentOccupation { get; set; }

        [Display(Name = "Annual Income")]
        public decimal? ParentAnnualIncome { get; set; }

        [Display(Name = "Phone")]
        public string? ParentPhone { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string? ParentEmail { get; set; }

        [Display(Name = "Address Line 1")]
        public string? ParentAddress1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string? ParentAddress2 { get; set; }

        [Display(Name = "Country")]
        public Guid? ParentCountryId { get; set; }
        public IEnumerable<SelectListItem> ParentCountries { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "State")]
        public Guid? ParentStateId { get; set; }
        public IEnumerable<SelectListItem> ParentStates { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "City")]
        public Guid? ParentCityId { get; set; }
        public IEnumerable<SelectListItem> ParentCities { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Zip Code")]
        public string? ParentZipCode { get; set; }

        [Display(Name = "Is Active (Parent)")]
        public bool ParentIsActive { get; set; } = true;

        [Display(Name = "Additional Notes")]
        [DataType(DataType.MultilineText)]
        public string AdditionalNotes { get; set; }
    }
}

