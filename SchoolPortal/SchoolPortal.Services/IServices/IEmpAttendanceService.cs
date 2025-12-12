// Create a new file: SchoolPortal.Services/IServices/IEmpAttendanceService.cs
using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IEmpAttendanceService
    {
        List<EmpAttendanceDetails> GetAll();
        EmpAttendanceDetails GetById(Guid id);
        Task<EmpAttendanceDetails> GetByIdAsync(Guid id);
        Guid Create(EmpAttendanceDetails attendance);
        bool Update(EmpAttendanceDetails attendance);
        Task<bool> UpdateAsync(EmpAttendanceDetails attendance);
        bool Delete(Guid id);
    }
}