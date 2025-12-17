using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Entities.ViewModels;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
	/// <summary>
	/// Service for managing student-related operations including CRUD and attendance
	/// </summary>
	public class StudentService : IStudentService, IDisposable
	{
		private readonly ILogger<StudentService> _logger;
		private bool disposedValue;

		public StudentService(ILogger<StudentService> logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		private static StudentMaster MapStudent(DataRow r)
		{
			var student = new StudentMaster();
			if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) student.Id = id;
			if (r.Table.Columns.Contains("RollNumber") && Guid.TryParse(r["RollNumber"].ToString(), out var rollNumber)) student.RollNumber = rollNumber;
			student.FirstName = r.Table.Columns.Contains("FirstName") ? r["FirstName"].ToString() ?? string.Empty : string.Empty;
			student.LastName = r.Table.Columns.Contains("LastName") ? r["LastName"].ToString() ?? string.Empty : string.Empty;
			student.Email = r.Table.Columns.Contains("Email") ? r["Email"].ToString() ?? string.Empty : string.Empty;
			student.Phone = r.Table.Columns.Contains("Phone") ? r["Phone"].ToString() ?? string.Empty : string.Empty;
			if (r.Table.Columns.Contains("DOB") && DateTime.TryParse(r["DOB"].ToString(), out var dob)) student.DOB = dob;
			if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var isActive)) student.IsActive = isActive;
			if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var isDeleted)) student.IsDeleted = isDeleted;
			if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) student.CompanyId = companyId;
			if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) student.SchoolId = schoolId;
			if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) student.CreatedBy = createdBy;
			if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) student.CreatedDate = createdDate;
			if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) student.ModifiedBy = modifiedBy;
			if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) student.ModifiedDate = modifiedDate;
			student.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
			student.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
			return student;
		}

		public async Task<bool> BulkUpdateStatusAsync(IEnumerable<Guid> studentIds, bool isActive)
		{
			try
			{
				int totalUpdated = 0;
				foreach (var id in studentIds)
				{
					var p = new Proc("Student_UpdateStatus");
					p["@Id"] = id;
					p["@IsActive"] = isActive;
					p.Exec();
					var ret = p.Parameters["@RETURN_VALUE"].Value;
					int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
					if (code == 1) totalUpdated++;
				}
				return totalUpdated > 0;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error updating student statuses");
				throw;
			}
		}

		public async Task<bool> CategoryExistsAsync(Guid categoryId)
		{
			try
			{
				var p = new Proc("StudentCategory_Exists");
				p["@CategoryId"] = categoryId;
				var dt = new DataTable();
				p.Exec(dt);
				return dt.Rows.Count > 0 && Convert.ToBoolean(dt.Rows[0][0]);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error checking if category exists");
				throw;
			}
		}

		public Guid Create(StudentMaster student)
		{
			try
			{
				var p = new Proc("Student_Create");
				p["@FirstName"] = student.FirstName;
				p["@LastName"] = student.LastName;
				p["@DOB"] = student.DOB;
				p["@Email"] = student.Email;
				p["@Phone"] = student.Phone;
				p["@IsActive"] = student.IsActive;
				p["@CompanyId"] = student.CompanyId;
				p["@SchoolId"] = student.SchoolId;
				p["@CreatedBy"] = student.CreatedBy;

				var dt = new DataTable();
				p.Exec(dt);
				
				if (dt.Rows.Count > 0 && dt.Rows[0]["Id"] != DBNull.Value)
				{
					return new Guid(dt.Rows[0]["Id"].ToString());
				}
				return Guid.Empty;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating student");
				throw;
			}
		}

		public async Task<Guid> CreateAsync(StudentMaster student)
		{
			// Since we're using sync stored procedures, we'll wrap the sync call in Task.Run
			return await Task.Run(() => Create(student));
		}

		public async Task<Guid> CreateStudentAttendanceAsync(StudentAttendanceDetails attendance)
		{
			try
			{
				var p = new Proc("StudentAttendance_Create");
				p["@StudentId"] = attendance.StudentGUID;
				p["@ClassId"] = attendance.ClassId;
				p["@SectionId"] = attendance.SectionId;
				p["@AttendanceDate"] = attendance.AttendenceDate;
				p["@Status"] = attendance.Status;
				p["@Reason"] = attendance.AttendanceReasonId;
				p["@Remarks"] = attendance.StatusMessage;
				p["@IsActive"] = attendance.IsActive;
				p["@CreatedBy"] = attendance.CreatedBy;

				var dt = new DataTable();
				p.Exec(dt);
				
				if (dt.Rows.Count > 0 && dt.Rows[0]["Id"] != DBNull.Value)
				{
					return new Guid(dt.Rows[0]["Id"].ToString());
				}
				return Guid.Empty;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating student attendance");
				throw;
			}
		}

		public bool Delete(Guid id)
		{
			try
			{
				var p = new Proc("Student_Delete");
				p["@Id"] = id;
				p.Exec();
				var ret = p.Parameters["@RETURN_VALUE"].Value;
				int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
				return code == 1;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error deleting student with ID {id}");
				throw;
			}
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			return await Task.Run(() => Delete(id));
		}

		public List<StudentMaster> GetAll(Guid? schoolId)
		{
			try
			{
				var p = new Proc("Student_GetAll");
				if (schoolId.HasValue)
				{
					p = new Proc("Student_GetBySchoolId");
					p["@SchoolId"] = schoolId.Value;
				}
				
				var dt = new DataTable();
				p.Exec(dt);
				
				var list = new List<StudentMaster>();
				foreach (DataRow row in dt.Rows)
				{
					list.Add(MapStudent(row));
				}
				return list;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving all students");
				throw;
			}
		}

		public async Task<List<StudentMaster>> GetAllAsync(Guid? schoolId)
		{
			return await Task.Run(() => GetAll(schoolId));
		}

		public StudentMaster GetById(Guid id)
		{
			try
			{
				var p = new Proc("Student_GetById");
				p["@Id"] = id;
				var dt = new DataTable();
				p.Exec(dt);
				
				if (dt.Rows.Count == 0) return null;
				return MapStudent(dt.Rows[0]);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error retrieving student with ID {id}");
				throw;
			}
		}

		public async Task<StudentMaster?> GetByIdAsync(Guid id)
		{
			return await Task.Run(() => GetById(id));
		}

		public async Task<StudentAttendanceDetails?> GetStudentAttendanceByIdAsync(Guid id)
		{
			try
			{
				var p = new Proc("StudentAttendance_GetById");
				p["@Id"] = id;
				var dt = new DataTable();
				p.Exec(dt);
				
				if (dt.Rows.Count == 0) return null;
				
				var attendance = new StudentAttendanceDetails();
				var row = dt.Rows[0];

                // Safely parse GUID values with null checks and TryParse
                if (row["Id"] != DBNull.Value && Guid.TryParse(row["Id"]?.ToString(), out var attendanceId))
                    attendance.Id = attendanceId;
                    
                if (row["StudentId"] != DBNull.Value && Guid.TryParse(row["StudentId"]?.ToString(), out var studentId))
                    attendance.StudentGUID = studentId;
                    
                if (row["ClassId"] != DBNull.Value && Guid.TryParse(row["ClassId"]?.ToString(), out var classId))
                    attendance.ClassId = classId;
                    
                if (row["SectionId"] != DBNull.Value && Guid.TryParse(row["SectionId"]?.ToString(), out var sectionId))
                    attendance.SectionId = sectionId;

                // Handle AttendanceDate
                if (row["AttendanceDate"] != DBNull.Value && DateTime.TryParse(row["AttendanceDate"]?.ToString(), out var attendanceDate))
                    attendance.AttendenceDate = attendanceDate;

                // Handle Status (nullable string)
                attendance.Status = row["Status"]?.ToString();

                // Handle AttendanceReasonId with safe parsing
                if (row["Reason"] != DBNull.Value && Guid.TryParse(row["Reason"]?.ToString(), out var reasonId))
                    attendance.AttendanceReasonId = reasonId;

                // Handle IsActive with safe parsing
                if (row["IsActive"] != DBNull.Value && bool.TryParse(row["IsActive"]?.ToString(), out var isActive))
                    attendance.IsActive = isActive;
                
                return attendance;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error retrieving student attendance with ID {id}");
				throw;
			}
		}

		public async Task<StudentStats> GetStudentStatisticsAsync(Guid? schoolId)
		{
			try
			{
				var p = new Proc("Student_GetStatistics");
				if (schoolId.HasValue)
				{
					p["@SchoolId"] = schoolId.Value;
				}
				
				var dt = new DataTable();
				p.Exec(dt);
				
				if (dt.Rows.Count == 0) return new StudentStats();
				
				return new StudentStats
				{
					TotalStudents = dt.Rows[0]["TotalStudents"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["TotalStudents"]) : 0,
					MaleCount = dt.Rows[0]["MaleCount"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["MaleCount"]) : 0,
					FemaleCount = dt.Rows[0]["FemaleCount"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["FemaleCount"]) : 0,
					ActiveStudents = dt.Rows[0]["ActiveStudents"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["ActiveStudents"]) : 0
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving student statistics");
				throw;
			}
		}

		public async Task<IEnumerable<StudentMaster>> SearchStudentsAsync(StudentSearchCriteria criteria)
		{
			try
			{
				var p = new Proc("Student_Search");
				//if (!string.IsNullOrEmpty(criteria..Name)) p["@Name"] = $"%{criteria.Name}%";
				if (criteria.ClassId.HasValue) p["@ClassId"] = criteria.ClassId.Value;
				//if (criteria.S.SectionId.HasValue) p["@SectionId"] = criteria.SectionId.Value;
				if (criteria.SchoolId.HasValue) p["@SchoolId"] = criteria.SchoolId.Value;
				
				var dt = new DataTable();
				p.Exec(dt);
				
				var list = new List<StudentMaster>();
				foreach (DataRow row in dt.Rows)
				{
					list.Add(MapStudent(row));
				}
				return list;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error searching students");
				throw;
			}
		}

		public bool Update(StudentMaster student)
		{
			try
			{
				var p = new Proc("Student_Update");
				p["@Id"] = student.Id;
				p["@FirstName"] = student.FirstName;
				p["@LastName"] = student.LastName;
				p["@DOB"] = student.DOB;
				p["@Email"] = student.Email;
				p["@Phone"] = student.Phone;
				p["@IsActive"] = student.IsActive;
				p["@SchoolId"] = student.SchoolId;
				p["@ModifiedBy"] = student.ModifiedBy;
				
				p.Exec();
				var ret = p.Parameters["@RETURN_VALUE"].Value;
				int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
				return code == 1;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error updating student with ID {student?.Id}");
				throw;
			}
		}

		public async Task<bool> UpdateAsync(StudentMaster student)
		{
			return await Task.Run(() => Update(student));
		}

		public async Task<bool> UpdateStudentAttendanceAsync(StudentAttendanceDetails attendance)
		{
			try
			{
				var p = new Proc("StudentAttendance_Update");
				p["@Id"] = attendance.Id;
				p["@Status"] = attendance.AttendenceStatus;
				p["@Reason"] = attendance.StatusMessage;
				p["@Remarks"] = attendance.StatusMessage;
				p["@IsActive"] = attendance.IsActive;
				p["@ModifiedBy"] = attendance.ModifiedBy;
				
				p.Exec();
				var ret = p.Parameters["@RETURN_VALUE"].Value;
				int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
				return code == 1;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error updating student attendance with ID {attendance?.Id}");
				throw;
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// No managed resources to dispose
				}
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~StudentService()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		void IDisposable.Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
