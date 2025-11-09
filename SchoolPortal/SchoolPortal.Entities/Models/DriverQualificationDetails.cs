#nullable disable
using System;

namespace SchoolPortal.Entities.Models;

public partial class DriverQualificationDetails
{
    public Guid Id { get; set; }
    public Guid DriverId { get; set; }
    public Guid QualificationId { get; set; }
    public Guid SchoolId { get; set; }
    public Guid CompanyId { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string Status { get; set; } = "INC";
    public string StatusMessage { get; set; } = "In Process....";
}
