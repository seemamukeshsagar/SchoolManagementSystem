using System;
using System.Collections.Generic;

namespace SchoolPortal.Entities.Models;

public partial class ClassSectionDetail
{
    public Guid Id { get; set; }

    public Guid ClassMasterId { get; set; }

    public Guid SectionMasterId { get; set; }

    public Guid LocationId { get; set; }

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

    public virtual ClassMaster Class { get; set; }
    public virtual SectionMaster Section { get; set; }
    public virtual LocationMaster Location { get; set; }
}