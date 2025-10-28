using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface ISubjectCategoryService
    {
        List<SubjectCategoryDetails> GetAll();
        SubjectCategoryDetails? GetById(Guid id);
        Guid Create(SubjectCategoryDetails category);
        bool Update(SubjectCategoryDetails category);
        bool Delete(Guid id);
    }
}
