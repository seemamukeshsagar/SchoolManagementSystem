#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolPortal.Entities.Models;

public partial class UserDetails
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string UserPassword { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }

    public Guid DesignationId { get; set; }

    public Guid? UserRoleId { get; set; }

    public bool? IsSuperUser { get; set; }

    public Guid? CompanyId { get; set; }

    public Guid? SchoolId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; } = DateTime.Now;

    public string Status { get; set; } = "INC";

    public string StatusMessage { get; set; } = "In Process....";

    [NotMapped]
    public string RoleName { get; set; } = string.Empty;
    
    [NotMapped]
    public List<string> Privileges { get; set; } = new List<string>();
}