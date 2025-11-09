using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IDeptMasterService
	{
		List<DeptMaster> GetAll();
		List<DeptMaster> GetBySchool(Guid schoolId);
		DeptMaster? GetById(Guid id);
		Guid Create(DeptMaster dept);
		bool Update(DeptMaster dept);
		bool Delete(Guid id);
		void BulkInsert(IEnumerable<DeptMaster> departments);
	}
}