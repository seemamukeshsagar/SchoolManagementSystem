#nullable disable
using System;
using System.Collections.Generic;

namespace SchoolPortal.Entities.Models;

public partial class RouteStopDetails
{
    public Guid Id { get; set; }

    public Guid RouteDetailId { get; set; }

    public Guid RouteId { get; set; }

    public Guid LocationId { get; set; }

    public int Number { get; set; }

    public string PickupTime { get; set; }

    public string DropTime { get; set; }

    public decimal? OneWayMonthlyFee { get; set; }

    public decimal? TwoWayMonthlyFee { get; set; }

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