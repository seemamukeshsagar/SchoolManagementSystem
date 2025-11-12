using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IUserDetailsService
    {
        List<UserDetails> GetAll();
        UserDetails? GetById(Guid id);
        Guid Create(UserDetails entity);
        bool Update(UserDetails entity);
        bool Delete(Guid id);
    }
}