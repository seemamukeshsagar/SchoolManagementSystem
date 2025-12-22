using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;
namespace SchoolPortal.Services.IServices
{
    public interface IEmpAttendanceService
    {
        List<EmpAttendanceDetails> GetAll();
        EmpAttendanceDetails? GetById(Guid id);  // Made return type nullable
        Task<EmpAttendanceDetails?> GetByIdAsync(Guid id);  // Made return type nullable
        Guid Create(EmpAttendanceDetails attendance);
        bool Update(EmpAttendanceDetails attendance);
        Task<bool> UpdateAsync(EmpAttendanceDetails attendance);
        bool Delete(Guid id);
    }
}