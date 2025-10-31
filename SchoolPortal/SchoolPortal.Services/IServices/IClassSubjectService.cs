// SchoolPortal.Services/IServices/IClassSubjectService.cs
using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IClassSubjectService
    {
        List<ClassSubjectDetail> GetAll();
        ClassSubjectDetail? GetById(Guid id);  // Made return type nullable
        List<ClassSubjectDetail> GetByClassId(Guid classId);
        List<ClassSubjectDetail> GetBySubjectId(Guid subjectId);
        Guid Create(ClassSubjectDetail classSubject);
        bool Update(ClassSubjectDetail classSubject);
        bool Delete(Guid id);
    }
}