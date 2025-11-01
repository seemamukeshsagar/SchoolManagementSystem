#nullable disable
using System;
using System.Collections.Generic;

namespace SchoolPortal.Entities.Models;

public partial class SalaryHeadMaster
{
    public Guid Id { get; set; }

    public string Code { get; set; }

    public string Description { get; set; }

    public bool? IsReadOnly { get; set; }

    public Guid SalaryTypeId { get; set; }

    public bool IsDeduction { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string Status { get; set; } = "INC";

    public string StatusMessage { get; set; } = "In Process....";

    public Guid? SchoolId { get; set; }

    public Guid? CompanyId { get; set; }
}