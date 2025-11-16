using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IQualificationMasterService
	{
		List<QualificationMaster> GetAll();
		QualificationMaster? GetById(Guid id);
		Guid Create(QualificationMaster qualification);
		bool Update(QualificationMaster qualification);
		bool Delete(Guid id);
	}
}
