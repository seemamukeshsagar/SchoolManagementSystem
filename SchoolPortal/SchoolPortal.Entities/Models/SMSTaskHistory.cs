#nullable disable
using System;
using System.Collections.Generic;

namespace SchoolPortal.Entities.Models;

public partial class SMSTaskHistory
{
    public Guid Id { get; set; }

    public Guid TaskId { get; set; }

    public DateTime SentDate { get; set; }

    public Guid ReceiverId { get; set; }

    public string NotificationReceiver { get; set; }

    public string SendType { get; set; }

    public string Status { get; set; } = "INC";

    public Guid? StudentGuid { get; set; }

    public Guid ParentId { get; set; }

    public Guid TeacherId { get; set; }

    public string EmailId { get; set; }

    public string PhoneNumber { get; set; }

    public string Description { get; set; }

    public Guid CompanyId { get; set; }

    public Guid SchoolId { get; set; }

    public bool? IsReadOnly { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }
}