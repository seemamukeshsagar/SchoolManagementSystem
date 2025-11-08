using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IHolidayTypeMasterService
	{
		List<HolidayTypeMaster> GetAll();
		HolidayTypeMaster? GetById(Guid id);
		Guid Create(HolidayTypeMaster holidayType);
		bool Update(HolidayTypeMaster holidayType);
		bool Delete(Guid id);
	}
}
