using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IRoleMasterService
	{
		IEnumerable<RoleMaster> GetAll();
		RoleMaster? GetById(Guid id);
		Guid Create(RoleMaster entity);
		bool Update(RoleMaster entity);
		bool Delete(Guid id);
		RoleMaster? GetByRoleName(string? roleName = null);
		(IEnumerable<RoleMaster> items, int totalCount) GetRoles(
        int pageNumber, 
        int pageSize, 
        string sortColumn = null, 
        string sortDirection = "asc", 
        string searchTerm = null);
		
	}
}