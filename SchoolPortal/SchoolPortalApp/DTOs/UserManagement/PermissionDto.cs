namespace SchoolPortalApp.DTOs.UserManagement
{
    public class PermissionDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsGranted { get; set; }
    }
}    