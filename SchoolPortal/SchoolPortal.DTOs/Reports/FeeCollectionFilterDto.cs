using System;

namespace SchoolPortal.DTOs.Reports
{
    public class FeeCollectionFilterDto
    {
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public int? FeeTypeId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? PaymentStatus { get; set; }
    }
}
