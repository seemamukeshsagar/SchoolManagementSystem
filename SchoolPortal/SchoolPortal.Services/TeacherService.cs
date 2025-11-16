using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
	public class TeacherService : ITeacherService
	{
		private readonly ILogger<TeacherService> _logger;
		
		public TeacherService(ILogger<TeacherService> logger)
		{
			_logger = logger;
		}
		
		private static TeacherMaster Map(DataRow r)
		{
			var t = new TeacherMaster();
			if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) t.Id = id;
			t.FirstName = r.Table.Columns.Contains("FirstName") ? r["FirstName"].ToString() ?? string.Empty : string.Empty;
			t.LastName = r.Table.Columns.Contains("LastName") ? r["LastName"].ToString() ?? string.Empty : string.Empty;
			if (r.Table.Columns.Contains("DOB") && DateTime.TryParse(r["DOB"].ToString(), out var dob)) t.DOB = dob;
			if (r.Table.Columns.Contains("DOJ") && DateTime.TryParse(r["DOJ"].ToString(), out var doj)) t.DOJ = doj;
			if (r.Table.Columns.Contains("DateOfLeaving") && DateTime.TryParse(r["DateOfLeaving"].ToString(), out var dol)) t.DateOfLeaving = dol;
			t.Address = r.Table.Columns.Contains("Address") ? r["Address"].ToString() ?? string.Empty : string.Empty;
			if (r.Table.Columns.Contains("CityId") && Guid.TryParse(r["CityId"].ToString(), out var cityId)) t.CityId = cityId;
			if (r.Table.Columns.Contains("StateId") && Guid.TryParse(r["StateId"].ToString(), out var stateId)) t.StateId = stateId;
			if (r.Table.Columns.Contains("CountryId") && Guid.TryParse(r["CountryId"].ToString(), out var countryId)) t.CountryId = countryId;
			t.ZipCode = r.Table.Columns.Contains("ZipCode") ? r["ZipCode"].ToString() ?? string.Empty : string.Empty;
			if (r.Table.Columns.Contains("Gender") && Guid.TryParse(r["Gender"].ToString(), out var gender)) t.Gender = gender;
			if (r.Table.Columns.Contains("MaritalStatusId") && Guid.TryParse(r["MaritalStatusId"].ToString(), out var marital)) t.MaritalStatusId = marital;
			t.Image = r.Table.Columns.Contains("Image") ? r["Image"].ToString() ?? string.Empty : string.Empty;
			t.Email = r.Table.Columns.Contains("Email") ? r["Email"].ToString() ?? string.Empty : string.Empty;
			t.Phone = r.Table.Columns.Contains("Phone") ? r["Phone"].ToString() ?? string.Empty : string.Empty;
			t.MobilePhone = r.Table.Columns.Contains("MobilePhone") ? r["MobilePhone"].ToString() ?? string.Empty : string.Empty;
			t.YearsOfExperience = r.Table.Columns.Contains("YearsOfExperience") ? r["YearsOfExperience"].ToString() ?? string.Empty : string.Empty;
			t.PreviousSchool = r.Table.Columns.Contains("PreviousSchool") ? r["PreviousSchool"].ToString() ?? string.Empty : string.Empty;
			t.Salutation = r.Table.Columns.Contains("Salutation") ? r["Salutation"].ToString() ?? string.Empty : string.Empty;
			if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) t.IsActive = active;
			if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) t.IsDeleted = deleted;
			if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) t.CompanyId = companyId;
			if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) t.SchoolId = schoolId;
			if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) t.CreatedBy = createdBy;
			if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) t.CreatedDate = createdDate;
			if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) t.ModifiedBy = modifiedBy;
			if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) t.ModifiedDate = modifiedDate;
			t.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
			t.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
			return t;
		}

		public List<TeacherMaster> GetAll()
		{
			var list = new List<TeacherMaster>();
			Proc p = new Proc("Teacher_GetAll");
			var dt = new DataTable();
			p.Exec(dt);
			foreach (DataRow r in dt.Rows)
			{
				list.Add(Map(r));
			}
			return list;
		}

		public List<TeacherMaster> GetAll(Guid schoolId)
		{
			var list = new List<TeacherMaster>();
			Proc p = new Proc("Teacher_GetAll_SchoolId");
			p["@SchoolId"] = schoolId;
			var dt = new DataTable();
			p.Exec(dt);
			foreach (DataRow r in dt.Rows)
			{
				list.Add(Map(r));
			}
			return list;
		}

		public TeacherMaster? GetById(Guid id)
		{
			Proc p = new Proc("Teacher_GetById");
			p["@Id"] = id;
			var dt = new DataTable();
			p.Exec(dt);
			if (dt.Rows.Count == 0) return null;
			return Map(dt.Rows[0]);
		}

		public Guid Create(TeacherMaster t)
		{
			try
			{
				_logger.LogInformation("Creating teacher: {FirstName} {LastName}", t.FirstName, t.LastName);
				_logger.LogInformation("Teacher details - DOB: {DOB}, Email: {Email}, SchoolId: {SchoolId}, CompanyId: {CompanyId}", t.DOB, t.Email, t.SchoolId, t.CompanyId);
				
				Proc p = new Proc("Teacher_Create");
				p["@FirstName"] = t.FirstName;
				p["@LastName"] = t.LastName ?? string.Empty;
				p["@DOB"] = t.DOB;
				p["@DOJ"] = (object?)t.DOJ ?? DBNull.Value;
				p["@DateOfLeaving"] = (object?)t.DateOfLeaving ?? DBNull.Value;
				p["@Address"] = t.Address ?? string.Empty;
				p["@CityId"] = (object?)t.CityId ?? DBNull.Value;
				p["@StateId"] = (object?)t.StateId ?? DBNull.Value;
				p["@CountryId"] = (object?)t.CountryId ?? DBNull.Value;
				p["@ZipCode"] = t.ZipCode ?? string.Empty;
				p["@Gender"] = (object?)t.Gender ?? DBNull.Value;
				p["@MaritalStatusId"] = (object?)t.MaritalStatusId ?? DBNull.Value;
				p["@Image"] = t.Image ?? string.Empty;
				p["@Email"] = t.Email ?? string.Empty;
				p["@Phone"] = t.Phone ?? string.Empty;
				p["@MobilePhone"] = t.MobilePhone ?? string.Empty;
				p["@YearsOfExperience"] = t.YearsOfExperience ?? string.Empty;
				p["@PreviousSchool"] = t.PreviousSchool ?? string.Empty;
				p["@Salutation"] = t.Salutation ?? string.Empty;
				p["@IsActive"] = t.IsActive;
				p["@IsDeleted"] = t.IsDeleted;
				p["@CompanyId"] = t.CompanyId;
				p["@SchoolId"] = t.SchoolId;
				p["@CreatedBy"] = t.CreatedBy;
				p["@Status"] = t.Status ?? string.Empty;
				p["@StatusMessage"] = t.StatusMessage ?? string.Empty;
				
				_logger.LogInformation("Executing Teacher_Create stored procedure");
				var dt = new DataTable();
				p.Exec(dt);
				_logger.LogInformation("Teacher_Create stored procedure executed, rows returned: {RowCount}", dt.Rows.Count);
				
				// The Teacher_Create stored procedure returns the ID using SELECT Id = @NewId
				if (dt.Rows.Count > 0)
				{
					var idObj = dt.Rows[0]["Id"];
					if (idObj != null && Guid.TryParse(idObj.ToString(), out var newId))
					{
						_logger.LogInformation("Successfully created teacher with ID: {TeacherId}", newId);
						return newId;
					}
					else
					{
						_logger.LogWarning("Failed to parse ID from stored procedure result");
					}
				}
				else
				{
					_logger.LogWarning("No rows returned from Teacher_Create stored procedure");
				}
				return Guid.Empty;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating teacher: {Message}", ex.Message);
				// Log the exception for debugging
				throw new Exception($"Error creating teacher: {ex.Message}", ex);
			}
		}

		public bool Update(TeacherMaster t)
		{
			Proc p = new Proc("Teacher_Update");
			p["@Id"] = t.Id;
			p["@FirstName"] = t.FirstName;
			p["@LastName"] = t.LastName ?? string.Empty;
			p["@DOB"] = t.DOB;
			p["@DOJ"] = (object?)t.DOJ ?? DBNull.Value;
			p["@DateOfLeaving"] = (object?)t.DateOfLeaving ?? DBNull.Value;
			p["@Address"] = t.Address ?? string.Empty;
			p["@CityId"] = (object?)t.CityId ?? DBNull.Value;
			p["@StateId"] = (object?)t.StateId ?? DBNull.Value;
			p["@CountryId"] = (object?)t.CountryId ?? DBNull.Value;
			p["@ZipCode"] = t.ZipCode ?? string.Empty;
			p["@Gender"] = (object?)t.Gender ?? DBNull.Value;
			p["@MaritalStatusId"] = (object?)t.MaritalStatusId ?? DBNull.Value;
			p["@Image"] = t.Image ?? string.Empty;
			p["@Email"] = t.Email ?? string.Empty;
			p["@Phone"] = t.Phone ?? string.Empty;
			p["@MobilePhone"] = t.MobilePhone ?? string.Empty;
			p["@YearsOfExperience"] = t.YearsOfExperience ?? string.Empty;
			p["@PreviousSchool"] = t.PreviousSchool ?? string.Empty;
			p["@Salutation"] = t.Salutation ?? string.Empty;
			p["@IsActive"] = t.IsActive;
			p["@SchoolId"] = t.SchoolId;
			p["@ModifiedBy"] = t.ModifiedBy ?? Guid.Empty;
			p["@Status"] = t.Status ?? string.Empty;
			p["@StatusMessage"] = t.StatusMessage ?? string.Empty;
			p["@IsDeleted"] = t.IsDeleted;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}

		public bool Delete(Guid id)
		{
			Proc p = new Proc("Teacher_Delete");
			p["@Id"] = id;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}
	}
}
