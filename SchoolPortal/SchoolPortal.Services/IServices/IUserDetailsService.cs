using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IUserDetailsService
    {
        List<SchoolPortal.Entities.Models.UserDetailsListViewModel> GetAll();
        SchoolPortal.Entities.Models.UserDetails? GetById(Guid id);
        Guid Create(SchoolPortal.Entities.Models.UserDetails entity);
        bool Update(SchoolPortal.Entities.Models.UserDetails entity);
        bool Delete(Guid id);
    }
}