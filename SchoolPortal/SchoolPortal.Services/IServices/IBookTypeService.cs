using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IBookTypeService
	{
		List<BookTypeMaster> GetAll();
		BookTypeMaster? GetById(Guid id);
		Guid Create(BookTypeMaster bookType);
		bool Update(BookTypeMaster bookType);
		bool Delete(Guid id);
		string BookTypeNameById(Guid id);
	}
}
