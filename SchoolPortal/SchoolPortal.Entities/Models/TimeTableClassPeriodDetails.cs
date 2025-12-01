#nullable disable
using System;
using System.Collections.Generic;

namespace SchoolPortal.Entities.Models;

public partial class TimeTableClassPeriodDetails
{
    public Guid Id { get; set; }

    public Guid ClassId { get; set; }

    public Guid SectionId { get; set; }

    public Guid SubjectId { get; set; }

    public Guid PeriodId { get; set; }

    public int DayOfWeek { get; set; }

    public Guid SessionId { get; set; }

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

    public bool IsBreak { get; set; }
    public string BreakName { get; set; }   

    public Guid? TeacherId { get; set; }
    public virtual TeacherMaster Teacher { get; set; }
    public virtual SubjectMaster Subject { get; set; }
    public int PeriodNumber { get; set; }
    public TimeSpan PeriodStartTime { get; set; }
    public TimeSpan PeriodEndTime { get; set; }
}