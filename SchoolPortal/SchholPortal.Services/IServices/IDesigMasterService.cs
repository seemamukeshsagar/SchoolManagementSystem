using System;
using System.Collections.Generic;
using Schoolortal.Entities.Models;

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