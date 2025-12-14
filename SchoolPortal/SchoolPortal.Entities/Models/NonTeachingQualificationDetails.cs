using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolPortal.Entities.Models
{
    [Table("NonTeachingQualificationDetails")]
    public class NonTeachingQualificationDetails
    {
        public string? QualificationType;

        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public Guid NonTeachingId { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "Qualification cannot exceed 100 characters")]
        public string Qualification { get; set; } = string.Empty;
        
        public Guid QualificationTypeId { get; set; }
        [StringLength(100, ErrorMessage = "Institution cannot exceed 100 characters")]
        public string? Institution { get; set; }
        
        [StringLength(100, ErrorMessage = "Board/University cannot exceed 100 characters")]
        public string? BoardUniversity { get; set; }
        
        [StringLength(20, ErrorMessage = "Year of passing cannot exceed 20 characters")]
        public string? YearOfPassing { get; set; }
        
        [Range(0, 100, ErrorMessage = "Percentage must be between 0 and 100")]
        public decimal Percentage { get; set; }

        [StringLength(20, ErrorMessage = "Division cannot exceed 20 characters")]
        public string? Division { get; set; }

        [Display(Name="Document Path")]
        [StringLength(500, ErrorMessage = "Document path cannot exceed 500 characters")]
        public string? DocumentPath { get; set; }

        [Display(Name = "Is Verified")]
    public bool IsVerified { get; set; }

    [Display(Name = "Verified By")]
    public Guid? VerifiedBy { get; set; }

    [Display(Name = "Verified On")]
    public DateTime? VerifiedOn { get; set; }

    [StringLength(500, ErrorMessage = "Remarks cannot exceed 500 characters")]
    public string Remarks { get; set; } = string.Empty;
    
    [Display(Name = "Is Active")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "Created By")]
    public Guid CreatedBy { get; set; }

    [Display(Name = "Created Date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [Display(Name = "Modified By")]
    public Guid? ModifiedBy { get; set; }

    [Display(Name = "Modified Date")]
    public DateTime? ModifiedDate { get; set; }
        
        [ForeignKey("NonTeachingId")]
        public virtual NonTeachingMaster? NonTeaching { get; set; }
    }
}
