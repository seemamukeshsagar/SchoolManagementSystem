#nullable disable
using System;
using System.Collections.Generic;

namespace SchoolPortal.Entities.Models;

public partial class Error
{
    public Guid ID { get; set; }

    public Guid UserID { get; set; }

    public DateTime Timestamp { get; set; }

    public Guid? ErrorTypeID { get; set; }

    public string ActiveForm { get; set; }

    public string Message { get; set; }

    public Guid CompanyId { get; set; }

    public Guid SchoolId { get; set; }

    public DateTime ServerTimeStamp { get; set; }
}