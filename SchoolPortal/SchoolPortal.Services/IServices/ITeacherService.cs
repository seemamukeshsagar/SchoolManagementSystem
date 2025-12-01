using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface ITeacherService
    {
        List<TeacherMaster> GetAll();
        List<TeacherMaster> GetAll(Guid? schoolId);
        List<TeacherMaster> GetAllActive(Guid? schoolId = null);
        TeacherMaster? GetById(Guid id);
        Guid Create(TeacherMaster teacher);
        bool Update(TeacherMaster teacher);
        bool Delete(Guid id);
        
    }
}