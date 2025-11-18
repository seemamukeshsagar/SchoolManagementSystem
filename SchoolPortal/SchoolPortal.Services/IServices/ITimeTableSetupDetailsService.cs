using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface ITimeTableSetupDetailsService
    {
        List<TimeTableSetupDetails> GetAll();
        TimeTableSetupDetails? GetById(Guid id);
        Guid Create(TimeTableSetupDetails item);
        bool Update(TimeTableSetupDetails item);
        bool Delete(Guid id);
    }
}