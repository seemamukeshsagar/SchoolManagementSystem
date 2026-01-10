using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IAuthorMasterService
    {
        List<AuthorMaster> GetAll();
        AuthorMaster? GetById(Guid id);
        Task<AuthorMaster?> GetByIdAsync(Guid id);
        Guid Create(AuthorMaster author);
        Task<Guid> CreateAsync(AuthorMaster author);
        bool Update(AuthorMaster author);
        Task<bool> UpdateAsync(AuthorMaster author);
        bool Delete(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
