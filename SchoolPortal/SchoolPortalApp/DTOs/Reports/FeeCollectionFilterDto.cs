namespace SchoolPortalApp.DTOs.Reports
{
    public class FeeCollectionFilterDto : ReportFilterDto
    {
        public int? ClassId { get; set; }
        public int? FeeTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
    }
}