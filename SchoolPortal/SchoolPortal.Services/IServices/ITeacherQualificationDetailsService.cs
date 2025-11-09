using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface ITeacherQualificationDetailsService
	{
		List<TeacherQualificationDetails> GetAll();
		TeacherQualificationDetails? GetById(Guid id);
		Guid Create(TeacherQualificationDetails item);
		bool Update(TeacherQualificationDetails item);
		bool Delete(Guid id);
	}
}
