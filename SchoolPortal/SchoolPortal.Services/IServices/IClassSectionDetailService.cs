// SchoolPortal.Services/IServices/IClassSectionDetailService.cs
using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IClassSectionDetailService
	{
		IEnumerable<ClassSectionDetail> GetAll();
		ClassSectionDetail? GetById(Guid id);
		Guid Create(ClassSectionDetail entity);
		bool Update(ClassSectionDetail entity);
		bool Delete(Guid id);
		bool ToggleStatus(Guid id, Guid? userId);
		IEnumerable<ClassSectionDetail> GetByClassId(Guid classId);
		IEnumerable<ClassSectionDetail> GetBySectionId(Guid sectionId);
	}
}