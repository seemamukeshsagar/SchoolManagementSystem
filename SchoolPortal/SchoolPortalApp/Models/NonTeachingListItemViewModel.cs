// SchoolPortalApp/Models/NonTeachingListItemViewModel.cs
using System;

namespace SchoolPortalApp.Models
{
    public class NonTeachingListItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        public string EmployeeCode { get; set; }
    }
}

