namespace SchoolPortalApp.DTOs.Reports
{
    public class StudentReportFilterDto : ReportFilterDto
    {
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public string Status { get; set; }
    }
}