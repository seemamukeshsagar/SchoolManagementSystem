using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using Microsoft.Extensions.Logging;

namespace SchoolPortal.Services
{
	public class TimeTablePeriodService : ITimeTablePeriodService
	{
		private readonly ILogger<TimeTablePeriodService> _logger;

		public TimeTablePeriodService(ILogger<TimeTablePeriodService> logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<IEnumerable<TimeTableClassPeriodDetails>> GetAllAsync()
		{
			try
			{
				return await Task.Run(() => 
				{
					var list = new List<TimeTableClassPeriodDetails>();
					using (var p = new Proc("TimeTablePeriod_GetAll"))
					{
						var dt = new DataTable();
						p.Exec(dt);
						
						foreach (DataRow r in dt.Rows)
						{
							list.Add(MapTimeTablePeriod(r));
						}
					}
					return (IEnumerable<TimeTableClassPeriodDetails>)list;
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting all timetable periods asynchronously");
				throw;
			}
		}

		public TimeTableClassPeriodDetails GetById(Guid id)
		{
			try
			{
				using (var p = new Proc("TimeTablePeriod_GetById"))
				{
					p["@Id"] = id;
					var dt = new DataTable();
					p.Exec(dt);
					
					if (dt.Rows.Count == 0) 
						return null;
						
					return MapTimeTablePeriod(dt.Rows[0]);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error getting timetable period with ID: {id}");
				throw;
			}
		}

		public async Task<TimeTableClassPeriodDetails> GetByIdAsync(Guid id)
		{
			return await Task.FromResult(GetById(id));
		}

		public async Task<TimeTableClassPeriodDetails> CreateAsync(TimeTableClassPeriodDetails period)
		{
			if (period == null)
				throw new ArgumentNullException(nameof(period));

			try
			{
				return await Task.Run(() => 
				{
					var newId = Guid.NewGuid();
					using (var p = new Proc("TimeTablePeriod_Create"))
					{
						p["@Id"] = newId;
						p["@ClassId"] = period.ClassId;
						p["@SectionId"] = period.SectionId;
						p["@SubjectId"] = period.SubjectId;
						p["@TeacherId"] = period.TeacherId;
						p["@DayOfWeek"] = period.DayOfWeek;
						p["@PeriodStartTime"] = period.PeriodStartTime; // Updated property name
						p["@PeriodEndTime"] = period.PeriodEndTime;     // Updated property name
						p["@IsActive"] = period.IsActive;
						p["@CreatedBy"] = period.CreatedBy;
						p["@CreatedDate"] = period.CreatedDate;         // Updated property name
						p["@ModifiedBy"] = period.ModifiedBy;
						p["@ModifiedDate"] = period.ModifiedDate;       // Updated property name
						
						var dt = new DataTable();
						p.Exec(dt);
						
						if (dt.Rows.Count > 0)
						{
							return MapTimeTablePeriod(dt.Rows[0]);
						}
						
						return period;
					}
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error creating timetable period: {period.Id}");
				throw;
			}
		}

		public async Task<bool> UpdateAsync(TimeTableClassPeriodDetails period)
		{
			if (period == null)
				throw new ArgumentNullException(nameof(period));

			try
			{
				return await Task.Run(() => 
				{
					using (var p = new Proc("TimeTablePeriod_Update"))
					{
						p["@Id"] = period.Id;
						p["@ClassId"] = period.ClassId;
						p["@SectionId"] = period.SectionId;
						p["@SubjectId"] = period.SubjectId;
						p["@TeacherId"] = period.TeacherId;
						p["@DayOfWeek"] = period.DayOfWeek;
						p["@PeriodStartTime"] = period.PeriodStartTime; // Updated property name
						p["@PeriodEndTime"] = period.PeriodEndTime;     // Updated property name
						p["@IsActive"] = period.IsActive;
						p["@ModifiedBy"] = period.ModifiedBy;
						p["@ModifiedDate"] = period.ModifiedDate;       // Updated property name
						
						// Use Exec instead of ExecNonQuery
						var dt = new DataTable();
						p.Exec(dt);
						return dt.Rows.Count > 0;
					}
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error updating timetable period: {period.Id}");
				throw;
			}
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			try
			{
				return await Task.Run(() => 
				{
					using (var p = new Proc("TimeTablePeriod_Delete"))
					{
						p["@Id"] = id;
						var dt = new DataTable();
						p.Exec(dt);
						return dt.Rows.Count > 0;
					}
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error deleting timetable period: {id}");
				throw;
			}
		}

		public async Task SaveAsync(TimeTableClassPeriodDetails period)
		{
			if (period == null)
				throw new ArgumentNullException(nameof(period));

			try
			{
				if (period.Id == Guid.Empty)
				{
					period.Id = Guid.NewGuid();
					period.CreatedDate = DateTime.UtcNow;
					Create(period);
				}
				else
				{
					period.ModifiedDate = DateTime.UtcNow;
					if (!Update(period))
					{
						throw new InvalidOperationException("Failed to update timetable period");
					}
				}
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

			foreach (var period in periods)
			{
				await SaveAsync(period);
			}
		}

		public async Task<bool> DeleteByClassSectionAndAcademicYearAsync(Guid classId, Guid sectionId, Guid academicYearId, Guid userId)
		{
			try
			{
				using (var p = new Proc("TimeTablePeriod_DeleteByClassSectionAndAcademicYear"))
				{
					p["@ClassId"] = classId;
					p["@SectionId"] = sectionId;
					p["@AcademicYearId"] = academicYearId;
					p["@ModifiedBy"] = userId;
					
					var dt = new DataTable();
					p.Exec(dt);
					return true;
				}
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
			try
			{
				var periods = new List<TimeTableClassPeriodDetails>();
				using (var p = new Proc("TimeTablePeriod_GetByClassSectionAndAcademicYear"))
				{
					p["@ClassId"] = classId;
					p["@SectionId"] = sectionId;
					p["@AcademicYearId"] = academicYearId;
					
					var dt = new DataTable();
					p.Exec(dt);
					
					foreach (DataRow row in dt.Rows)
					{
						periods.Add(MapTimeTablePeriod(row));
					}
				}
				return periods;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error getting timetable periods for Class: {classId}, Section: {sectionId}, AcademicYear: {academicYearId}");
				throw;
			}
		}

		public async Task<IEnumerable<TimeTableClassPeriodDetails>> GetByTeacherIdAsync(Guid teacherId)
		{
			try
			{
				var periods = new List<TimeTableClassPeriodDetails>();
				using (var p = new Proc("TimeTablePeriod_GetByTeacherId"))
				{
					p["@TeacherId"] = teacherId;
					
					var dt = new DataTable();
					p.Exec(dt);
					
					foreach (DataRow row in dt.Rows)
					{
						periods.Add(MapTimeTablePeriod(row));
					}
				}
				return periods;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error getting timetable periods for Teacher: {teacherId}");
				throw;
			}
		}

		public async Task<IEnumerable<TimeTableClassPeriodDetails>> GetBySubjectIdAsync(Guid subjectId)
		{
			try
			{
				var periods = new List<TimeTableClassPeriodDetails>();
				using (var p = new Proc("TimeTablePeriod_GetBySubjectId"))
				{
					p["@SubjectId"] = subjectId;
					
					var dt = new DataTable();
					p.Exec(dt);
					
					foreach (DataRow row in dt.Rows)
					{
						periods.Add(MapTimeTablePeriod(row));
					}
				}
				return periods;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error getting timetable periods for Subject: {subjectId}");
				throw;
			}
		}

		public async Task<bool> IsTeacherAvailableAsync(Guid teacherId, int dayOfWeek, TimeSpan startTime, TimeSpan endTime, Guid? excludePeriodId = null)
		{
			try
			{
				using (var p = new Proc("CheckTeacherAvailability"))
				{
					p["@TeacherId"] = teacherId;
					p["@DayOfWeek"] = dayOfWeek;
					p["@StartTime"] = startTime;
					p["@EndTime"] = endTime;
					p["@ExcludePeriodId"] = excludePeriodId ?? (object)DBNull.Value;

					var dt = new DataTable();
					p.Exec(dt);
					var result = dt.Rows.Count > 0 ? dt.Rows[0][0] : null;
					return result != null && Convert.ToBoolean(result);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error checking teacher availability");
				return false; // Default to false in case of error
			}
		}

		public async Task<bool> IsClassroomAvailableAsync(Guid classroomId, int dayOfWeek, TimeSpan startTime, TimeSpan endTime, Guid? excludePeriodId = null)
		{
			try
			{
				using (var p = new Proc("CheckClassroomAvailability"))
				{
					p["@ClassroomId"] = classroomId;
					p["@DayOfWeek"] = dayOfWeek;
					p["@StartTime"] = startTime;
					p["@EndTime"] = endTime;
					p["@ExcludePeriodId"] = excludePeriodId ?? (object)DBNull.Value;

					var dt = new DataTable();
					p.Exec(dt);
					var result = dt.Rows.Count > 0 ? dt.Rows[0][0] : null;
					return result != null && Convert.ToBoolean(result);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error checking classroom availability");
				return false; // Default to false in case of error
			}
		}

		private static TimeTableClassPeriodDetails MapTimeTablePeriod(DataRow r)
		{
			var period = new TimeTableClassPeriodDetails();
			
			if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) 
				period.Id = id;
				
			if (r.Table.Columns.Contains("ClassId") && r["ClassId"] != DBNull.Value && Guid.TryParse(r["ClassId"].ToString(), out var classId)) 
				period.ClassId = classId;
				
			if (r.Table.Columns.Contains("SectionId") && r["SectionId"] != DBNull.Value && Guid.TryParse(r["SectionId"].ToString(), out var sectionId)) 
				period.SectionId = sectionId;
				
			if (r.Table.Columns.Contains("SubjectId") && r["SubjectId"] != DBNull.Value && Guid.TryParse(r["SubjectId"].ToString(), out var subjectId)) 
				period.SubjectId = subjectId;
				
			if (r.Table.Columns.Contains("PeriodId") && r["PeriodId"] != DBNull.Value && Guid.TryParse(r["PeriodId"].ToString(), out var periodId)) 
				period.PeriodId = periodId;
				
			if (r.Table.Columns.Contains("DayOfWeek") && int.TryParse(r["DayOfWeek"].ToString(), out var dayOfWeek)) 
				period.DayOfWeek = dayOfWeek;
				
			if (r.Table.Columns.Contains("SessionId") && r["SessionId"] != DBNull.Value && Guid.TryParse(r["SessionId"].ToString(), out var sessionId)) 
				period.SessionId = sessionId;
				
			if (r.Table.Columns.Contains("CompanyId") && r["CompanyId"] != DBNull.Value && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) 
				period.CompanyId = companyId;
				
			if (r.Table.Columns.Contains("SchoolId") && r["SchoolId"] != DBNull.Value && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) 
				period.SchoolId = schoolId;
				
			if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var isActive)) 
				period.IsActive = isActive;
				
			if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var isDeleted)) 
				period.IsDeleted = isDeleted;
				
			if (r.Table.Columns.Contains("CreatedBy") && r["CreatedBy"] != DBNull.Value && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) 
				period.CreatedBy = createdBy;
				
			if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) 
				period.CreatedDate = createdDate;
				
			if (r.Table.Columns.Contains("ModifiedBy") && r["ModifiedBy"] != DBNull.Value && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) 
				period.ModifiedBy = modifiedBy;
				
			if (r.Table.Columns.Contains("ModifiedDate") && r["ModifiedDate"] != DBNull.Value && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) 
				period.ModifiedDate = modifiedDate;
				
			if (r.Table.Columns.Contains("Status")) 
				period.Status = r["Status"]?.ToString();
				
			if (r.Table.Columns.Contains("StatusMessage")) 
				period.StatusMessage = r["StatusMessage"]?.ToString();
				
			if (r.Table.Columns.Contains("IsBreak") && bool.TryParse(r["IsBreak"].ToString(), out var isBreak))
				period.IsBreak = isBreak;
				
			if (r.Table.Columns.Contains("BreakName"))
				period.BreakName = r["BreakName"]?.ToString();

			return period;
		}

		// Add this method to the TimeTablePeriodService class
		public async Task<TimeTableClassPeriodDetails> GetBySetupIdAsync(Guid setupId)
		{
			try
			{
				using (var p = new Proc("TimeTablePeriod_GetBySetupId"))
				{
					p["@SetupId"] = setupId;
					var dt = new DataTable();
					p.Exec(dt);
					
					if (dt.Rows.Count == 0)
						return null;
						
					return MapTimeTablePeriod(dt.Rows[0]);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error getting timetable period by setup ID: {setupId}");
				throw;
			}
		}

		private void Create(TimeTableClassPeriodDetails period)
{
	if (period == null)
		throw new ArgumentNullException(nameof(period));

	try
	{
		using (var p = new Proc("TimeTablePeriod_Create"))
		{
			p["@Id"] = period.Id;
			p["@ClassId"] = period.ClassId;
			p["@SectionId"] = period.SectionId;
			p["@SubjectId"] = period.SubjectId;
			p["@TeacherId"] = period.TeacherId;
			p["@DayOfWeek"] = period.DayOfWeek;
			p["@PeriodStartTime"] = period.PeriodStartTime;
			p["@PeriodEndTime"] = period.PeriodEndTime;
			p["@IsActive"] = period.IsActive;
			p["@CreatedBy"] = period.CreatedBy;
			p["@CreatedDate"] = period.CreatedDate;
			p["@ModifiedBy"] = period.ModifiedBy;
			p["@ModifiedDate"] = period.ModifiedDate;
			
			var dt = new DataTable();
			p.Exec(dt);
		}
	}
	catch (Exception ex)
	{
		_logger.LogError(ex, "Error creating timetable period");
		throw;
	}
}

// 5. Add the missing Update method
private bool Update(TimeTableClassPeriodDetails period)
{
	if (period == null)
		throw new ArgumentNullException(nameof(period));

	try
	{
		using (var p = new Proc("TimeTablePeriod_Update"))
		{
			p["@Id"] = period.Id;
			p["@ClassId"] = period.ClassId;
			p["@SectionId"] = period.SectionId;
			p["@SubjectId"] = period.SubjectId;
			p["@TeacherId"] = period.TeacherId;
			p["@DayOfWeek"] = period.DayOfWeek;
			p["@PeriodStartTime"] = period.PeriodStartTime;
			p["@PeriodEndTime"] = period.PeriodEndTime;
			p["@IsActive"] = period.IsActive;
			p["@ModifiedBy"] = period.ModifiedBy;
			p["@ModifiedDate"] = period.ModifiedDate;
			
			var dt = new DataTable();
			p.Exec(dt);
			return dt.Rows.Count > 0;
		}
	}
	catch (Exception ex)
	{
		_logger.LogError(ex, $"Error updating timetable period: {period.Id}");
		throw;
	}
}



// Implement the synchronous methods
	public IEnumerable<TimeTableClassPeriodDetails> GetAll()
	{
		try
		{
			var list = new List<TimeTableClassPeriodDetails>();
			using (var p = new Proc("TimeTablePeriod_GetAll"))
			{
				var dt = new DataTable();
				p.Exec(dt);
				
				foreach (DataRow r in dt.Rows)
				{
					list.Add(MapTimeTablePeriod(r));
				}
			}
			return list;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error getting all timetable periods");
			throw;
		}
	}	

	public bool Delete(Guid id, Guid modifiedBy)
	{
		try
		{
			using (var p = new Proc("TimeTablePeriod_Delete"))
			{
				p["@Id"] = id;
				p["@ModifiedBy"] = modifiedBy;
				
				var dt = new DataTable();
				p.Exec(dt);
				return dt.Rows.Count > 0;
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, $"Error deleting timetable period: {id}");
			throw;
		}
	}

	
	}
}