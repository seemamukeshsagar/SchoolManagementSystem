using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IClassService
	{
		List<ClassMaster> GetAll();
        List<ClassMaster> GetAll(Guid? schoolId);
        ClassMaster? GetById(Guid id);
        Task<ClassMaster?> GetByIdAsync(Guid id);
		Guid Create(ClassMaster cls);
		bool Update(ClassMaster cls);
		bool Delete(Guid id);
		string ClassNameById(Guid id);
		Task<IEnumerable<ClassMaster>> GetAllActiveAsync();
	}
}
