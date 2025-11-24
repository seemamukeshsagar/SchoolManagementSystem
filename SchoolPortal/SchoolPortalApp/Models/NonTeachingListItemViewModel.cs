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

// SchoolPortalApp/Models/NonTeachingDetailsViewModel.cs
namespace SchoolPortalApp.Models
{
    public class NonTeachingDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string EmployeeCode { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string DOB { get; set; }
        public string DOJ { get; set; }
        public string DateOfLeaving { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Qualification { get; set; }
        public decimal? Salary { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        public string IFSCCode { get; set; }
        public string PAN { get; set; }
        public string AadharNumber { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string EmergencyContactRelation { get; set; }
        public bool IsActive { get; set; }
    }
}