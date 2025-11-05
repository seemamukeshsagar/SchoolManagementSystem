using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IClassService
    {
        List<ClassMaster> GetAll();
        ClassMaster? GetById(Guid id);
        Guid Create(ClassMaster cls);
        bool Update(ClassMaster cls);
        bool Delete(Guid id);
        string ClassNameById(Guid id);
    }
}
