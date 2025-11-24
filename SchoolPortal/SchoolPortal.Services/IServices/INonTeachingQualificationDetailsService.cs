// SchoolPortal.Services.IServices/INonTeachingQualificationDetailsService.cs
using SchoolPortal.Entities.Models;
using System;
using System.Collections.Generic;

namespace SchoolPortal.Services.IServices
{
    public interface INonTeachingQualificationDetailsService
    {
        IEnumerable<NonTeachingQualificationDetails> GetByNonTeachingId(Guid nonTeachingId);
        NonTeachingQualificationDetails GetQualificationById(Guid id);
        bool Add(NonTeachingQualificationDetails entity);
        bool Update(NonTeachingQualificationDetails entity);
        bool Delete(Guid id);
    }
}