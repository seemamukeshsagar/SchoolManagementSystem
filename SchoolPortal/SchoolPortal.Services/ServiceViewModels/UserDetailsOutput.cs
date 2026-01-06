using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.ServiceViewModels
{
	public class UserDetailsOutput : UserDetails
	{
		public string FullName { get; set; } = string.Empty;
		public new List<string> Privileges { get; set; } = new List<string>();
		public string DesignationName { get; set; } = string.Empty;
		public new string RoleName { get; set; } = string.Empty;
		public string? PasswordHash { get; set; } // Added for caching
	}
}
