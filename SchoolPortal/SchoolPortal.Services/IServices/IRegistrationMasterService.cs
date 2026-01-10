using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IRegistrationMasterService
    {
        List<RegistrationMaster> GetAll();
        RegistrationMaster? GetById(Guid id);
        Task<RegistrationMaster?> GetByIdAsync(Guid id);
        Guid Create(RegistrationMaster registration);
        Task<Guid> CreateAsync(RegistrationMaster registration);
        bool Update(RegistrationMaster registration);
        Task<bool> UpdateAsync(RegistrationMaster registration);
        bool Delete(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
