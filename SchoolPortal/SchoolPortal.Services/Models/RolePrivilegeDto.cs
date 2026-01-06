namespace SchoolPortal.Services.Models
{
    public class RolePrivilegeDto
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid PrivilegeId { get; set; }
        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanPrint { get; set; }
        public bool CanExport { get; set; }
        public bool CanImport { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
