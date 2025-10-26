using System;
using System.Collections.Generic;
using Schoolortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IClassRoomService
    {
        List<ClassRoomMaster> GetAll();
        ClassRoomMaster? GetById(Guid id);
        Guid Create(ClassRoomMaster room);
        bool Update(ClassRoomMaster room);
        bool Delete(Guid id);
    }
}
