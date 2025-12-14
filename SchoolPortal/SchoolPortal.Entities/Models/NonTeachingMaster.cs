using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolPortal.Entities.Models
{
    [Table("NonTeachingMaster")]
    public class NonTeachingMaster
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; } = string.Empty;

        [StringLength(100)]
        public string? LastName { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(20)]
        public string? MobilePhone { get; set; }

        [StringLength(100)]
        public string? Designation { get; set; }

        [StringLength(100)]
        public string? Department { get; set; }

        public bool IsActive { get; set; }

        [StringLength(50)]
        public string? EmployeeCode { get; set; }

        public DateTime? DOB { get; set; }
        public DateTime? DOJ { get; set; }
        public DateTime? DateOfLeaving { get; set; }
        
        [StringLength(500)]
        public string? Address { get; set; }
        
        public Guid? CityId { get; set; }
        public Guid? StateId { get; set; }
        public Guid? CountryId { get; set; }
        
        [StringLength(20)]
        public string? ZipCode { get; set; }
        
        [StringLength(10)]
        public string? Gender { get; set; }
        
        public Guid? MaritalStatusId { get; set; }
        
        [StringLength(500)]
        public byte[]? Image { get; set; }
        
        [StringLength(200)]
        public string? Qualification { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Salary { get; set; }
        
        [StringLength(50)]
        public string? BankAccountNumber { get; set; }
        
        [StringLength(100)]
        public string? BankName { get; set; }
        
        [StringLength(20)]
        public string? IFSCCode { get; set; }
        
        [StringLength(20)]
        public string? PAN { get; set; }
        
        [StringLength(20)]
        public string? AadharNumber { get; set; }
        
        [StringLength(100)]
        public string? EmergencyContactName { get; set; }
        
        [StringLength(20)]
        public string? EmergencyContactNumber { get; set; }
        
        [StringLength(50)]
        public string? EmergencyContactRelation { get; set; }

        public Guid CompanyId { get; set; }
        public Guid SchoolId { get; set; }

        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ModifiedOn { get; set; }

         [Required]
        public bool IsDeleted { get; set; } = false;


        // Navigation properties
        public virtual List<NonTeachingDocumentDetails> Documents { get; set; } = new List<NonTeachingDocumentDetails>();
        public virtual List<NonTeachingQualificationDetails> Qualifications { get; set; } = new List<NonTeachingQualificationDetails>();

    }
}
