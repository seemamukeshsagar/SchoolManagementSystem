using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolPortal.Entities.Models
{
    [Table("NonTeachingQualificationDetails")]
    public class NonTeachingQualificationDetails
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public Guid NonTeachingId { get; set; }
        
        [Required]
        public int QualificationTypeId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Qualification { get; set; }
        
        [StringLength(200)]
        public string BoardUniversity { get; set; }
        
        [StringLength(100)]
        public string Institution { get; set; }
        
        [StringLength(20)]
        public string YearOfPassing { get; set; }
        
        [StringLength(20)]
        public string Percentage { get; set; }
        
        [StringLength(50)]
        public string Division { get; set; }
        
        [StringLength(500)]
        public string DocumentPath { get; set; }
        
        [StringLength(500)]
        public string Remarks { get; set; }
        
        public bool IsActive { get; set; } = true;
        public bool IsVerified { get; set; }
        public string VerifiedBy { get; set; }
        public DateTime? VerifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        [ForeignKey("NonTeachingId")]
        public virtual NonTeachingMaster NonTeaching { get; set; }
    }
}
