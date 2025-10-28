using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface ISchoolContactService
    {
        List<SchoolContactMaster> GetAll();
        SchoolContactMaster? GetById(Guid id);
        Guid Create(SchoolContactMaster contact);
        bool Update(SchoolContactMaster contact);
        bool Delete(Guid id);
    }
}
