using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IEmpService
    {
        List<EmpMaster> GetAll();
        EmpMaster? GetById(Guid id);
        Guid Create(EmpMaster emp);
        bool Update(EmpMaster emp);
        bool Delete(Guid id);
    }
}
