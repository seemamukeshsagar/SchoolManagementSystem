using System;

namespace SchoolPortal.Entities.ViewModels
{
    public class RegistrationMasterViewModel
    {
        public Guid Id { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? StudentName { get; set; }
        public string? Gender { get; set; }
        public DateTime DOB { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public decimal Age { get; set; }
        public Guid ClassId { get; set; }
        public string? ClassName { get; set; }
        public Guid SectionId { get; set; }
        public string? SectionName { get; set; }
        public DateTime Date { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public Guid SessionId { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address { get; set; }
        public Guid CityId { get; set; }
        public Guid StateId { get; set; }
        public Guid CountryId { get; set; }
        public string? ZipCode { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? ParentName { get; set; }
        public string? ParentMobile { get; set; }
        public string? PreviousSchool { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Guid CompanyId { get; set; }
        public Guid SchoolId { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Status { get; set; }
        public string? StatusMessage { get; set; }
    }
}
