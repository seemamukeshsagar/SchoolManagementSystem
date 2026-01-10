using System;

namespace SchoolPortal.Entities.ViewModels
{
    public class ParentMasterViewModel
    {
        public Guid Id { get; set; }
        public Guid StudentGUID { get; set; }
        public string? StudentName { get; set; }
        public string? ParentFirstName { get; set; }
        public string? ParentLastName { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public DateTime? ParentDOB { get; set; }
        public Guid QualificationId { get; set; }
        public string? Occupation { get; set; }
        public string? FatherOccupation { get; set; }
        public string? MotherOccupation { get; set; }
        public decimal? AnnualIncome { get; set; }
        public Guid DesignationId { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? FatherMobile { get; set; }
        public string? MotherMobile { get; set; }
        public string? Email { get; set; }
        public string? FatherEmail { get; set; }
        public string? MotherEmail { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address { get; set; }
        public Guid CityId { get; set; }
        public Guid StateId { get; set; }
        public Guid CountryId { get; set; }
        public string? ZipCode { get; set; }
        public string? OfficeAddress1 { get; set; }
        public string? OfficeAddress2 { get; set; }
        public Guid OfficeCityId { get; set; }
        public Guid OfficeStateId { get; set; }
        public Guid OfficeCountryId { get; set; }
        public string? OfficeZipCode { get; set; }
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
