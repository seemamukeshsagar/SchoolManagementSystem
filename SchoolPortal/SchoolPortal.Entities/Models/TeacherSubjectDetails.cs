#nullable disable
using System;
using System.Collections.Generic;

namespace SchoolPortal.Entities.Models;

public partial class TeacherSubjectDetails
{
    public Guid Id { get; set; }

    public Guid TeacherId { get; set; }

    public Guid SubjectId { get; set; }

    public Guid ClassId { get; set; }

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