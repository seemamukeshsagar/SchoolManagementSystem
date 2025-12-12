using System;
namespace SchoolPortalApp.Models.Attendance
{
    public class StudentAttendanceListItemViewModel
    {
        public Guid Id { get; set; }
        public string StudentName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public DateTime AttendenceDate { get; set; }
        public bool AttendenceStatus { get; set; }
        public string Status { get; set; }
    }
}