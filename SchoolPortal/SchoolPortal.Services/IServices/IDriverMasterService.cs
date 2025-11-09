using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IDriverMasterService
    {
        Guid Create(DriverMaster driver);
        DriverMaster? GetByKey(Guid companyId, Guid schoolId, string firstName, string lastName);
        List<DriverMaster> GetAll();
        DriverMaster? GetById(Guid id);
        bool Update(DriverMaster driver);
        bool Delete(Guid id);
    }
}
