#nullable disable
using System;
using System.Collections.Generic;

namespace SchoolPortal.Entities.Models;

public partial class ErrorType
{
    public Guid ID { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid CompanyId { get; set; }

    public Guid SchoolId { get; set; }
}