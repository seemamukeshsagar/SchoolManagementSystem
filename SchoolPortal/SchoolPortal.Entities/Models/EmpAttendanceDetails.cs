#nullable enable
namespace SchoolPortal.Entities.Models;
public partial class EmpAttendanceDetails
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public int? AttendenceMonth { get; set; }
    public int? AttendenceYear { get; set; }
    public DateTime AttendenceDate { get; set; }
    public bool AttendenceMarked { get; set; }
    public Guid AttendenceLeaveTypeId { get; set; }
    public string AttendenceTime { get; set; } = string.Empty;
    public bool? IsHalfDay { get; set; }
    public Guid CompanyId { get; set; }
    public Guid SchoolId { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string Status { get; set; } = "INC";
    public string StatusMessage { get; set; } = "In Process....";
}