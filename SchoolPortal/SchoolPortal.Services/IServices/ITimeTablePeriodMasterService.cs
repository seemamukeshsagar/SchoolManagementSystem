using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface ITimeTablePeriodMasterService
    {
        List<TimeTablePeriodMaster> GetAll();
        TimeTablePeriodMaster? GetById(Guid id);
        List<TimeTablePeriodMaster> GetBySetupId(Guid setupId);
        //Task<List<TimeTablePeriodMaster>> GetByClassSectionAndAcademicYearAsync(Guid classId, Guid sectionId, Guid academicYearId);
        Task<IEnumerable<TimeTablePeriodMaster>> GetByClassSectionAndAcademicYearAsync(Guid classId, Guid sectionId, Guid academicYearId);
        Task<bool> DeleteByClassSectionAndAcademicYearAsync(Guid classId, Guid sectionId, Guid academicYearId, Guid userId);
        Task<bool> SaveAsync(TimeTablePeriodMaster period);
        Guid Create(TimeTablePeriodMaster item);
        bool Update(TimeTablePeriodMaster item);
        bool Delete(Guid id);
    }
}