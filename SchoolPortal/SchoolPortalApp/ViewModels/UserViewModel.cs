using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortalApp.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}".Trim();

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Last Login")]
        public DateTime? LastLoginDate { get; set; }
        
        [Display(Name = "Last Login")]
        public DateTime? LastLogin => LastLoginDate;

        [Display(Name = "Profile Image")]
        public string ProfileImage { get; set; }

        [Display(Name = "Is Locked Out")]
        public bool IsLockedOut { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Roles")]
        public List<RoleViewModel> Roles { get; set; } = new List<RoleViewModel>();

        [Display(Name = "Permissions")]
        public List<PermissionViewModel> Permissions { get; set; } = new List<PermissionViewModel>();

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
