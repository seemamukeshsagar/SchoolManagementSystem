using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IPrivilegeService
    {
        IEnumerable<Privileges> GetAll();
        Privileges? GetById(Guid id);
        Guid Create(Privileges entity);
        bool Update(Privileges entity);
        bool Delete(Guid id);
    }
}