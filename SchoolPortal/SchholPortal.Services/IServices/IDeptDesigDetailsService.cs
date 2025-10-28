using System;
using System.Collections.Generic;
using Schoolortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IDeptDesigDetailsService
    {
        List<DeptDesigDetails> GetAll();
        DeptDesigDetails? GetById(Guid id);
        Guid Create(DeptDesigDetails deptDesigDetails);
        bool Update(DeptDesigDetails deptDesigDetails);
        bool Delete(Guid id);
    }
}