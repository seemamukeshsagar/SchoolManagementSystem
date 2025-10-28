using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IDesigMasterService
    {
        List<DesigMaster> GetAll();
        DesigMaster? GetById(Guid id);
        Guid Create(DesigMaster desig);
        bool Update(DesigMaster desig);
        bool Delete(Guid id);
    }
}