// File: SchoolPortalApp/Models/AcademicYear/AcademicYearViewModel.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.Models.AcademicYear
{
    public class AcademicYearViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Academic Year Name is required")]
        [StringLength(100, ErrorMessage = "Academic Year Name cannot exceed 100 characters")]
        public string AcademicYearName { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "End Date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DateGreaterThan("StartDate", ErrorMessage = "End Date must be greater than Start Date")]
        public DateTime EndDate { get; set; } = DateTime.Today.AddYears(1);

        [Display(Name = "Is Current Year")]
        public bool IsCurrent { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        // For display purposes
        public string Status => IsActive ? "Active" : "Inactive";
    }

    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        
        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null)
                return new ValidationResult($"Unknown property: {_comparisonProperty}");

            var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);
            var currentValue = (DateTime)value;

            if (currentValue <= comparisonValue)
            {
                return new ValidationResult(ErrorMessage ?? $"The {validationContext.DisplayName} must be greater than {_comparisonProperty}");
            }

            return ValidationResult.Success;
        }
    }
}