using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IProfessionMasterService
	{
		List<ProfessionMaster> GetAll();
		ProfessionMaster? GetById(Guid id);
		Guid Create(ProfessionMaster profession);
		bool Update(ProfessionMaster profession);
		bool Delete(Guid id);
	}
}
