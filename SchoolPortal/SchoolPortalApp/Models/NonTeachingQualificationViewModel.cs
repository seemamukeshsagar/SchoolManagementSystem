namespace SchoolPortalApp.Models
{
    public class NonTeachingQualificationViewModel
    {
        public Guid Id { get; set; }
        public Guid NonTeachingId { get; set; }
        public string Qualification { get; set; }
        public Guid QualificationTypeId { get; set; }
        public string Institution { get; set; }
        public string BoardUniversity { get; set; }
        public string? YearOfPassing { get; set; }
        public decimal? Percentage { get; set; }
        public string Division { get; set; }
        public string DocumentPath { get; set; }
        public bool IsVerified { get; set; }
        public Guid VerifiedBy { get; set; }
        public DateTime? VerifiedOn { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
