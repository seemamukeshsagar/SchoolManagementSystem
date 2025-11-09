using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface ISchoolService
	{
		List<SchoolMaster> GetAll();
		List<SchoolMaster> GetByCompany(Guid companyId);
		SchoolMaster? GetById(Guid id);
		Guid Create(SchoolMaster school);
		bool Update(SchoolMaster school);
		bool Delete(Guid id);
		string SchoolNameById(Guid id);
	}
}
