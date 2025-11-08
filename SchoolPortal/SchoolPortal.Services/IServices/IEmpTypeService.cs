using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IEmpTypeService
	{
		List<EmpTypeMaster> GetAll();
		EmpTypeMaster? GetById(Guid id);
		Guid Create(EmpTypeMaster empType);
		bool Update(EmpTypeMaster empType);
		bool Delete(Guid id);
	}
}

