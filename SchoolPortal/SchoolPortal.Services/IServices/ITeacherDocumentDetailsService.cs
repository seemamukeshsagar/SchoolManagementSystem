using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface ITeacherDocumentDetailsService
	{
		List<TeacherDocumentDetails> GetAll();
		TeacherDocumentDetails? GetById(Guid id);
		Guid Create(TeacherDocumentDetails item);
		bool Update(TeacherDocumentDetails item);
		bool Delete(Guid id);
	}
}
