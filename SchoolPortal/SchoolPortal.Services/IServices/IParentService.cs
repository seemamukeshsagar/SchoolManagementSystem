using System;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IParentService
	{
		void CreateForStudent(
			Guid studentId,
			Guid schoolId,
			Guid companyId,
			Guid createdBy,
			string? parentFirstName,
			string? parentLastName,
			DateTime? parentDOB,
			Guid? relationTypeId,
			Guid? qualificationId,
			string? occupation,
			decimal? annualIncome,
			Guid? designationId,
			string? phone,
			string? email,
			string? address1,
			string? address2,
			Guid? countryId,
			Guid? stateId,
			Guid? cityId,
			string? zipCode,
			bool isActive
		);
		
		ParentMaster? GetByStudentId(Guid studentId);
	}
}
