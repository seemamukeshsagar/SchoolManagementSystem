namespace SchoolPortal.DTOs.Reports
{
    public class StudentReportFilterDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public string? SearchTerm { get; set; }
        public string? Status { get; set; }
    }
}
