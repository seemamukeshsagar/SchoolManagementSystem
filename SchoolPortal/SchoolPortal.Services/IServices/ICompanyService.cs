using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface ICompanyService
    {
        List<CompanyMaster> GetAll();
        CompanyMaster? GetById(Guid id);
        Guid Create(CompanyMaster company);
        bool Update(CompanyMaster company);
        bool Delete(Guid id);
    }
}
