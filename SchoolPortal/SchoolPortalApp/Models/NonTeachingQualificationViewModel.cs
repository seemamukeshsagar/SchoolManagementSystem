#nullable enable

namespace SchoolPortalApp.Models
{
    public class NonTeachingQualificationViewModel
    {
        public Guid Id { get; set; } = Guid.Empty;
        public Guid NonTeachingId { get; set; } = Guid.Empty;
        public string Qualification { get; set; } = string.Empty;
        public Guid QualificationTypeId { get; set; } = Guid.Empty;
        public string Institution { get; set; } = string.Empty;
        public string BoardUniversity { get; set; } = string.Empty;
        public string? YearOfPassing { get; set; } = string.Empty;
        public decimal? Percentage { get; set; }
        public string Division { get; set; } = string.Empty;
        public string DocumentPath { get; set; } = string.Empty;
        public bool IsVerified { get; set; } = false;
        public Guid VerifiedBy { get; set; } = Guid.Empty;
        public DateTime? VerifiedOn { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public Guid CreatedBy { get; set; } = Guid.Empty;
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public Guid ModifiedBy { get; set; } = Guid.Empty;
        public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
