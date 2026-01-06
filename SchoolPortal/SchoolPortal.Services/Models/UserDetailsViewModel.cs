using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Services.Models
{
    public class UserDetailsViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Password")]
        public string UserPassword { get; set; } = string.Empty;

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Designation")]
        public Guid DesignationId { get; set; }

        [Display(Name = "Role")]
        public Guid? UserRoleId { get; set; }

        [Display(Name = "Super User")]
        public bool? IsSuperUser { get; set; }

        [Display(Name = "Company")]
        public Guid? CompanyId { get; set; }

        [Display(Name = "School")]
        public Guid? SchoolId { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        public IEnumerable<SelectListItem> Designations { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Roles { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Companies { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Schools { get; set; } = Array.Empty<SelectListItem>();
        public string RoleName { get; internal set; } = string.Empty;
        public object Privileges { get; internal set; } = new List<string>();
        public string DesignationName { get; internal set; } = string.Empty;
        public string CompanyName { get; internal set; } = string.Empty;
        public string SchoolName { get; internal set; } = string.Empty;
        public DateTime CreatedDate { get; internal set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; internal set; }
    }
}
