using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface ISubjectService
	{
		List<SubjectMaster> GetAll();
        List<SubjectMaster> GetAll(Guid? schoolId);
        SubjectMaster? GetById(Guid id);
		Guid Create(SubjectMaster subject);
		bool Update(SubjectMaster subject);
		bool Delete(Guid id);
		public List<SubjectMaster> GetByClassId(Guid id);

    }
}
