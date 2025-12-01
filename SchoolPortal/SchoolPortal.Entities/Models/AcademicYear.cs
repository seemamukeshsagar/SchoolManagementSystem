// File: SchoolPortal.Entities/Models/AcademicYear.cs
using System;

namespace SchoolPortal.Entities.Models
{
    public class AcademicYear
    {
        public Guid Id { get; set; }
        public string AcademicYearName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}