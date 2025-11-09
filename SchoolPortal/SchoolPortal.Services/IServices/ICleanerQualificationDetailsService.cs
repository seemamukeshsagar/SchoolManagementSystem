using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface ICleanerQualificationDetailsService
	{
		List<CleanerQualificationDetails> GetAll();
		CleanerQualificationDetails? GetById(Guid id);
		Guid Create(CleanerQualificationDetails item);
		bool Update(CleanerQualificationDetails item);
		bool Delete(Guid id);
	}
}
