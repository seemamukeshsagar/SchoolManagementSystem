using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IVehicleExpenseDetailsService
	{
		List<VehicleExpenseDetails> GetAll();
		List<VehicleExpenseDetails> GetByVehicle(Guid vehicleId);
		List<VehicleExpenseDetails> GetByCompany(Guid companyId);
		List<VehicleExpenseDetails> GetBySchool(Guid schoolId);
		VehicleExpenseDetails? GetById(Guid id);
		Guid Create(VehicleExpenseDetails vehicleExpense);
		bool Update(VehicleExpenseDetails vehicleExpense);
		bool Delete(Guid id);
	}
}