using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface ISectionService
    {
        List<SectionMaster> GetAll();
        SectionMaster? GetById(Guid id);
        Guid Create(SectionMaster section);
        bool Update(SectionMaster section);
        bool Delete(Guid id);
    }
}
