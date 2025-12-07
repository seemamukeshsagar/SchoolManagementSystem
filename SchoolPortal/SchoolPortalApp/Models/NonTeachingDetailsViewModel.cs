#nullable enable

// SchoolPortalApp/Models/NonTeachingDetailsViewModel.cs
namespace SchoolPortalApp.Models
{
    public class NonTeachingDetailsViewModel
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string MobilePhone { get; set; } = string.Empty;
        public string EmployeeCode { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string DOB { get; set; } = string.Empty;
        public string DOJ { get; set; } = string.Empty;
        public string DateOfLeaving { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string MaritalStatus { get; set; } = string.Empty;
        public string Qualification { get; set; } = string.Empty;
        public decimal? Salary { get; set; }
        public string BankName { get; set; } = string.Empty;
        public string BankAccountNumber { get; set; } = string.Empty;
        public string IFSCCode { get; set; } = string.Empty;
        public string PAN { get; set; } = string.Empty;
        public string AadharNumber { get; set; } = string.Empty;
        public string EmergencyContactName { get; set; } = string.Empty;
        public string EmergencyContactNumber { get; set; } = string.Empty;
        public string EmergencyContactRelation { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}