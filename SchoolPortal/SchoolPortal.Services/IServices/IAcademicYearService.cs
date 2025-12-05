// File: SchoolPortal.Services/IServices/IAcademicYearService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IAcademicYearService
    {
        IEnumerable<AcademicYear> GetAll();
        Task<IEnumerable<AcademicYear>> GetAllActiveAsync();
        AcademicYear? GetById(Guid id);
        Task<AcademicYear?> GetByIdAsync(Guid id);
        Guid Create(AcademicYear academicYear);
        bool Update(AcademicYear academicYear);
        bool Delete(Guid id);
        bool ToggleStatus(Guid id);
        AcademicYear? GetCurrentAcademicYear();
    }
}