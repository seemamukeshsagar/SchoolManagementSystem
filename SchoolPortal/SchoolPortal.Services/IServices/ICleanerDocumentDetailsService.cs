using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface ICleanerDocumentDetailsService
	{
		List<CleanerDocumentDetails> GetAll();
		CleanerDocumentDetails? GetById(Guid id);
		Guid Create(CleanerDocumentDetails item);
		bool Update(CleanerDocumentDetails item);
		bool Delete(Guid id);
	}
}
