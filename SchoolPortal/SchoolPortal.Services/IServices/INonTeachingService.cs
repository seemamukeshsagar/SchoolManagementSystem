using SchoolPortal.Entities.Models;
using System;
using System.Collections.Generic;

namespace SchoolPortal.Services.IServices
{
    public interface INonTeachingService
    {
        IEnumerable<NonTeachingMaster> GetAll();
        NonTeachingMaster? GetById(Guid id);
        int Add(NonTeachingMaster entity);
        bool Update(NonTeachingMaster entity);
        bool Delete(Guid id, Guid? currentUserId);
    }
}