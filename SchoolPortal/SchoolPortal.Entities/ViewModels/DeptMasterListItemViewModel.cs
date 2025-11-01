namespace SchoolPortal.Entities.ViewModels
{
    public class DeptMasterListItemViewModel
    {
        public Guid Id { get; set; }
        public string DeptCode { get; set; } = string.Empty;
        public string DeptName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string SchoolName { get; set; } = string.Empty;
    }
}