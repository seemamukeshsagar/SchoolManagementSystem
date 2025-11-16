using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IAssesmentMasterService
	{
		List<AssesmentMaster> GetAll();
		AssesmentMaster? GetById(Guid id);
		Guid Create(AssesmentMaster assesment);
		bool Update(AssesmentMaster assesment);
		bool Delete(Guid id);
	}
}
