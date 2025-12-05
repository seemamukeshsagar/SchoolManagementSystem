using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface ITimeTableSetupDetailsService
    {
        List<TimeTableSetupDetails> GetAll();
        TimeTableSetupDetails? GetById(Guid id);
        Task<TimeTableSetupDetails?> GetLatestSetupAsync(Guid classId, Guid sectionId, Guid academicYearId);
        Guid Create(TimeTableSetupDetails item);
        bool Update(TimeTableSetupDetails item);
        bool Delete(Guid id);
    }
}