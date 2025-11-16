using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
	public interface IAttendanceReasonMasterService
	{
		List<AttendanceReasonMaster> GetAll();
		AttendanceReasonMaster? GetById(Guid id);
		Guid Create(AttendanceReasonMaster attendanceReason);
		bool Update(AttendanceReasonMaster attendanceReason);
		bool Delete(Guid id);
	}
}
