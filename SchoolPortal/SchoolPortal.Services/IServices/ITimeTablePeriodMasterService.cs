using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface ITimeTablePeriodMasterService
    {
        List<TimeTablePeriodMaster> GetAll();
        TimeTablePeriodMaster? GetById(Guid id);
        Guid Create(TimeTablePeriodMaster item);
        bool Update(TimeTablePeriodMaster item);
        bool Delete(Guid id);
    }
}