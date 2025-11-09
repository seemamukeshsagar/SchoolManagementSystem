using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface ICleanerMasterService
    {
        Guid Create(CleanerMaster cleaner);
        CleanerMaster? GetByKey(Guid companyId, Guid schoolId, string name);
        List<CleanerMaster> GetAll();
        CleanerMaster? GetById(Guid id);
        bool Update(CleanerMaster cleaner);
        bool Delete(Guid id);
    }
}
