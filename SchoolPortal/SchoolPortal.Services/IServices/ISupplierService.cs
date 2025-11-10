using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface ISupplierService
	{
		List<SupplierMaster> GetAll();
		SupplierMaster? GetById(Guid id);
		Guid Create(SupplierMaster supplier);
		bool Update(SupplierMaster supplier);
		bool Delete(Guid id);
		string SupplierNameById(Guid id);
	}
}