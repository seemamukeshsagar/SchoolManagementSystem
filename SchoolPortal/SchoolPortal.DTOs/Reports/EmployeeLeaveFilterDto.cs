namespace SchoolPortal.DTOs.Reports
{
    public class EmployeeLeaveFilterDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? EmployeeId { get; set; }
        public int? LeaveTypeId { get; set; }
        public string? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
