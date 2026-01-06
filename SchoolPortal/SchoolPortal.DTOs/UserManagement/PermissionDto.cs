namespace SchoolPortal.DTOs.UserManagement
{
    public class PermissionDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public bool IsGranted { get; set; }
    }
}
