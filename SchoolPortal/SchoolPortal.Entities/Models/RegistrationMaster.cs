#nullable disable
using System;
using System.Collections.Generic;

namespace SchoolPortal.Entities.Models;

public partial class RegistrationMaster
{
    public Guid Id { get; set; }

    public string RegistrationNumber { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime DOB { get; set; }

    public decimal Age { get; set; }

    public Guid ClassId { get; set; }

    public DateTime Date { get; set; }

    public Guid SessionId { get; set; }

    public string Address1 { get; set; }

    public string Address2 { get; set; }

    public Guid CityId { get; set; }

    public Guid StateId { get; set; }

    public Guid CountryId { get; set; }

    public string ZipCode { get; set; }

    public string ContactNumber { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public Guid ReligionId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid Gender { get; set; }

    public Guid BloodGroupId { get; set; }

    public decimal? RegistrationFees { get; set; }

    public bool? RegistrationFeesPaid { get; set; }

    public string SiblingsIfAny { get; set; }

    public Guid SiblingClassMasterId { get; set; }

    public string FathersFirstName { get; set; }

    public string FathersLastName { get; set; }

    public DateTime? FathersDOB { get; set; }

    public Guid FathersQualificationId { get; set; }

    public string FathersOccupation { get; set; }

    public Guid FathersDesignationId { get; set; }

    public decimal? FathersAnnualIncome { get; set; }

    public string MothersFirstName { get; set; }

    public string MothersLastName { get; set; }

    public DateTime? MothersDOB { get; set; }

    public Guid MothersQualificationId { get; set; }

    public string MothersOccupation { get; set; }

    public Guid MothersDesignationId { get; set; }

    public decimal? MothersAnnualIncome { get; set; }

    public string BirthCertificate { get; set; }

    public bool? BirthCertificateSubmitted { get; set; }

    public string TransferCertificate { get; set; }

    public bool? TransferCertificateSubmitted { get; set; }

    public string ReportCardLastClass { get; set; }

    public bool? ReportCardSubmitted { get; set; }

    public string OtherCertificateName { get; set; }

    public bool? OtherCertificateSubmitted { get; set; }

    public string OtherDescription { get; set; }

    public bool? IsAdmissionConfirmed { get; set; }

    public Guid? StudentGuid { get; set; }

    public Guid CompanyId { get; set; }

    public Guid SchoolId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string Status { get; set; } = "INC";

    public string StatusMessage { get; set; } = "In Process....";
}