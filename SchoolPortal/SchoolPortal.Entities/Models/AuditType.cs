#nullable disable
using System;
using System.Collections.Generic;

namespace SchoolPortal.Entities.Models;

public partial class AuditType
{
    public int ID { get; set; }

    public string Category { get; set; }

    public string Type { get; set; }

    public string Name { get; set; }

    public Guid? SchoolId { get; set; }

    public Guid? CompanyId { get; set; }
}