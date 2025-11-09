using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface ITeacherSubjectDetailsService
	{
		List<TeacherSubjectDetails> GetAll();
		TeacherSubjectDetails? GetById(Guid id);
		Guid Create(TeacherSubjectDetails item);
		bool Update(TeacherSubjectDetails item);
		bool Delete(Guid id);
	}
}
