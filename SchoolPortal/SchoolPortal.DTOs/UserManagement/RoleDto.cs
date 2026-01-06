using System.ComponentModel.DataAnnotations;

namespace SchoolPortal.DTOs.UserManagement
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Role name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Role name must be between 3 and 50 characters")]
        public required string Name { get; set; }
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }
        public bool IsSystemRole { get; set; }
    }
}
