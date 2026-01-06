namespace SchoolPortal.DTOs.UserManagement
{
    public class UpdatePermissionsRequest
    {
        public Guid RoleId { get; set; }
        public List<string> Permissions { get; set; } = new();
    }
}
