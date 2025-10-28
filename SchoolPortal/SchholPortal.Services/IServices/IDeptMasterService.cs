using System;
using System.Collections.Generic;
using Schoolortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IDeptMasterService
    {
        List<DeptMaster> GetAll();
        DeptMaster? GetById(Guid id);
        Guid Create(DeptMaster dept);
        bool Update(DeptMaster dept);
        bool Delete(Guid id);
    }
}