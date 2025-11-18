using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface ISessionMasterService
    {
        List<SessionMaster> GetAll();
        SessionMaster? GetById(Guid id);
        Guid Create(SessionMaster entity);
        bool Update(SessionMaster entity);
        bool Delete(Guid id);
    }
}