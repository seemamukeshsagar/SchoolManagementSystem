using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IStudentMasterService
    {
        List<StudentMaster> GetAll();
        StudentMaster? GetById(Guid id);
        Task<StudentMaster?> GetByIdAsync(Guid id);
        Guid Create(StudentMaster student);
        bool Update(StudentMaster student);
        Task<bool> UpdateAsync(StudentMaster student);
        bool Delete(Guid id);
    }
}
