using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IParentMasterService
    {
        List<ParentMaster> GetAll();
        ParentMaster? GetById(Guid id);
        Task<ParentMaster?> GetByIdAsync(Guid id);
        Guid Create(ParentMaster parent);
        Task<Guid> CreateAsync(ParentMaster parent);
        bool Update(ParentMaster parent);
        Task<bool> UpdateAsync(ParentMaster parent);
        bool Delete(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
