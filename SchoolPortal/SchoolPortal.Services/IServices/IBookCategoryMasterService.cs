using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IBookCategoryMasterService
    {
        List<BookCategoryMaster> GetAll();
        BookCategoryMaster? GetById(Guid id);
        Task<BookCategoryMaster?> GetByIdAsync(Guid id);
        Guid Create(BookCategoryMaster bookCategory);
        bool Update(BookCategoryMaster bookCategory);
        Task<bool> UpdateAsync(BookCategoryMaster bookCategory);
        bool Delete(Guid id);
    }
}
