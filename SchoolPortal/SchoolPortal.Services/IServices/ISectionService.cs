using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface ISectionService
    {
        List<SectionMaster> GetAll();
        List<SectionMaster> GetAll(Guid? schoolId);
        SectionMaster? GetById(Guid id);
        Task<SectionMaster?> GetByIdAsync(Guid id);
        List<SectionMaster> GetSectionsByClassId(Guid? classId);
        Guid Create(SectionMaster section);
        bool Update(SectionMaster section);
        bool Delete(Guid id);
        string SectionNameById(Guid id);
        IEnumerable<SectionMaster> GetByClassId(Guid classId);
        Task<IEnumerable<SectionMaster>> GetByClassIdAsync(Guid classId);
    }
}
