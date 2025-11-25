using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolPortal.Entities.Models
{
    [Table("NonTeachingQualificationDetails")]
    public class NonTeachingQualificationDetails
    {
        public object QualificationType;

        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public Guid NonTeachingId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Qualification { get; set; }
        
        public Guid QualificationTypeId { get; set; }
        [StringLength(100)]
        public string Institution { get; set; }
        
        [StringLength(100)]
        public string BoardUniversity { get; set; }
        
        [StringLength(20)]
        public string YearOfPassing { get; set; }
        
        public decimal Percentage { get; set; }

        public string Division { get; set; }
        public string DocumentPath { get; set; }

        public bool IsVerified { get; set; }
        public Guid VerifiedBy { get; set; }

        public DateTime VerifiedOn { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        
        public bool IsActive { get; set; } = true;
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        [ForeignKey("NonTeachingId")]
        public virtual NonTeachingMaster NonTeaching { get; set; }
    }
}
