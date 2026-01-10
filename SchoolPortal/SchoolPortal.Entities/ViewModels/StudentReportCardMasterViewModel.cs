using System;

namespace SchoolPortal.Entities.ViewModels
{
    public class StudentReportCardMasterViewModel
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string? StudentName { get; set; }
        public Guid ClassId { get; set; }
        public string? ClassName { get; set; }
        public Guid SectionId { get; set; }
        public string? SectionName { get; set; }
        public Guid SessionId { get; set; }
        public Guid AcademicYearId { get; set; }
        public string? AcademicYearName { get; set; }
        public Guid ReportCardType { get; set; }
        public string? ExamType { get; set; }
        public string? ReportCardValue { get; set; }
        public decimal? TotalMarks { get; set; }
        public decimal? ObtainedMarks { get; set; }
        public decimal? Percentage { get; set; }
        public string? Grade { get; set; }
        public int? Rank { get; set; }
        public string? Remarks { get; set; }
        public string? Period { get; set; }
        public DateTime? GeneratedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Guid CompanyId { get; set; }
        public Guid SchoolId { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDateValue { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedDateValue { get; set; }
        public string? Status { get; set; }
        public string? StatusMessage { get; set; }
    }
}
