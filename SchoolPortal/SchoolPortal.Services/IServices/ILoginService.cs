using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.ServiceViewModels;

namespace SchoolPortal.Services.IServices
{
	public interface ILoginService
	{
		public UserDetailsOutput? AuthenticateUser(string userName, string password);
		public Task<UserDetailsOutput?> AuthenticateUserAsync(string userName, string password);
		public string ChangePassword(string userName, string oldPassword, string newPassword);
	}
}
