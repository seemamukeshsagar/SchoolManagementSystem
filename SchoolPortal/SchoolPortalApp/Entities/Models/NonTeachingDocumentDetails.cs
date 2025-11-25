using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolPortal.Entities.Models
{
    [Table("NonTeachingDocumentDetails")]
    public class NonTeachingDocumentDetails
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public Guid NonTeachingId { get; set; }
        
        [Required]
        public int DocumentTypeId { get; set; }
        
        [StringLength(100)]
        public string DocumentType { get; set; }
        
        [StringLength(255)]
        public string DocumentNumber { get; set; }
        
        [StringLength(500)]
        public string DocumentPath { get; set; }
        
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        
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
