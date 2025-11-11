using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IBookCategoryService
	{
		List<BookCategoryMaster> GetAll();
		BookCategoryMaster? GetById(Guid id);
		Guid Create(BookCategoryMaster bookCategory);
		bool Update(BookCategoryMaster bookCategory);
		bool Delete(Guid id);
		string CategoryNameById(Guid id);
	}
}