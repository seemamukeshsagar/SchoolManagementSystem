using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface ITeacherSectionDetailsService
    {
        List<TeacherSectionDetails> GetAll();
        TeacherSectionDetails? GetById(Guid id);
        Guid Create(TeacherSectionDetails item);
        bool Update(TeacherSectionDetails item);
        bool Delete(Guid id);
    }
}