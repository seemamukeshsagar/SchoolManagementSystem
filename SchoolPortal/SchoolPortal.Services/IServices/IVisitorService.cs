using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IVisitorService
    {
        List<VisitorMaster> GetAll();
        VisitorMaster? GetById(Guid id);
        Guid Create(VisitorMaster entity);
        bool Update(VisitorMaster entity);
        bool Delete(Guid id);
    }
}