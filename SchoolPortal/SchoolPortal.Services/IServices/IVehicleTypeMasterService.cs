using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IVehicleTypeMasterService
	{
		List<VehicleTypeMaster> GetAll();
		List<VehicleTypeMaster> GetByCompany(Guid companyId);
		List<VehicleTypeMaster> GetBySchool(Guid schoolId);
		VehicleTypeMaster? GetById(Guid id);
		Guid Create(VehicleTypeMaster vehicleType);
		bool Update(VehicleTypeMaster vehicleType);
		bool Delete(Guid id);
		string VehicleTypeNameById(Guid id);
	}
}