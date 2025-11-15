using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface ICategoryMasterService
	{
		List<CategoryMaster> GetAll();
		CategoryMaster? GetById(Guid id);
		Guid Create(CategoryMaster category);
		bool Update(CategoryMaster category);
		bool Delete(Guid id);
	}
}
