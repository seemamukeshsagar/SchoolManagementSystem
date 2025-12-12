// SchoolPortal.Services/IServices/IStudentAttendanceService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IStudentAttendanceService
    {
        List<StudentAttendanceDetails> GetAll();
        StudentAttendanceDetails GetById(Guid id);
        Task<StudentAttendanceDetails> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(StudentAttendanceDetails attendance);
        Task<bool> UpdateAsync(StudentAttendanceDetails attendance);
        Task<bool> DeleteAsync(Guid id);
    }
}