#nullable disable
using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Entities.Models;

public partial class SchoolContactMaster
{
    public Guid Id { get; set; }

    public Guid SchoolId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string MobilePhone { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public Guid CityId { get; set; }

    public Guid StateId { get; set; }

    public Guid CountryId { get; set; }
    public virtual CountryMaster Country { get; set; }

    public virtual StateMaster State { get; set; }

    public virtual CityMaster City { get; set; }

    public virtual SchoolMaster School { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string Status { get; set; } = "INC";

    public string StatusMessage { get; set; } = "In Process....";
}