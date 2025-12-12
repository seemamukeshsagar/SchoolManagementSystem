using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IStudentService
	{
		List<StudentMaster> GetAll(Guid? schoolId = null);
		Task<StudentAttendanceDetails> GetByIdAsync(Guid id);
		Task<Guid> CreateAsync(StudentAttendanceDetails attendance);
		Task<bool> UpdateAsync(StudentAttendanceDetails attendance);
		Task<bool> DeleteAsync(Guid id);
		bool CategoryExists(Guid categoryId);
	}
}
