using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IHolidayMasterService
	{
		List<HolidayMaster> GetAll();
		HolidayMaster? GetById(Guid id);
		Guid Create(HolidayMaster holiday);
		bool Update(HolidayMaster holiday);
		bool Delete(Guid id);
	}
}
