using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IBookMasterService
    {
        List<BookMaster> GetAll();
        BookMaster? GetById(Guid id);
        Task<BookMaster?> GetByIdAsync(Guid id);
        Guid Create(BookMaster book);
        Task<Guid> CreateAsync(BookMaster book);
        bool Update(BookMaster book);
        Task<bool> UpdateAsync(BookMaster book);
        bool Delete(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
