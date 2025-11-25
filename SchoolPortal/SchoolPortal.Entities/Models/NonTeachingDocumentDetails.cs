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
         [StringLength(100, ErrorMessage = "Document type cannot exceed 100 characters")]
        [Display(Name = "Document Type")]
        public string DocumentType { get; set; }

        public Guid DocumentTypeId { get; set; }

         [Display(Name = "Document Number")]
       [StringLength(255, ErrorMessage = "Document number cannot exceed 255 characters")]
        public string DocumentNumber { get; set; }
        
         [Display(Name = "Document Path")]
         [Required]
         [StringLength(500, ErrorMessage = "Document path cannot exceed 500 characters")]
        public string DocumentPath { get; set; }
        
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        
        [StringLength(500, ErrorMessage = "Remarks cannot exceed 500 characters")]
        public string Remarks { get; set; }

        public bool IsVerified { get; set; }

        public Guid VerifiedBy { get; set; }
        public DateTime? VerifiedOn { get; set; }  

        [NotMapped]
        public byte[] FileContent { get; set; }

       [StringLength(100, ErrorMessage = "File type cannot exceed 100 characters")]
        public string FileType { get; set; }

       [StringLength(255, ErrorMessage = "File name cannot exceed 255 characters")]
        public string FileName { get; set; }

       [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }     

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        [ForeignKey("NonTeachingId")]
        public virtual NonTeachingMaster NonTeaching { get; set; }

    }
}
