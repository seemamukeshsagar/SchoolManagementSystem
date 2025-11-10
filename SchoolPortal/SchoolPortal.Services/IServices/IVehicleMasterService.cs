using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IVehicleMasterService
	{
		List<VehicleMaster> GetAll();
		VehicleMaster? GetById(Guid id);
		Guid Create(VehicleMaster vehicle);
		bool Update(VehicleMaster vehicle);
		bool Delete(Guid id);
		string VehicleNumberById(Guid id);
	}
}