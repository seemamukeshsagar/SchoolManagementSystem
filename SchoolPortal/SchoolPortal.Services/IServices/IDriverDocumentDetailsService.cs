using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IDriverDocumentDetailsService
	{
		List<DriverDocumentDetails> GetAll();
		DriverDocumentDetails? GetById(Guid id);
		Guid Create(DriverDocumentDetails item);
		bool Update(DriverDocumentDetails item);
		bool Delete(Guid id);
	}
}
