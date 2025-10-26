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

        public IEnumerable<SelectListItem> Schools { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Roll Number")]
        public Guid RollNumber { get; set; }

        public string? Address { get; set; }

        [Display(Name = "City")]
        public Guid CityId { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "State")]
        public Guid StateId { get; set; }
        public IEnumerable<SelectListItem> States { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Country")]
        public Guid CountryId { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; } = Array.Empty<SelectListItem>();

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

        [Display(Name = "Section")]
        public Guid SectionId { get; set; }
        public IEnumerable<SelectListItem> Sections { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Avail Transport")]
        public bool? AvailTransport { get; set; }

        public string? Image { get; set; }
        [Display(Name = "Upload Image")]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Siblings If Any")]
        public bool? SiblingsIfAny { get; set; }

        [Display(Name = "Sibling Class")]
        public Guid? SiblingClassId { get; set; }
        public IEnumerable<SelectListItem> SiblingClasses { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Gender")]
        public Guid? Gender { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Disability (If Any)")]
        public string? DisabilityAny { get; set; }

        [Display(Name = "Medical Allergy (If Any)")]
        public string? MedicalAlleryAny { get; set; }

        [Display(Name = "Birth City")]
        public Guid BirthCityId { get; set; }
        public IEnumerable<SelectListItem> BirthCities { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Birth State")]
        public Guid BirthStateId { get; set; }
        public IEnumerable<SelectListItem> BirthStates { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Birth Country")]
        public Guid BirthCountryId { get; set; }
        public IEnumerable<SelectListItem> BirthCountries { get; set; } = Array.Empty<SelectListItem>();

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
        public bool? UseTransportFees { get; set; }

        [Display(Name = "Session")]
        public Guid? SessionId { get; set; }
        public IEnumerable<SelectListItem> Sessions { get; set; } = Array.Empty<SelectListItem>();

        [Display(Name = "Company")]
        public Guid CompanyId { get; set; }

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
    }
}

