using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Extensions.Logging;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using IsolationLevel = System.Data.IsolationLevel;

// Note: Ensure you have the Proc.Add extension method available
// If not, you'll need to implement it as an extension method for the Proc class

namespace SchoolPortal.Services
{
	public class TimeTablePeriodService : ITimeTablePeriodService
	{
		private readonly ILogger<TimeTablePeriodService> _logger;

		public TimeTablePeriodService(ILogger<TimeTablePeriodService> logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		private static TimeTableClassPeriodDetails? MapTimeTablePeriod(DataRow? r)
		{
			if (r == null) return null;
			
			var period = new TimeTableClassPeriodDetails();

			if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"]?.ToString(), out var id)) 
				period.Id = id;
			if (r.Table.Columns.Contains("ClassId") && Guid.TryParse(r["ClassId"].ToString(), out var classId)) 
				period.ClassId = classId;
			if (r.Table.Columns.Contains("SectionId") && Guid.TryParse(r["SectionId"].ToString(), out var sectionId)) 
				period.SectionId = sectionId;
			if (r.Table.Columns.Contains("SubjectId") && Guid.TryParse(r["SubjectId"].ToString(), out var subjectId)) 
				period.SubjectId = subjectId;
			if (r.Table.Columns.Contains("TeacherId") && Guid.TryParse(r["TeacherId"].ToString(), out var teacherId)) 
				period.TeacherId = teacherId;
			if (r.Table.Columns.Contains("PeriodId") && Guid.TryParse(r["PeriodId"].ToString(), out var periodId)) 
				period.PeriodId = periodId;
			if (r.Table.Columns.Contains("DayOfWeek") && int.TryParse(r["DayOfWeek"].ToString(), out var dayOfWeek)) 
				period.DayOfWeek = dayOfWeek;
			if (r.Table.Columns.Contains("SessionId") && Guid.TryParse(r["SessionId"].ToString(), out var sessionId)) 
				period.SessionId = sessionId;
			if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) 
				period.CompanyId = companyId;
			if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) 
				period.SchoolId = schoolId;
			if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var isActive)) 
				period.IsActive = isActive;
			if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var isDeleted)) 
				period.IsDeleted = isDeleted;
			if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) 
				period.CreatedBy = createdBy;
			if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) 
				period.CreatedDate = createdDate;
			if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) 
				period.ModifiedBy = modifiedBy;
			if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) 
				period.ModifiedDate = modifiedDate;
			period.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? "PEN" : "PEN";
			period.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? "Pending" : "Pending";
			
			// Handle string properties with default values
			period.Status = r.Table.Columns.Contains("Status") && !string.IsNullOrEmpty(r["Status"]?.ToString()) 
				? r["Status"].ToString() 
				: "INC";
				
			period.StatusMessage = r.Table.Columns.Contains("StatusMessage") && !string.IsNullOrEmpty(r["StatusMessage"]?.ToString())
				? r["StatusMessage"].ToString()
				: "Pending";

			// Map related entities if they exist
			if (r.Table.Columns.Contains("SubjectName") && period.Subject == null)
			{
				period.Subject = new SubjectMaster
				{
					Id = period.SubjectId,
					SubjectName = r["SubjectName"]?.ToString() ?? string.Empty
				};
			}
			
			if (r.Table.Columns.Contains("TeacherName") && period.Teacher == null)
			{
				var teacherName = r["TeacherName"]?.ToString() ?? string.Empty;
				var nameParts = teacherName.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
				
				period.Teacher = new TeacherMaster
				{
					Id = period.TeacherId ?? Guid.Empty,
					FirstName = nameParts.Length > 0 ? nameParts[0] : string.Empty,
					LastName = nameParts.Length > 1 ? nameParts[1] : string.Empty,
					// Add other UserMaster properties if available in the row
					Email = r.Table.Columns.Contains("TeacherEmail") ? r["TeacherEmail"]?.ToString() : null
				};
			}
			
			return period;
		}

		public async Task SaveAsync(TimeTableClassPeriodDetails period)
		{
			try
			{
				if (period == null)
					throw new ArgumentNullException(nameof(period));

				var p = new Proc("TimeTablePeriod_Insert");
				p["Id"] = period.Id;
				p["ClassId"] = period.ClassId;
				p["SectionId"] = period.SectionId;
				p["SubjectId"] = period.SubjectId;
				p["TeacherId"] = period.TeacherId ?? (object)DBNull.Value;
				p["PeriodId"] = period.PeriodId;
				p["DayOfWeek"] = period.DayOfWeek;
				p["SessionId"] = period.SessionId;
				p["CompanyId"] = period.CompanyId;
				p["SchoolId"] = period.SchoolId;
				p["IsActive"] = period.IsActive;
				p["CreatedBy"] = period.CreatedBy;

				await Task.Run(() => p.Exec());
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error saving timetable period");
				throw;
			}
		}

		public async Task SaveBulkAsync(IEnumerable<TimeTableClassPeriodDetails> periods)
		{
			if (periods == null)
				throw new ArgumentNullException(nameof(periods));

			using var transaction = new TransactionScope(TransactionScopeOption.Required, 
				new TransactionOptions { IsolationLevel = (System.Transactions.IsolationLevel)IsolationLevel.ReadCommitted }, 
				TransactionScopeAsyncFlowOption.Enabled);

			try
			{
				foreach (var period in periods)
				{
					await SaveAsync(period);
				}
				transaction.Complete();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error saving bulk timetable periods");
				throw;
			}
		}

		public async Task<bool> DeleteByClassSectionAndAcademicYearAsync(Guid classId, Guid sectionId, Guid academicYearId, Guid userId)
		{
			try
			{
				var p = new Proc("TimeTablePeriod_DeleteByClassSectionAndAcademicYear");
				p["ClassId"] = classId;
				p["SectionId"] = sectionId;
				p["AcademicYearId"] = academicYearId;
				p["ModifiedBy"] = userId;

				await Task.Run(() => p.Exec());
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error deleting timetable periods");
				return false;
			}
		}

		public async Task<IEnumerable<TimeTableClassPeriodDetails>> GetByClassSectionAndAcademicYearAsync(
			Guid classId, Guid sectionId, Guid academicYearId)
		{
			var result = new List<TimeTableClassPeriodDetails>();
			
			try
			{
				var p = new Proc("TimeTablePeriod_GetByClassSectionAndAcademicYear");
				p["ClassId"] = classId;
				p["SectionId"] = sectionId;
				p["AcademicYearId"] = academicYearId;

				var dt = new DataTable();
				await Task.Run(() => p.Exec(dt));

				foreach (DataRow row in dt.Rows)
				{
					var period = MapTimeTablePeriod(row);
					if (period != null)
					{
						result.Add(period);
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting timetable periods by class, section and academic year");
			}
			return result;
		}

		public TimeTableClassPeriodDetails? GetById(Guid id)
		{
			try
			{
				var p = new Proc("TimeTablePeriod_GetById");
				p["Id"] = id;

				var dt = new DataTable();
				p.Exec(dt);

				if (dt.Rows.Count == 0)
					return null;

				return MapTimeTablePeriod(dt.Rows[0]);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error getting timetable period by ID: {id}");
				throw;
			}
		}

		public async Task<TimeTableClassPeriodDetails?> GetByIdAsync(Guid id)
		{
			try
			{
				var p = new Proc("TimeTablePeriod_GetById");
				p["Id"] = id;

				var dt = new DataTable();
				await Task.Run(() => p.Exec(dt));

				if (dt.Rows.Count == 0)
					return null;

				return MapTimeTablePeriod(dt.Rows[0]);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error getting timetable period by ID: {id}");
				throw;
			}
		}

		public async Task<IEnumerable<TimeTableClassPeriodDetails>> GetAllAsync()
		{
			try
			{
				var p = new Proc("TimeTablePeriod_GetAll");
				var dt = new DataTable();
				await Task.Run(() => p.Exec(dt));

				var result = new List<TimeTableClassPeriodDetails>();
				foreach (DataRow row in dt.Rows)
				{
					var period = MapTimeTablePeriod(row);
					if (period != null)
					{
						result.Add(period);
					}
				}
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting all timetable periods");
				throw;
			}
		}

		public async Task<TimeTableClassPeriodDetails> CreateAsync(TimeTableClassPeriodDetails period)
		{
			if (period == null)
				throw new ArgumentNullException(nameof(period));

			try
			{
				period.Id = Guid.NewGuid();
				await SaveAsync(period);
				return period;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating timetable period");
				throw;
			}
		}

		public async Task<bool> UpdateAsync(TimeTableClassPeriodDetails period)
		{
			if (period == null)
				throw new ArgumentNullException(nameof(period));

			try
			{
				await SaveAsync(period);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			try
			{
				var p = new Proc("TimeTablePeriod_Delete");
				p["Id"] = id;
				await Task.Run(() => p.Exec());
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error deleting timetable period with ID: {id}");
				return false;
			}
		}

		public async Task<IEnumerable<TimeTableClassPeriodDetails>> GetByTeacherIdAsync(Guid teacherId) 
		{
			try
			{
				var p = new Proc("TimeTablePeriod_GetByTeacherId");
				p["TeacherId"] = teacherId;
				var dt = new DataTable();
				await Task.Run(() => p.Exec(dt));

				var result = new List<TimeTableClassPeriodDetails>();
				foreach (DataRow row in dt.Rows)
				{
					var period = MapTimeTablePeriod(row);
					if (period != null)
					{
						result.Add(period);
					}
				}
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error getting timetable periods for teacher ID: {teacherId}");
				throw;
			}
		}

		public async Task<IEnumerable<TimeTableClassPeriodDetails>> GetBySubjectIdAsync(Guid subjectId) 
		{
			try
			{
				var p = new Proc("TimeTablePeriod_GetBySubjectId");
				p["SubjectId"] = subjectId;
				var dt = new DataTable();
				await Task.Run(() => p.Exec(dt));

				var result = new List<TimeTableClassPeriodDetails>();
				foreach (DataRow row in dt.Rows)
				{
					var period = MapTimeTablePeriod(row);
					if (period != null)
					{
						result.Add(period);
					}
				}
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error getting timetable periods for subject ID: {subjectId}");
				throw;
			}
		}

		public async Task<bool> IsTeacherAvailableAsync(Guid teacherId, int dayOfWeek, TimeSpan startTime, TimeSpan endTime, Guid? excludePeriodId = null)
		{
			try
			{
				var p = new Proc("TimeTablePeriod_IsTeacherAvailable");
				p["TeacherId"] = teacherId;
				p["DayOfWeek"] = dayOfWeek;
				p["StartTime"] = startTime;
				p["EndTime"] = endTime;
				p["ExcludePeriodId"] = excludePeriodId ?? (object)DBNull.Value;

				var dt = new DataTable();
				await Task.Run(() => p.Exec(dt));

				return dt.Rows.Count == 0;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error checking teacher availability for teacher ID: {teacherId}");
				throw;
			}
		}

		public async Task<bool> IsClassroomAvailableAsync(Guid classroomId, int dayOfWeek, TimeSpan startTime, TimeSpan endTime, Guid? excludePeriodId = null)
		{
			try
			{
				var p = new Proc("TimeTablePeriod_IsClassroomAvailable");
				p["ClassroomId"] = classroomId;
				p["DayOfWeek"] = dayOfWeek;
				p["StartTime"] = startTime;
				p["EndTime"] = endTime;
				p["ExcludePeriodId"] = excludePeriodId ?? (object)DBNull.Value;

				var dt = new DataTable();
				await Task.Run(() => p.Exec(dt));

				return dt.Rows.Count == 0;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error checking classroom availability for classroom ID: {classroomId}");
				throw;
			}
		}

		public async Task<TimeTableClassPeriodDetails> GetBySetupIdAsync(Guid setupId)
		{
			try
			{
				var p = new Proc("TimeTablePeriod_GetBySetupId");
				p["SetupId"] = setupId;

				var dt = new DataTable();
				await Task.Run(() => p.Exec(dt));

				if (dt.Rows.Count == 0)
					throw new InvalidOperationException($"No timetable period found for setup ID: {setupId}");

				var result = MapTimeTablePeriod(dt.Rows[0]);
				if (result == null)
					throw new InvalidOperationException($"Mapping failed for timetable period with setup ID: {setupId}");

				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error getting timetable period by setup ID: {setupId}");
				throw;
			}
		}
	}
}