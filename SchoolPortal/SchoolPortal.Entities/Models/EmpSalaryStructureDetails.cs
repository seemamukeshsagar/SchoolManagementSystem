#nullable disable
using System;
using System.Collections.Generic;

namespace SchoolPortal.Entities.Models;

public partial class EmpSalaryStructureDetails
{
    public Guid ID { get; set; }

    public Guid EmployeeId { get; set; }

    public Guid DesignationGradeId { get; set; }

    public Guid Session { get; set; }

    public decimal? Value { get; set; }

    public Guid SalaryTypeId { get; set; }

    public bool IsDeductance { get; set; }

    public Guid SalaryCodeId { get; set; }

    public string Description { get; set; }

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