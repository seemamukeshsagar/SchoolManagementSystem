using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IFeesCategoryMasterService
	{
		List<FeesCategoryMaster> GetAll();
		FeesCategoryMaster? GetById(Guid id);
		Guid Create(FeesCategoryMaster category);
		bool Update(FeesCategoryMaster category);
		bool Delete(Guid id);
	}
}
