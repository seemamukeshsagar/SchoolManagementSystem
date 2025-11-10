using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IVendorService
	{
		List<VendorMaster> GetAll();
		VendorMaster? GetById(Guid id);
		Guid Create(VendorMaster vendor);
		bool Update(VendorMaster vendor);
		bool Delete(Guid id);
		string VendorNameById(Guid id);
	}
}