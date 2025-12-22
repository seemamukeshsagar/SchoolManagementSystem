using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using SchoolPortal.Services.ServiceViewModels;
using SchoolPortal.Services;
using Microsoft.Extensions.Logging;
using SchoolPortal.DBAccess;
using System.Data;

namespace SchoolPortal.Services
{
	public class StudentAttendanceService : IStudentAttendanceService
	{
		private readonly ILogger<StudentAttendanceService> _logger;

		public StudentAttendanceService(ILogger<StudentAttendanceService> logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public List<StudentAttendanceDetails> GetAll()
		{
			try
			{
				// Implement the actual data retrieval logic here
				// This is just a placeholder
				return new List<StudentAttendanceDetails>();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while getting all student attendance records");
				throw;
			}
		}

		public StudentAttendanceDetails GetById(Guid id)
		{
			try
			{
				// Implement the actual data retrieval logic here
				// This is just a placeholder
				return new StudentAttendanceDetails { Id = id };
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error occurred while getting student attendance by ID: {id}");
				throw;
			}
		}

		public async Task<StudentAttendanceDetails> GetByIdAsync(Guid id)
		{
			try
			{
				return await Task.Run(() =>
				{
					using Proc p = new Proc("StudentAttendance_GetById");
					p["@Id"] = id;
					var dt = new DataTable();
					p.Exec(dt);
					if (dt.Rows.Count == 0)
						return new StudentAttendanceDetails(); // Return a non-null instance
					// Ensure MapToStudentAttendanceDetails never returns null
					var details = MapToStudentAttendanceDetails(dt.Rows[0]);
					return details ?? new StudentAttendanceDetails();
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error occurred while getting student attendance by ID: {id}");
				throw;
			}
		}

		public Guid Create(StudentAttendanceDetails attendance)
		{
			try
			{
				// Implement the actual creation logic here
				// This is just a placeholder
				attendance.Id = Guid.NewGuid();
				attendance.CreatedDate = DateTime.UtcNow;
				return attendance.Id;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while creating student attendance record");
				throw;
			}
		}

		public async Task<Guid> CreateAsync(StudentAttendanceDetails attendance)
		{
			return await Task.Run(() => Create(attendance));
		}

		public bool Update(StudentAttendanceDetails attendance)
		{
			try
			{
				// Implement the actual update logic here
				// This is just a placeholder
				attendance.ModifiedDate = DateTime.UtcNow;
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error occurred while updating student attendance record: {attendance.Id}");
				throw;
			}
		}

		public async Task<bool> UpdateAsync(StudentAttendanceDetails attendance)
		{
			return await Task.Run(() => Update(attendance));
		}

		public bool Delete(Guid id)
		{
			try
			{
				// Implement the actual deletion logic here
				// This is just a placeholder
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error occurred while deleting student attendance record: {id}");
				throw;
			}
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			return await Task.Run(() => Delete(id));
		}

		private StudentAttendanceDetails MapToStudentAttendanceDetails(DataRow row)
		{
			// row is never null here, so no need for null check
			return new StudentAttendanceDetails
			{
				Id = row["Id"] != DBNull.Value ? (Guid)row["Id"] : Guid.Empty,
				StudentGUID = row["StudentId"] != DBNull.Value ? (Guid)row["StudentId"] : Guid.Empty,
				AttendenceDate = row["AttendanceDate"] != DBNull.Value ? Convert.ToDateTime(row["AttendanceDate"]) : DateTime.MinValue,
				// Add other properties as needed
			};
		}
	}
}