using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using System.Security.Claims;
using SchoolPortal.Entities.ViewModels;
using SchoolPortal.Entities;

namespace SchoolPortal.Services
{
	public class StudentService : IStudentService, IDisposable
	{
		private bool _disposed = false;
		private readonly ILookupService _lookupService;
		private readonly IMemoryCache _cache;
		private readonly ILogger<StudentService> _logger;
		private const string StudentCacheKey = "Students_All";
		private static readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);
		private static readonly SemaphoreSlim _cacheLock = new SemaphoreSlim(1, 1);

		public StudentService(
			ILookupService lookupService, 
			IMemoryCache cache, 
			ILogger<StudentService> logger)
		{
			_lookupService = lookupService ?? throw new ArgumentNullException(nameof(lookupService));
			_cache = cache ?? throw new ArgumentNullException(nameof(cache));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		private Guid GetCurrentUserId()
		{
			// TODO: Implement actual user ID retrieval
			// Example for ASP.NET Core:
			// var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
			// return Guid.TryParse(userId, out var id) ? id : Guid.Empty;
			return Guid.NewGuid(); // Temporary implementation
		}

		#region Core CRUD Operations

		public Task<List<StudentMaster>> GetAllAsync(Guid? schoolId = null)
		{
			return GetAllAsync(schoolId, CancellationToken.None);
		}

		private async Task<List<StudentMaster>> GetAllAsync(Guid? schoolId, CancellationToken cancellationToken)
		{
			try
			{
				var cacheKey = schoolId.HasValue ? $"{StudentCacheKey}_{schoolId}" : StudentCacheKey;
				
				// Use semaphore to prevent cache stampede
				await _cacheLock.WaitAsync(cancellationToken).ConfigureAwait(false);
				try
				{
					var result = await _cache.GetOrCreateAsync(cacheKey, async entry =>
					{
						entry.AbsoluteExpirationRelativeToNow = _cacheDuration;

						using (var p = new Proc("Student_GetAll"))
						{
							if (schoolId.HasValue)
								p["@SchoolId"] = schoolId.Value;

							var dt = new DataTable();
							await Task.Run(() => p.Exec(dt), cancellationToken).ConfigureAwait(false);

							var students = dt.Rows.Cast<DataRow>()
								.Select(row => Map(row, _logger))
								.Where(student => student != null)
								.Cast<StudentMaster>()
								.ToList();
							return students;
						}
					}) ?? new List<StudentMaster>();

					return result ?? new List<StudentMaster>();
				}
				finally
				{
					_cacheLock.Release();
				}
			}
			catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
			{
				_logger.LogInformation("Operation was canceled");
				throw;
			}
			catch (Exception ex) when (ex is not StudentServiceException)
			{
				_logger.LogError(ex, "Error retrieving students for school {SchoolId}", schoolId);
				throw new StudentServiceException("An error occurred while retrieving students", ex);
			}
		}

		public List<StudentMaster> GetAll(Guid? schoolId = null)
		{
			try
			{
				// For sync-over-async, use Task.Run to avoid deadlocks
				return Task.Run(() => GetAllAsync(schoolId)).GetAwaiter().GetResult();
			}
			catch (Exception ex) when (ex is not StudentServiceException)
			{
				_logger.LogError(ex, "Error in sync GetAll for school {SchoolId}", schoolId);
				throw new StudentServiceException("Error retrieving students", ex);
			}
		}

		public async Task<StudentMaster> GetByIdAsync(Guid id, ICacheEntry entry, CancellationToken cancellationToken = default)
		{
			if (id == Guid.Empty)
				throw new ArgumentException("Student ID cannot be empty", nameof(id));

			try
			{
				var cacheKey = $"Student_{id}";
				
				await _cacheLock.WaitAsync(cancellationToken).ConfigureAwait(false);
				try
				{
					return await _cache.GetOrCreateAsync(cacheKey, async entry =>
					{
                        entry.AbsoluteExpirationRelativeToNow = _cacheDuration;
						
						using (var p = new Proc("Student_GetById"))
						{
							p["@Id"] = id;
							var dt = new DataTable();
							await Task.Run(() => p.Exec(dt), cancellationToken).ConfigureAwait(false);
							
							if (dt.Rows.Count == 0)
								throw new KeyNotFoundException($"Student with ID {id} not found");
								
							return Map(dt.Rows[0], _logger);
						}
					}).ConfigureAwait(false);
				}
				finally
				{
					_cacheLock.Release();
				}
			}
			catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
			{
				_logger.LogInformation("Operation was canceled");
				throw;
			}
			catch (Exception ex) when (ex is not StudentServiceException)
			{
				_logger.LogError(ex, "Error retrieving student with ID {StudentGUID}", id);
				throw new StudentServiceException($"An error occurred while retrieving student with ID {id}", ex);
			}
		}

		public async Task<Guid> CreateAsync(StudentMaster student, CancellationToken cancellationToken = default)
		{
			if (student == null)
				throw new ArgumentNullException(nameof(student));

			try
			{
				// Validate required fields
				if (student.CompanyId == Guid.Empty)
					throw new ArgumentException("CompanyId is required", nameof(student.CompanyId));

				if (student.SchoolId == Guid.Empty)
					throw new ArgumentException("SchoolId is required", nameof(student.SchoolId));

				if (student.CreatedBy == Guid.Empty)
					throw new ArgumentException("CreatedBy is required", nameof(student.CreatedBy));

				// Validate category exists if provided
				if (student.CategoryId != Guid.Empty && !await CategoryExistsAsync(student.CategoryId, cancellationToken).ConfigureAwait(false))
					throw new ArgumentException("Invalid CategoryId. The specified category does not exist.", nameof(student.CategoryId));

				// Set default values
				student.CreatedDate = DateTime.UtcNow;
				student.IsActive = true;
				student.IsDeleted = false;

				using (var p = new Proc("Student_Create"))
				{
					MapStudentToParameters(p, student);

					var dt = new DataTable();
					await Task.Run(() => p.Exec(dt), cancellationToken).ConfigureAwait(false);

					if (dt.Rows.Count == 0 || dt.Rows[0]["Id"] == DBNull.Value)
						throw new StudentServiceException("Failed to create student. No ID returned from database.");

					var newId = new Guid(dt.Rows[0]["Id"]!.ToString()!);

					// Invalidate cache in a background task
					_ = Task.Run(() =>
					{
						_cache.Remove(StudentCacheKey);
						_cache.Remove($"{StudentCacheKey}_{student.SchoolId}");
					}, cancellationToken);

					_logger.LogInformation("Created student with ID: {StudentGUID}", newId);
					return newId;
				}
			}
			catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
			{
				_logger.LogInformation("Operation was canceled");
				throw;
			}
			catch (Exception ex) when (ex is not StudentServiceException)
			{
				_logger.LogError(ex, "Error creating student");
				throw new StudentServiceException("An error occurred while creating the student", ex);
			}
		}

		public async Task<bool> UpdateAsync(StudentMaster student, CancellationToken cancellationToken = default)
		{
			if (student == null)
				throw new ArgumentNullException(nameof(student));

			if (student.Id == Guid.Empty)
				throw new ArgumentException("Student ID cannot be empty", nameof(student.Id));

			try
			{
				// Validate category exists if provided
				if (student.CategoryId != Guid.Empty &&
					!await CategoryExistsAsync(student.CategoryId, cancellationToken).ConfigureAwait(false))
				{
					throw new ArgumentException(
						"Invalid CategoryId. The specified category does not exist.",
						nameof(student.CategoryId));
				}

				// Set modified date
				student.ModifiedDate = DateTime.UtcNow;

				if (student.ModifiedBy == Guid.Empty)
				{
					student.ModifiedBy = GetCurrentUserId();
				}

				using (var p = new Proc("Student_Update"))
				{
					// Map student to stored procedure parameters
					MapStudentToParameters(p, student);

					// Update specific parameters
					p["@Id"] = student.Id;
					p["@ModifiedBy"] = student.ModifiedBy ?? (object)DBNull.Value;
					p["@ModifiedDate"] = student.ModifiedDate;

					var dt = new DataTable();
					await Task.Run(() => p.Exec(dt), cancellationToken).ConfigureAwait(false);

					if (dt.Rows.Count == 0)
					{
						_logger.LogWarning("No rows affected when updating student with ID: {StudentGUID}", student.Id);
						return false;
					}

					// Invalidate relevant caches in background
					_ = Task.Run(() =>
					{
						_cache.Remove(StudentCacheKey);
						_cache.Remove($"{StudentCacheKey}_{student.SchoolId}");
						_cache.Remove($"Student_{student.Id}");
					}, cancellationToken);

					_logger.LogInformation("Updated student with ID: {StudentGUID}", student.Id);
					return true;
				}
			}
			catch (ArgumentException ex)
			{
				_logger.LogWarning(ex, $"Validation error updating student with ID: {student.Id}");
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error updating student with ID: {student.Id}");
				throw new StudentUpdateException("An error occurred while updating the student", ex);
			}
		}

		public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
		{
			if (id == Guid.Empty)
				throw new ArgumentException("Student ID cannot be empty", nameof(id));

			try
			{
				// Get student first to get school ID for cache invalidation
				var student = await _cache.GetOrCreateAsync($"Student_{id}", async entry =>
				{
					entry.AbsoluteExpirationRelativeToNow = _cacheDuration;
					using (var p = new Proc("Student_GetById"))
					{
						p["@Id"] = id;
						var dt = new DataTable();
						await Task.Run(() => p.Exec(dt), cancellationToken).ConfigureAwait(false);
						return dt.Rows.Count > 0 ? Map(dt.Rows[0], _logger) : null;
					}
				}).ConfigureAwait(false);

				if (student == null)
				{
					_logger.LogWarning("Delete operation failed: Student {StudentGUID} not found", id);
					return false;
				}

				using (var p = new Proc("Student_Delete"))
				{
					var currentUserId = GetCurrentUserId();
					if (currentUserId == Guid.Empty)
					{
						_logger.LogWarning("Delete operation failed: Current user ID is not available");
						return false;
					}

					p["@Id"] = id;
					p["@ModifiedBy"] = currentUserId;
					p["@ModifiedDate"] = DateTime.UtcNow;

					var dt = new DataTable();
					await Task.Run(() => p.Exec(dt), cancellationToken).ConfigureAwait(false);

					bool success = dt.Rows.Count > 0;

					if (success)
					{
						// Invalidate relevant caches in background
						_ = Task.Run(() =>
						{
							_cache.Remove(StudentCacheKey);
							_cache.Remove($"{StudentCacheKey}_{student.SchoolId}");
							_cache.Remove($"Student_{id}");
						}, cancellationToken);

						_logger.LogInformation("Soft-deleted student with ID: {StudentGUID}", id);
					}
					else
					{
						_logger.LogWarning("Student with ID {StudentGUID} not found or already deleted", id);
					}

					return success;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error deleting student with ID: {id}");
				throw new StudentDeleteException($"An error occurred while deleting student with ID: {id}", ex);
			}
		}

		#endregion

		#region Student Attendance

		public async Task<StudentAttendanceDetails?> GetStudentAttendanceByIdAsync(Guid id, CancellationToken cancellationToken = default)
		{
			if (id == Guid.Empty)
				throw new ArgumentException("Attendance ID cannot be empty", nameof(id));

			try
			{
				var cacheKey = $"StudentAttendance_{id}";

				await _cacheLock.WaitAsync(cancellationToken).ConfigureAwait(false);
				try
				{
					return await _cache.GetOrCreateAsync(cacheKey, async entry =>
					{
						entry.AbsoluteExpirationRelativeToNow = _cacheDuration;

						using (var p = new Proc("StudentAttendance_GetById"))
						{
							p["@Id"] = id;
							var dt = new DataTable();
							await Task.Run(() => p.Exec(dt), cancellationToken).ConfigureAwait(false);

							if (dt.Rows.Count == 0)
								return null;

							return MapToStudentAttendanceDetails(dt.Rows[0]);
						}
					}).ConfigureAwait(false);
				}
				finally
				{
					_cacheLock.Release();
				}
			}
			catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
			{
				_logger.LogInformation("GetStudentAttendanceByIdAsync was canceled for ID: {AttendanceId}", id);
				throw;
			}
			catch (Exception ex) when (ex is not StudentServiceException)
			{
				_logger.LogError(ex, "Error retrieving attendance with ID: {AttendanceId}", id);
				throw new StudentServiceException($"An error occurred while retrieving attendance with ID: {id}", ex);
			}
		}

		public async Task<Guid> CreateStudentAttendanceAsync(StudentAttendanceDetails attendance, CancellationToken cancellationToken = default)
		{
			if (attendance == null)
				throw new ArgumentNullException(nameof(attendance));

			if (attendance.StudentGUID == Guid.Empty)
				throw new ArgumentException("Student ID cannot be empty", nameof(attendance.StudentGUID));

			try
			{
				using (var p = new Proc("StudentAttendance_Create"))
				{
					p["@Id"] = Guid.NewGuid();
					p["@StudentGUID"] = attendance.StudentGUID;
					p["@AttendenceDate"] = attendance.AttendenceDate;
					p["@IsPresent"] = attendance.AttendenceStatus;
					p["@CreatedBy"] = GetCurrentUserId();
					p["@CreatedDate"] = DateTime.UtcNow;

					var dt = new DataTable();
					await Task.Run(() => p.Exec(dt), cancellationToken).ConfigureAwait(false);

					if (dt.Rows.Count == 0 || dt.Rows[0]["Id"] == DBNull.Value)
						throw new StudentServiceException("Failed to create student attendance. No ID returned from database.");

					var newId = new Guid(dt.Rows[0]["Id"]!.ToString()!);

					// Invalidate cache in a background task
					_ = Task.Run(() =>
					{
						_cache.Remove($"StudentAttendance_{attendance.StudentGUID}");
					}, cancellationToken);

					_logger.LogInformation($"Created attendance record {newId} for student {attendance.StudentGUID}");
					return newId;
				}
			}
			catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
			{
				_logger.LogInformation("Operation was canceled");
				throw;
			}
			catch (Exception ex) when (ex is not StudentServiceException)
			{
				_logger.LogError(ex, "Error creating attendance for student with ID {StudentGUID}", attendance.StudentGUID);
				throw new StudentServiceException($"An error occurred while creating attendance for student with ID {attendance.StudentGUID}", ex);
			}
		}

		public async Task<bool> UpdateStudentAttendanceAsync(StudentAttendanceDetails attendance)
		{
			if (_disposed)
				throw new ObjectDisposedException(nameof(StudentService));

			if (attendance == null)
				throw new ArgumentNullException(nameof(attendance));

			try
			{
				using (var p = new Proc("StudentAttendance_Update"))
				{
					p["@Id"] = attendance.Id;
					p["@StudentGUID"] = attendance.StudentGUID;
					p["@ClassId"] = attendance.ClassId;
					p["@SectionId"] = attendance.SectionId;
					p["@Month"] = attendance.Month ?? (object)DBNull.Value;
					p["@Year"] = attendance.Year ?? (object)DBNull.Value;
					p["@AttendenceDate"] = attendance.AttendenceDate;
					p["@AttendenceStatus"] = attendance.AttendenceStatus;
					p["@AttendanceReasonId"] = attendance.AttendanceReasonId;
					p["@AttendenceTime"] = attendance.AttendenceTime;
					p["@ModifiedBy"] = GetCurrentUserId();
					p["@Status"] = attendance.Status;
					p["@StatusMessage"] = attendance.StatusMessage;

					var result = (int)await Task.Run(() => p.ExecScalar()).ConfigureAwait(false);
					return result > 0;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error updating student attendance with ID {AttendanceId}", attendance.Id);
				throw new StudentServiceException($"Error updating student attendance with ID {attendance.Id}", ex);
			}
		}

		#endregion

		#region Search and Statistics

		public async Task<IEnumerable<StudentMaster>> SearchStudentsAsync(StudentSearchCriteria criteria, CancellationToken cancellationToken = default)
		{
			if (_disposed)
				throw new ObjectDisposedException(nameof(StudentService));

			if (criteria == null)
				throw new ArgumentNullException(nameof(criteria));

			try
			{
				var cacheKey = $"StudentSearch_{criteria.SearchTerm}_{criteria.SchoolId}_{criteria.ClassId}_{criteria.IsActive}_{criteria.PageNumber}_{criteria.PageSize}";
				
				return await _cache.GetOrCreateAsync(cacheKey, async entry =>
				{
					entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5); // Shorter cache duration for search results
					
					using (var p = new Proc("Student_Search"))
					{
						p["@SearchTerm"] = string.IsNullOrEmpty(criteria.SearchTerm) ? (object)DBNull.Value : criteria.SearchTerm;
						p["@SchoolId"] = criteria.SchoolId ?? (object)DBNull.Value;
						p["@ClassId"] = criteria.ClassId ?? (object)DBNull.Value;
						p["@IsActive"] = criteria.IsActive ?? (object)DBNull.Value;
						p["@PageNumber"] = criteria.PageNumber;
						p["@PageSize"] = criteria.PageSize;

						var dt = new DataTable();
						await Task.Run(() => p.Exec(dt), cancellationToken).ConfigureAwait(false);

						// Ensure the result is IEnumerable<StudentMaster> (no nulls)
						return dt.Rows.Cast<DataRow>()
							.Select(row => Map(row, _logger))
							.Where(student => student != null)
							.Cast<StudentMaster>();
					}
				}).ConfigureAwait(false) ?? Enumerable.Empty<StudentMaster>();
			}
			catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
			{
				_logger.LogInformation("Operation was canceled");
				throw;
			}
			catch (Exception ex) when (ex is not StudentServiceException)
			{
				_logger.LogError(ex, "Error searching students with criteria: {SearchTerm}, SchoolId: {SchoolId}, ClassId: {ClassId}", 
					criteria.SearchTerm, criteria.SchoolId, criteria.ClassId);
				throw new StudentServiceException("An error occurred while searching students", ex);
			}
		}

		public async Task<StudentStats> GetStudentStatisticsAsync(Guid? schoolId = null, CancellationToken cancellationToken = default)
		{
			if (_disposed)
				throw new ObjectDisposedException(nameof(StudentService));

			try
			{
				var cacheKey = $"StudentStats_{schoolId ?? Guid.Empty}";
				
				return await _cache.GetOrCreateAsync(cacheKey, async entry =>
				{
					entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15);
					
					using (var p = new Proc("Student_GetStatistics"))
					{
						if (schoolId.HasValue)
						 p["@SchoolId"] = schoolId.Value;

						var stats = new StudentStats();
						
						using (var reader = await Task.Run(() => p.ExecReader(), cancellationToken).ConfigureAwait(false))
						{
							if (reader.Read())
							{
								stats.TotalStudents = reader.GetInt32(reader.GetOrdinal("TotalStudents"));
								stats.ActiveStudents = reader.GetInt32(reader.GetOrdinal("ActiveStudents"));
								stats.NewThisMonth = reader.GetInt32(reader.GetOrdinal("NewThisMonth"));
								stats.InactiveStudents = reader.GetInt32(reader.GetOrdinal("InactiveStudents"));
								stats.GraduatedThisYear = reader.GetInt32(reader.GetOrdinal("GraduatedThisYear"));
							}
						}
						
						return stats;
					}
				}).ConfigureAwait(false) ?? new StudentStats();
			}
			catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
			{
				_logger.LogInformation("Operation was canceled");
				throw;
			}
			catch (Exception ex) when (ex is not StudentServiceException)
			{
				_logger.LogError(ex, "Error retrieving student statistics for school {SchoolId}", schoolId);
				throw new StudentServiceException("An error occurred while retrieving student statistics", ex);
			}
		}

		public async Task<bool> BulkUpdateStatusAsync(IEnumerable<Guid> StudentGUIDs, bool isActive)
		{
			if (_disposed)
				throw new ObjectDisposedException(nameof(StudentService));

			if (StudentGUIDs == null || !StudentGUIDs.Any())
				throw new ArgumentException("Student IDs cannot be empty", nameof(StudentGUIDs));

			try
			{
				using (var p = new Proc("Student_BulkUpdateStatus"))
				{
					var dt = new DataTable();
					dt.Columns.Add("Id", typeof(Guid));
					foreach (var id in StudentGUIDs.Distinct())
					{
						dt.Rows.Add(id);
					}

					p["@StudentGUIDs"] = dt;
					p["@IsActive"] = isActive;
					p["@ModifiedBy"] = GetCurrentUserId();

					var result = (int)await Task.Run(() => p.ExecScalar()).ConfigureAwait(false);
					return result > 0;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error updating status for {Count} students", StudentGUIDs.Count());
				throw new StudentServiceException("Error updating student statuses", ex);
			}
		}

		#endregion

		#region Helper Methods

		private static StudentMaster? Map(DataRow row, ILogger<StudentService> logger)
		{
			try
			{
				if (row == null) return null;

				var student = new StudentMaster
				{
					Id = row.Field<Guid>("Id"),
					RollNumber = row.Field<Guid>("RollNumber"),
					FirstName = row.Field<string>("FirstName"),
					LastName = row.Field<string>("LastName"),
					Address = row.Table.Columns.Contains("Address") ? row.Field<string>("Address") : null,
					CityId = row.Table.Columns.Contains("CityId") && !row.IsNull("CityId") ? row.Field<Guid>("CityId") : Guid.Empty,
					StateId = row.Table.Columns.Contains("StateId") && !row.IsNull("StateId") ? row.Field<Guid>("StateId") : Guid.Empty,
					CountryId = row.Field<Guid>("CountryId"),
					ZipCode = row.Field<string>("ZipCode"),
					ContactNumber = row.Field<string>("ContactNumber"),
					EmergencyContactNumber = row.Field<string>("EmergencyContactNumber"),
					DOB = row.Field<DateTime>("DOB"),
					DOJ = row.Field<DateTime>("DOJ"),
					RegistrationNumber = row.Field<string>("RegistrationNumber"),
					ClassId = row.Field<Guid>("ClassId"),
					SectionId = row.Field<Guid>("SectionId"),
					AvailTransport = row.Field<bool?>("AvailTransport"),
					Image = row.Field<string>("Image"),
					Email = row.Field<string>("Email"),
					CategoryId = row.Field<Guid>("CategoryId"),
					SiblingsIfAny = row.Field<bool?>("SiblingsIfAny"),
					SiblingClassId = row.Field<Guid?>("SiblingClassId"),
					Gender = row.Field<Guid?>("Gender"),
					DisabilityAny = row.Field<string>("DisabilityAny"),
					MedicalAlleryAny = row.Field<string>("MedicalAlleryAny"),
					BirthCityId = row.Field<Guid>("BirthCityId"),
					BirthStateId = row.Field<Guid>("BirthStateId"),
					BirthCountryId = row.Field<Guid>("BirthCountryId"),
					PreviousSchoolAttended = row.Field<string>("PreviousSchoolAttended"),
					PreviousSchoolClassId = row.Field<Guid?>("PreviousSchoolClassId"),
					PreviousSchoolPercentage = row.Field<decimal?>("PreviousSchoolPercentage"),
					PreviousSchoolRank = row.Field<string>("PreviousSchoolRank"),
					PreviousSchoolBoardId = row.Field<Guid>("PreviousSchoolBoardId"),
					PreviousSchoolFromDate = row.Field<DateTime?>("PreviousSchoolFromDate"),
					PreviousSchoolToDate = row.Field<DateTime?>("PreviousSchoolToDate"),
					WithdrawnDate = row.Field<DateTime?>("WithdrawnDate"),
					WithdrawnReason = row.Field<string>("WithdrawnReason"),
					BloodGroupId = row.Field<Guid>("BloodGroupId"),
					Nationality = row.Field<Guid>("Nationality"),
					Hobbies = row.Field<string>("Hobbies"),
					ReligionId = row.Field<Guid>("ReligionId"),
					Phone = row.Field<string>("Phone"),
					RouteId = row.Field<Guid?>("RouteId"),
					RouteStopDetailsId = row.Field<Guid?>("RouteStopDetailsId"),
					ClassTeacherId = row.Field<Guid?>("ClassTeacherId"),
					RoutePickAndDrop = row.Field<bool?>("RoutePickAndDrop"),
					FeesDiscountCategoryMasterId = row.Field<Guid?>("FeesDiscountCategoryMasterId"),
					TutionFees = row.Field<decimal?>("TutionFees"),
					AnnualFees = row.Field<decimal?>("AnnualFees"),
					TransportFees = row.Field<decimal?>("TransportFees"),
					UseTransportFees = row.Field<bool?>("UseTransportFees"),
					SessionId = row.Field<Guid?>("SessionId"),
					CompanyId = row.Field<Guid>("CompanyId"),
					SchoolId = row.Field<Guid>("SchoolId"),
					IsActive = row.Field<bool>("IsActive"),
					IsDeleted = row.Field<bool>("IsDeleted"),
					CreatedBy = row.Field<Guid>("CreatedBy"),
					CreatedDate = row.Field<DateTime>("CreatedDate"),
					ModifiedBy = row.Field<Guid?>("ModifiedBy"),
					ModifiedDate = row.Field<DateTime?>("ModifiedDate"),
					Status = row.Field<string>("Status") ?? "INC",
					StatusMessage = row.Field<string>("StatusMessage") ?? "In Process....",
					HouseAllotted = row.Field<Guid?>("HouseAllotted"),
					AdditionalNotes = row.Field<string>("AdditionalNotes")
				};

				return student;
			}
			catch (Exception ex)
			{
				logger?.LogError(ex, "Error mapping student data");
				throw new StudentServiceException("Error mapping student data", ex);
			}
		}

		private void MapStudentToParameters(Proc p, StudentMaster student)
		{
			p["@Id"] = student.Id != Guid.Empty ? student.Id : (object)DBNull.Value;
			p["@RollNumber"] = student.RollNumber;
			p["@FirstName"] = student.FirstName;
			p["@LastName"] = student.LastName;
			p["@Address"] = student.Address ?? (object)DBNull.Value;
			p["@CityId"] = student.CityId;
			p["@StateId"] = student.StateId;
			p["@CountryId"] = student.CountryId;
			p["@ZipCode"] = student.ZipCode ?? (object)DBNull.Value;
			p["@ContactNumber"] = student.ContactNumber ?? (object)DBNull.Value;
			p["@EmergencyContactNumber"] = student.EmergencyContactNumber ?? (object)DBNull.Value;
			p["@DOB"] = student.DOB;
			p["@DOJ"] = student.DOJ;
			p["@RegistrationNumber"] = student.RegistrationNumber ?? (object)DBNull.Value;
			p["@ClassId"] = student.ClassId;
			p["@SectionId"] = student.SectionId;
			p["@AvailTransport"] = student.AvailTransport ?? (object)DBNull.Value;
			p["@Image"] = student.Image ?? (object)DBNull.Value;
			p["@Email"] = student.Email ?? (object)DBNull.Value;
			p["@CategoryId"] = student.CategoryId;
			p["@SiblingsIfAny"] = student.SiblingsIfAny ?? (object)DBNull.Value;
			p["@SiblingClassId"] = student.SiblingClassId ?? (object)DBNull.Value;
			p["@Gender"] = student.Gender ?? (object)DBNull.Value;
			p["@DisabilityAny"] = student.DisabilityAny ?? (object)DBNull.Value;
			p["@MedicalAlleryAny"] = student.MedicalAlleryAny ?? (object)DBNull.Value;
			p["@BirthCityId"] = student.BirthCityId;
			p["@BirthStateId"] = student.BirthStateId;
			p["@BirthCountryId"] = student.BirthCountryId;
			p["@PreviousSchoolAttended"] = student.PreviousSchoolAttended ?? (object)DBNull.Value;
			p["@PreviousSchoolClassId"] = student.PreviousSchoolClassId ?? (object)DBNull.Value;
			p["@PreviousSchoolPercentage"] = student.PreviousSchoolPercentage ?? (object)DBNull.Value;
			p["@PreviousSchoolRank"] = student.PreviousSchoolRank ?? (object)DBNull.Value;
			p["@PreviousSchoolBoardId"] = student.PreviousSchoolBoardId;
			p["@PreviousSchoolFromDate"] = student.PreviousSchoolFromDate ?? (object)DBNull.Value;
			p["@PreviousSchoolToDate"] = student.PreviousSchoolToDate ?? (object)DBNull.Value;
			p["@WithdrawnDate"] = student.WithdrawnDate ?? (object)DBNull.Value;
			p["@WithdrawnReason"] = student.WithdrawnReason ?? (object)DBNull.Value;
			p["@BloodGroupId"] = student.BloodGroupId;
			p["@Nationality"] = student.Nationality;
			p["@Hobbies"] = student.Hobbies ?? (object)DBNull.Value;
			p["@ReligionId"] = student.ReligionId;
			p["@Phone"] = student.Phone ?? (object)DBNull.Value;
			p["@RouteId"] = student.RouteId ?? (object)DBNull.Value;
			p["@RouteStopDetailsId"] = student.RouteStopDetailsId ?? (object)DBNull.Value;
			p["@ClassTeacherId"] = student.ClassTeacherId ?? (object)DBNull.Value;
			p["@RoutePickAndDrop"] = student.RoutePickAndDrop ?? (object)DBNull.Value;
			p["@FeesDiscountCategoryMasterId"] = student.FeesDiscountCategoryMasterId ?? (object)DBNull.Value;
			p["@TutionFees"] = student.TutionFees ?? (object)DBNull.Value;
			p["@AnnualFees"] = student.AnnualFees ?? (object)DBNull.Value;
			p["@TransportFees"] = student.TransportFees ?? (object)DBNull.Value;
			p["@UseTransportFees"] = student.UseTransportFees ?? (object)DBNull.Value;
			p["@SessionId"] = student.SessionId ?? (object)DBNull.Value;
			p["@CompanyId"] = student.CompanyId;
			p["@SchoolId"] = student.SchoolId;
			p["@IsActive"] = student.IsActive;
			p["@IsDeleted"] = student.IsDeleted;
			p["@CreatedBy"] = student.CreatedBy;
			p["@CreatedDate"] = student.CreatedDate;
			p["@ModifiedBy"] = student.ModifiedBy ?? (object)DBNull.Value;
			p["@ModifiedDate"] = student.ModifiedDate ?? (object)DBNull.Value;
			p["@Status"] = student.Status ?? "INC";
			p["@StatusMessage"] = student.StatusMessage ?? "In Process....";
			p["@HouseAllotted"] = student.HouseAllotted ?? (object)DBNull.Value;
			p["@AdditionalNotes"] = student.AdditionalNotes ?? (object)DBNull.Value;
		}

		public async Task<bool> CategoryExistsAsync(Guid categoryId, CancellationToken cancellationToken = default)
		{
			if (categoryId == Guid.Empty)
				return false;

			try
			{
				using (var p = new Proc("Category_Exists"))
				{
					p["@Id"] = categoryId;
					var dt = new DataTable();
					await Task.Run(() => p.Exec(dt), cancellationToken).ConfigureAwait(false);
					return dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value && Convert.ToInt32(dt.Rows[0][0]) > 0;
				}
			}
			catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
			{
				_logger.LogInformation("Operation was canceled");
				throw;
			}
			catch (Exception ex) when (ex is not StudentServiceException)
			{
				_logger.LogError(ex, "Error checking if category with ID {CategoryId} exists", categoryId);
				throw new StudentServiceException($"An error occurred while checking if category with ID {categoryId} exists", ex);
			}
		}

		#endregion

		#region IDisposable Implementation
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_cacheLock?.Dispose();
				}
				_disposed = true;
			}
		}
		#endregion

		#region IStudentService Implementation

		public async Task<StudentMaster?> GetByIdAsync(Guid id)
		{
			using (var p = new Proc("Student_GetById"))
			{
				p["@Id"] = id;
				var dt = new DataTable();
				await Task.Run(() => p.Exec(dt));
				return dt.Rows.Count > 0 ? Map(dt.Rows[0], _logger) : null;
			}
		}

		public async Task<Guid> CreateAsync(StudentMaster student)
		{
			using (var p = new Proc("Student_Create"))
			{
				p["@Id"] = student.Id;
				p["@FirstName"] = student.FirstName;
				p["@LastName"] = student.LastName;
				// Add other properties as needed...
				
				var result = await Task.Run(() => p.ExecScalar());
				return (Guid)result;
			}
		}

		public async Task<bool> UpdateAsync(StudentMaster student)
		{
			using (var p = new Proc("Student_Update"))
			{
				p["@Id"] = student.Id;
				p["@FirstName"] = student.FirstName;
				p["@LastName"] = student.LastName;
				// Add other properties as needed...
				
				var result = await Task.Run(() => p.ExecNonQuery());
				return result > 0;
			}
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			using (var p = new Proc("Student_Delete"))
			{
				p["@Id"] = id;
				var result = await Task.Run(() => p.ExecNonQuery());
				return result > 0;
			}
		}

		public async Task<bool> CategoryExistsAsync(Guid categoryId)
		{
			using (var p = new Proc("Category_Exists"))
			{
				p["@Id"] = categoryId;
				var result = await Task.Run(() => p.ExecScalar());
				return Convert.ToBoolean(result);
			}
		}

		public async Task<StudentAttendanceDetails?> GetStudentAttendanceByIdAsync(Guid id)
		{
			if (_disposed)
				throw new ObjectDisposedException(nameof(StudentService));
			if (id == Guid.Empty)
				throw new ArgumentException("ID cannot be empty", nameof(id));
			try
			{
				using (var p = new Proc("StudentAttendance_GetById"))
				{
					p["@Id"] = id;
					
					var dt = new DataTable();
					await Task.Run(() => p.Exec(dt)).ConfigureAwait(false);
					
					if (dt.Rows.Count == 0)
						return null;
					return MapToStudentAttendanceDetails(dt.Rows[0]);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving student attendance with ID {AttendanceId}", id);
				throw new StudentServiceException($"Error retrieving student attendance with ID {id}", ex);
			}
		}

		public async Task<Guid> CreateStudentAttendanceAsync(StudentAttendanceDetails attendance)
		{
			using (var p = new Proc("StudentAttendance_Create"))
			{
				p["@Id"] = attendance.Id;
				p["@StudentGUID"] = attendance.StudentGUID;
				p["@ClassId"] = attendance.ClassId;
				p["@SectionId"] = attendance.SectionId;
				p["@AttendenceDate"] = attendance.AttendenceDate;
				p["@AttendenceStatus"] = attendance.AttendenceStatus;
				p["@AttendanceReasonId"] = attendance.AttendanceReasonId;
				
				// Optional parameters
				p["@Month"] = attendance.Month ?? (object)DBNull.Value;
				p["@Year"] = attendance.Year ?? (object)DBNull.Value;
				p["@AttendenceTime"] = attendance.AttendenceTime;
				p["@CompanyId"] = attendance.CompanyId;
				p["@SchoolId"] = attendance.SchoolId;
				p["@IsActive"] = attendance.IsActive;
				p["@IsDeleted"] = attendance.IsDeleted;
				p["@CreatedBy"] = attendance.CreatedBy;
				p["@CreatedDate"] = attendance.CreatedDate;
				p["@Status"] = attendance.Status;
				p["@StatusMessage"] = attendance.StatusMessage;
				
				// Execute the stored procedure
				var result = await Task.Run(() => p.ExecScalar());
				return (Guid)result;
			}
		}

		public StudentMaster GetById(Guid id)
		{
			return GetByIdAsync(id).GetAwaiter().GetResult();
		}

		public Guid Create(StudentMaster student)
		{
			return CreateAsync(student).GetAwaiter().GetResult();
		}

		public bool Update(StudentMaster student)
		{
			return UpdateAsync(student).GetAwaiter().GetResult();
		}

		public bool Delete(Guid id)
		{
			return DeleteAsync(id).GetAwaiter().GetResult();
		}

		public async Task<IEnumerable<StudentMaster>> SearchStudentsAsync(StudentSearchCriteria criteria)
		{
			using (var p = new Proc("Student_Search"))
			{
				p["@SearchTerm"] = criteria.SearchTerm ?? (object)DBNull.Value;
				p["@SchoolId"] = criteria.SchoolId ?? (object)DBNull.Value;
				p["@ClassId"] = criteria.ClassId ?? (object)DBNull.Value;
				p["@IsActive"] = criteria.IsActive ?? (object)DBNull.Value;
				p["@PageNumber"] = criteria.PageNumber;
				p["@PageSize"] = criteria.PageSize;
				
				var dt = new DataTable();
				await Task.Run(() => p.Exec(dt));
				
				return dt.Rows.Cast<DataRow>()
					.Select(row => Map(row, _logger))
					.Where(student => student != null)
					.Select(student => student!);
			}
		}

		public async Task<StudentStats> GetStudentStatisticsAsync(Guid? schoolId = null)
		{
			using (var p = new Proc("Student_GetStatistics"))
			{
				p["@SchoolId"] = schoolId ?? (object)DBNull.Value;
				
				var dt = new DataTable();
				await Task.Run(() => p.Exec(dt));
				
				if (dt.Rows.Count == 0)
					return new StudentStats();
					
				var row = dt.Rows[0];
				return new StudentStats
				{
					TotalStudents = Convert.ToInt32(row["TotalStudents"]),
					ActiveStudents = Convert.ToInt32(row["ActiveStudents"]),
					NewThisMonth = Convert.ToInt32(row["NewThisMonth"]),
					InactiveStudents = Convert.ToInt32(row["InactiveStudents"] ?? 0),
					GraduatedThisYear = Convert.ToInt32(row["GraduatedThisYear"] ?? 0)
				};
			}
		}

		private StudentAttendanceDetails MapToStudentAttendanceDetails(DataRow row)
		{
			return new StudentAttendanceDetails
			{
				Id = row.Field<Guid>("Id"),
				StudentGUID = row.Field<Guid>("StudentGUID"),
				ClassId = row.Field<Guid>("ClassId"),
				SectionId = row.Field<Guid>("SectionId"),
				Month = row.Field<int?>("Month"),
				Year = row.Field<int?>("Year"),
				AttendenceDate = row.Field<DateTime>("AttendenceDate"),
				AttendenceStatus = row.Field<bool>("IsPresent"),
				AttendanceReasonId = row.Field<Guid>("AttendanceReasonId"),
				AttendenceTime = row.Field<string>("AttendanceTime"),
				CompanyId = row.Field<Guid>("CompanyId"),
				SchoolId = row.Field<Guid>("SchoolId"),
				IsActive = row.Field<bool>("IsActive"),
				IsDeleted = row.Field<bool>("IsDeleted"),
				CreatedBy = row.Field<Guid>("CreatedBy"),
				CreatedDate = row.Field<DateTime>("CreatedDate"),
				ModifiedBy = row.Field<Guid?>("ModifiedBy"),
				ModifiedDate = row.Field<DateTime?>("ModifiedDate"),
				Status = row.Field<string>("Status") ?? "INC",
				StatusMessage = row.Field<string>("StatusMessage") ?? "In Process...."
			};
		}

		#endregion
	}

	public class StudentServiceException : Exception
	{
		public StudentServiceException() { }
		public StudentServiceException(string message) : base(message) { }
		public StudentServiceException(string message, Exception inner) : base(message, inner) { }
	}

	public class StudentNotFoundException : StudentServiceException
	{
		public StudentNotFoundException(Guid StudentGUID) 
			: base($"Student with ID {StudentGUID} was not found.") { }
	}

	public class StudentUpdateException : StudentServiceException
	{
		public StudentUpdateException(string message, Exception? inner = null) 
			: base(message, inner ?? new Exception(message)) { }
	}

	public class StudentDeleteException : StudentServiceException
	{
		public StudentDeleteException(string message, Exception? inner = null) 
			: base(message, inner ?? new Exception(message)) { }
	}
}
