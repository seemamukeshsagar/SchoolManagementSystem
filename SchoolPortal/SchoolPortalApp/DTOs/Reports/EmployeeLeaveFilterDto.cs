namespace SchoolPortalApp.DTOs.Reports
{
    public class EmployeeLeaveFilterDto : ReportFilterDto
    {
        public string Department { get; set; }
        public string LeaveType { get; set; }
        public string Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}