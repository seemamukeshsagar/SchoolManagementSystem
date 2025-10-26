using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schoolortal.Entities.Models;

namespace SchoolPortal.Services.ServiceViewModels
{
    public class UserDetailsOutput : UserDetails
    {
        public string FullName { get; set; } = string.Empty;
        public List<string> Privileges { get; set; } = new List<string>();
    }
}
