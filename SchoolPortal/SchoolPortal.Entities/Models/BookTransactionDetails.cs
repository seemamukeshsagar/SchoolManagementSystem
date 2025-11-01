#nullable disable
using System;
using System.Collections.Generic;

namespace SchoolPortal.Entities.Models;

public partial class BookTransactionDetails
{
    public Guid Id { get; set; }

    public Guid BookId { get; set; }

    public DateTime IssueDate { get; set; }

    public int IssueDays { get; set; }

    public DateTime ReturnDueDate { get; set; }

    public DateTime? ActualReturnDate { get; set; }

    public int? LateDays { get; set; }

    public decimal? FinePerDay { get; set; }

    public bool? IsFineApplicable { get; set; }

    public bool? IsFinePaid { get; set; }

    public Guid? BookTransactionTypeId { get; set; }

    public Guid? ClassMasterId { get; set; }

    public Guid? SectionMasterId { get; set; }

    public Guid? StudentGuid { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CompanyId { get; set; }

    public Guid SchoolId { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string Status { get; set; } = "INC";

    public string StatusMessage { get; set; } = "In Process....";
}