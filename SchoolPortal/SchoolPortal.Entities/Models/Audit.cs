#nullable disable
using System;
using System.Collections.Generic;

namespace SchoolPortal.Entities.Models;

public partial class Audit
{
    public Guid ID { get; set; }

    public string FieldName { get; set; }

    public string BeforeValue { get; set; }

    public string AfterValue { get; set; }

    public string Message { get; set; }

    public Guid ChangeUserID { get; set; }

    public DateTime ChangeDate { get; set; }

    public string Note { get; set; }

    public string DeviceName { get; set; }

    public Guid? SchoolId { get; set; }

    public Guid? CompanyId { get; set; }

}