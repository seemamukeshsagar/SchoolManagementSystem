using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IDriverQualificationDetailsService
	{
		List<DriverQualificationDetails> GetAll();
		DriverQualificationDetails? GetById(Guid id);
		Guid Create(DriverQualificationDetails item);
		bool Update(DriverQualificationDetails item);
		bool Delete(Guid id);
	}
}
