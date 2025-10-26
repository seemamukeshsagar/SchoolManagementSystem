using System;
using System.Collections.Generic;
using Schoolortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface ITeacherClassDetailsService
    {
        List<TeacherClassDetails> GetAll();
        TeacherClassDetails? GetById(Guid id);
        Guid Create(TeacherClassDetails item);
        bool Update(TeacherClassDetails item);
        bool Delete(Guid id);
    }
}
