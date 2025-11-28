using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IStudentService
	{
		List<StudentMaster> GetAll(Guid? schoolId = null);
		StudentMaster? GetById(Guid id);
		Guid Create(StudentMaster student);
		bool Update(StudentMaster student);
		bool Delete(Guid id);
		bool CategoryExists(Guid categoryId);
	}
}
