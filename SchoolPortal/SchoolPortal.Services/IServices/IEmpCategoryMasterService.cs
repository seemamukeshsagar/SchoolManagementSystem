using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IEmpCategoryMasterService
    {
        List<EmpCategoryMaster> GetAll();
        EmpCategoryMaster? GetById(Guid id);
        Task<EmpCategoryMaster?> GetByIdAsync(Guid id);
        Guid Create(EmpCategoryMaster category);
        Task<Guid> CreateAsync(EmpCategoryMaster category);
        bool Update(EmpCategoryMaster category);
        Task<bool> UpdateAsync(EmpCategoryMaster category);
        bool Delete(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
