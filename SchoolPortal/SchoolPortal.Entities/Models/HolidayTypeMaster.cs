#nullable disable
using System;
using System.Collections.Generic;

namespace SchoolPortal.Entities.Models;

public partial class HolidayTypeMaster
{
    public Guid Id { get; set; }

    public string HolidayTypeName { get; set; }

    public string HolidayTypeDescription { get; set; }

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