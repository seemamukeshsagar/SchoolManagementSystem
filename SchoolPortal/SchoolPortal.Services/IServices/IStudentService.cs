using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IStudentService
    {
        // Standard async methods
        Task<List<StudentMaster>> GetAllAsync(Guid? schoolId = null);
        Task<StudentMaster> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(StudentMaster student);
        Task<bool> UpdateAsync(StudentMaster student);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> CategoryExistsAsync(Guid categoryId);
    }
}
