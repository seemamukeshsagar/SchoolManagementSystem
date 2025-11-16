using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services
{
	public class LookupService : ILookupService
	{
		private static List<LookupItem> Map(DataTable dt, string idCol = "Id", string nameCol = "Name")
		{
			var list = new List<LookupItem>();
			foreach (DataRow r in dt.Rows)
			{
				var item = new LookupItem();
				if (dt.Columns.Contains(idCol) && Guid.TryParse(r[idCol]?.ToString(), out var id)) item.Id = id;
				item.Name = dt.Columns.Contains(nameCol) ? (r[nameCol]?.ToString() ?? string.Empty) : string.Empty;
				list.Add(item);
			}
			return list;
		}

		public List<LookupItem> GetCountries()
		{
			try
			{
				Proc p = new Proc("Country_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				return Map(dt, "Id", "CountryName");
			}
			catch
			{
				// Fallback: return empty list if SP is missing to avoid runtime crash
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetStates(Guid countryId)
		{
			Proc p = new Proc("State_GetByCountry");
			p["@CountryId"] = countryId;
			var dt = new DataTable();
			p.Exec(dt);
			return Map(dt, "Id", "StateName");
		}

		public List<LookupItem> GetCities(Guid stateId)
		{
			Proc p = new Proc("City_GetByState");
			p["@StateId"] = stateId;
			var dt = new DataTable();
			p.Exec(dt);
			return Map(dt, "Id", "CityName");
		}

		public List<LookupItem> GetDepartments()
		{
			try
			{
				Proc p = new Proc("Department_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				return Map(dt, "Id", "DepartmentName");
			}
			catch (Exception)
			{
				// Log the exception if needed
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetDepartments(Guid schoolId)
		{
			try
			{
				Proc p = new Proc("Department_GetBySchool");
				p["@SchoolId"] = schoolId;
				var dt = new DataTable();
				p.Exec(dt);
				return Map(dt, "Id", "DepartmentName");
			}
			catch (Exception)
			{
				// Log the exception if needed
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetDesignations()
		{
			try
			{
				Proc p = new Proc("Designation_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				var nameCol = dt.Columns.Contains("Name") ? "Name" : (dt.Columns.Contains("DesignationName") ? "DesignationName" : "Name");
				return Map(dt, "Id", nameCol);
			}
			catch (Exception)
			{
				// Log the exception if needed
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetQualifications()
		{
			try
			{
				Proc p = new Proc("Qualification_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				return Map(dt, "Id", "QualificationName");
			}
			catch (Exception)
			{
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetRelationTypes()
		{
			try
			{
				Proc p = new Proc("RelationType_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				return Map(dt, "Id", "Name");
			}
			catch (Exception)
			{
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetCompanies()
		{
			try
			{
				Proc p = new Proc("Company_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				return Map(dt, "Id", "CompanyName");
			}
			catch (Exception)
			{
				// Log the exception if needed
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetSchools()
		{
			try
			{
				Proc p = new Proc("School_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				return Map(dt, "Id", "SchoolName");
			}
			catch (Exception)
			{
				// Log the exception if needed
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetGenders()
		{
			try
			{
				Proc p = new Proc("Gender_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				return Map(dt, "Id", "Gender");
			}
			catch (Exception)
			{
				// Log the exception if needed
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetPaymentModes()
		{
			try
			{
				Proc p = new Proc("PaymentMode_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				var nameCol = dt.Columns.Contains("Name")
					? "Name"
					: (dt.Columns.Contains("PaymentModeName") ? "PaymentModeName" : (dt.Columns.Contains("ModeName") ? "ModeName" : "Name"));
				var idCol = dt.Columns.Contains("Id")
					? "Id"
					: (dt.Columns.Contains("PaymentModeId") ? "PaymentModeId" : (dt.Columns.Contains("ModeId") ? "ModeId" : "Id"));
				return Map(dt, idCol, nameCol);
			}
			catch (Exception)
			{
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetPaymentModes(Guid schoolId)
		{
			try
			{
				Proc p = new Proc("PaymentMode_GetAll");
				p["@SchoolId"] = schoolId;
				var dt = new DataTable();
				p.Exec(dt);
				var nameCol = dt.Columns.Contains("Name")
					? "Name"
					: (dt.Columns.Contains("PaymentModeName") ? "PaymentModeName" : (dt.Columns.Contains("ModeName") ? "ModeName" : "Name"));
				var idCol = dt.Columns.Contains("Id")
					? "Id"
					: (dt.Columns.Contains("PaymentModeId") ? "PaymentModeId" : (dt.Columns.Contains("ModeId") ? "ModeId" : "Id"));
				return Map(dt, idCol, nameCol);
			}
			catch (Exception)
			{
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetEmployeeTypes()
		{
			try
			{
				Proc p = new Proc("EmployeeType_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				var nameCol = dt.Columns.Contains("Name")
					? "Name"
					: (dt.Columns.Contains("EmployeeTypeName") ? "EmployeeTypeName" : (dt.Columns.Contains("TypeName") ? "TypeName" : "Name"));
				var idCol = dt.Columns.Contains("Id")
					? "Id"
					: (dt.Columns.Contains("EmployeeTypeId") ? "EmployeeTypeId" : (dt.Columns.Contains("TypeId") ? "TypeId" : "Id"));
				return Map(dt, idCol, nameCol);
			}
			catch (Exception)
			{
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetEmployeeTypes(Guid schoolId)
		{
			try
			{
				Proc p = new Proc("EmployeeType_GetAll");
				p["@SchoolId"] = schoolId;
				var dt = new DataTable();
				p.Exec(dt);
				var nameCol = dt.Columns.Contains("Name")
					? "Name"
					: (dt.Columns.Contains("EmployeeTypeName") ? "EmployeeTypeName" : (dt.Columns.Contains("TypeName") ? "TypeName" : "Name"));
				var idCol = dt.Columns.Contains("Id")
					? "Id"
					: (dt.Columns.Contains("EmployeeTypeId") ? "EmployeeTypeId" : (dt.Columns.Contains("TypeId") ? "TypeId" : "Id"));
				return Map(dt, idCol, nameCol);
			}
			catch (Exception)
			{
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetEmployeeCategories()
		{
			try
			{
				Proc p = new Proc("EmployeeCategory_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				return Map(dt); // assume columns Id, Name
			}
			catch (Exception)
			{
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetGrades()
		{
			try
			{
				Proc p = new Proc("Grade_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				var nameCol = dt.Columns.Contains("Name") ? "Name" : (dt.Columns.Contains("GradeName") ? "GradeName" : "Name");
				var idCol = dt.Columns.Contains("Id") ? "Id" : (dt.Columns.Contains("GradeId") ? "GradeId" : "Id");
				return Map(dt, idCol, nameCol);
			}
			catch (Exception)
			{
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetGrades(Guid schoolId)
		{
			try
			{
				Proc p = new Proc("Grade_GetAll");
				p["@SchoolId"] = schoolId;
				var dt = new DataTable();
				p.Exec(dt);
				var nameCol = dt.Columns.Contains("Name") ? "Name" : (dt.Columns.Contains("GradeName") ? "GradeName" : "Name");
				var idCol = dt.Columns.Contains("Id") ? "Id" : (dt.Columns.Contains("GradeId") ? "GradeId" : "Id");
				return Map(dt, idCol, nameCol);
			}
			catch (Exception)
			{
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetCategories()
		{
			try
			{
				Proc p = new Proc("Category_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				// CategoryMaster has columns Id, Name
				return Map(dt, "Id", "Name");
			}
			catch (Exception)
			{
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetBloodGroups()
		{
			try
			{
				Proc p = new Proc("BloodGroup_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				return Map(dt); // assume columns Id, Name
			}
			catch (Exception)
			{
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetReligions()
		{
			try
			{
				Proc p = new Proc("Religion_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				return Map(dt); // assume columns Id, Name
			}
			catch (Exception)
			{
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetMaritalStatuses()
		{
			try
			{
				Proc p = new Proc("MaritalStatus_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				var nameCol = dt.Columns.Contains("Name") ? "Name" : (dt.Columns.Contains("MaritalStatusName") ? "MaritalStatusName" : (dt.Columns.Contains("StatusName") ? "StatusName" : "Name"));
				var idCol = dt.Columns.Contains("Id") ? "Id" : (dt.Columns.Contains("MaritalStatusId") ? "MaritalStatusId" : "Id");
				return Map(dt, idCol, nameCol);
			}
			catch (Exception)
			{
				return new List<LookupItem>();
			}
		}

		public List<LookupItem> GetSchoolBoards()
		{
			try
			{
				Proc p = new Proc("SchoolBoard_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				return Map(dt, "Id", "BoardName");
			}
			catch (Exception)
			{
				return new List<LookupItem>();
			}
		}

		public IEnumerable<ClassMaster> GetClasses()
		{
			try
			{
				Proc p = new Proc("ClassMaster_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				return MapToClassMasterList(dt);
			}
			catch (Exception)
			{
				// Log the exception if needed
				return new List<ClassMaster>();
			}
		}

		public IEnumerable<SectionMaster> GetSections()
		{
			try
			{
				Proc p = new Proc("SectionMaster_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				return MapToSectionMasterList(dt);
			}
			catch (Exception)
			{
				// Log the exception if needed
				return new List<SectionMaster>();
			}
		}

		public IEnumerable<LocationMaster> GetLocations()
		{
			try
			{
				Proc p = new Proc("LocationMaster_GetAll");
				var dt = new DataTable();
				p.Exec(dt);
				return MapToLocationList(dt);
			}
			catch (Exception)
			{
				// Log the exception if needed
				return new List<LocationMaster>();
			}
		}

		private List<ClassMaster> MapToClassMasterList(DataTable dt)
		{
			var list = new List<ClassMaster>();
			foreach (DataRow row in dt.Rows)
			{
				var item = new ClassMaster();
				if (Guid.TryParse(row["Id"]?.ToString(), out var id)) item.Id = id;
				item.Name = row["Name"]?.ToString() ?? string.Empty;
				// Map other properties as needed
				list.Add(item);
			}
			return list;
		}

		private List<SectionMaster> MapToSectionMasterList(DataTable dt)
		{
			var list = new List<SectionMaster>();
			foreach (DataRow row in dt.Rows)
			{
				var item = new SectionMaster();
				if (Guid.TryParse(row["Id"]?.ToString(), out var id)) item.Id = id;
				item.Name = row["Name"]?.ToString() ?? string.Empty;
				// Map other properties as needed
				list.Add(item);
			}
			return list;
		}

		private List<LocationMaster> MapToLocationList(DataTable dt)
		{
			var list = new List<LocationMaster>();
			foreach (DataRow row in dt.Rows)
			{
				var item = new LocationMaster();
				if (Guid.TryParse(row["Id"]?.ToString(), out var id)) item.Id = id;
				item.Name = row["Name"]?.ToString() ?? string.Empty;
				item.Code = row["Code"]?.ToString() ?? string.Empty;
				if (Guid.TryParse(row["CityId"]?.ToString(), out var cityId)) item.CityId = cityId;
				if (Guid.TryParse(row["CompanyId"]?.ToString(), out var companyId)) item.CompanyId = companyId;
				if (Guid.TryParse(row["SchoolId"]?.ToString(), out var schoolId)) item.SchoolId = schoolId;
				if (bool.TryParse(row["IsActive"]?.ToString(), out var isActive)) item.IsActive = isActive;
				if (bool.TryParse(row["IsDeleted"]?.ToString(), out var isDeleted)) item.IsDeleted = isDeleted;
				if (Guid.TryParse(row["CreatedBy"]?.ToString(), out var createdBy)) item.CreatedBy = createdBy;
				if (DateTime.TryParse(row["CreatedDate"]?.ToString(), out var createdDate)) item.CreatedDate = createdDate;
				if (row["ModifiedBy"] != DBNull.Value && Guid.TryParse(row["ModifiedBy"]?.ToString(), out var modifiedBy)) item.ModifiedBy = modifiedBy;
				if (row["ModifiedDate"] != DBNull.Value && DateTime.TryParse(row["ModifiedDate"]?.ToString(), out var modifiedDate)) item.ModifiedDate = modifiedDate;
				item.Status = row["Status"]?.ToString();
				item.StatusMessage = row["StatusMessage"]?.ToString();
				list.Add(item);
			}
			return list;
		}
	}
}
