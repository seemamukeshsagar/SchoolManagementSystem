#nullable disable
using System;
using System.Collections.Generic;

namespace SchoolPortal.Entities.Models;

public partial class StudentMarksDetails
{
    public Guid Id { get; set; }

    public Guid StudentGUID { get; set; }

    public Guid SubjectId { get; set; }

    public decimal? GradeQ1 { get; set; }

    public decimal? GradeQ2 { get; set; }

    public decimal? GradeQ3 { get; set; }

    public decimal? GradeFA1 { get; set; }

    public decimal? GradeFA2 { get; set; }

    public decimal? GradeFA3 { get; set; }

    public decimal? GradeFA4 { get; set; }

    public decimal? GradeSA1 { get; set; }

    public decimal? GradeSA2 { get; set; }

    public Guid ClassId { get; set; }

    public Guid SectionId { get; set; }

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