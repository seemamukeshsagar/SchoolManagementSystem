using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface ISystemParametersService
	{
		List<SystemParameters> GetAll();
		SystemParameters? GetById(Guid id);
		Guid Create(SystemParameters item);
		bool Update(SystemParameters item);
		bool Delete(Guid id);
	}
}
