using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using System.Data.Common;

namespace SchoolPortal.Services
{
	public class DeptMasterService : IDeptMasterService
	{
		private static DeptMaster Map(DataRow r)
		{
			var d = new DeptMaster();
			if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) d.Id = id;
			d.DeptCode = r.Table.Columns.Contains("DeptCode") ? r["DeptCode"].ToString() ?? string.Empty : string.Empty;
			d.DeptName = r.Table.Columns.Contains("DeptName") ? r["DeptName"].ToString() ?? string.Empty : string.Empty;
			if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) d.IsActive = active;
			if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) d.CompanyId = companyId;
			if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) d.SchoolId = schoolId;
			if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) d.CreatedBy = createdBy;
			if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) d.CreatedDate = createdDate;
			if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) d.ModifiedBy = modifiedBy;
			if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) d.ModifiedDate = modifiedDate;
			d.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
			d.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
			return d;
		}

		public List<DeptMaster> GetAll()
		{
			var list = new List<DeptMaster>();
			Proc p = new Proc("DeptMaster_GetAll");
			var dt = new DataTable();
			p.Exec(dt);
			foreach (DataRow r in dt.Rows)
			{
				list.Add(Map(r));
			}
			return list;
		}

		public List<DeptMaster> GetBySchool(Guid schoolId)
		{
			var list = new List<DeptMaster>();
			Proc p = new Proc("DeptMaster_GetBySchool");
			p["@SchoolId"] = schoolId;
			var dt = new DataTable();
			p.Exec(dt);
			foreach (DataRow r in dt.Rows)
			{
				list.Add(Map(r));
			}
			return list;
		}

		public DeptMaster? GetById(Guid id)
		{
			Proc p = new Proc("DeptMaster_GetById");
			p["@Id"] = id;
			var dt = new DataTable();
			p.Exec(dt);
			if (dt.Rows.Count == 0) return null;
			return Map(dt.Rows[0]);
		}

		public Guid Create(DeptMaster dept)
		{
			Proc p = new Proc("DeptMaster_Create");
			p["@DeptCode"] = dept.DeptCode;
			p["@DeptName"] = dept.DeptName;
			p["@IsActive"] = dept.IsActive;
			p["@CompanyId"] = dept.CompanyId;
			p["@SchoolId"] = dept.SchoolId;
			p["@CreatedBy"] = dept.CreatedBy;
			var dt = new DataTable();
			p.Exec(dt);
			if (dt.Rows.Count > 0)
			{
				var idObj = dt.Rows[0]["Id"];
				if (idObj != null && Guid.TryParse(idObj.ToString(), out var newId))
				{
					return newId;
				}
			}
			return Guid.Empty;
		}

		public bool Update(DeptMaster dept)
		{
			Proc p = new Proc("DeptMaster_Update");
			p["@Id"] = dept.Id;
			p["@DeptCode"] = dept.DeptCode;
			p["@DeptName"] = dept.DeptName;
			p["@IsActive"] = dept.IsActive;
			p["@SchoolId"] = dept.SchoolId;
			p["@ModifiedBy"] = dept.ModifiedBy ?? Guid.Empty;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}

		public bool Delete(Guid id)
		{
			Proc p = new Proc("DeptMaster_Delete");
			p["@Id"] = id;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}

		public void BulkInsert(IEnumerable<DeptMaster> departments)
		{
			if (departments == null || !departments.Any())
				return;

			// Create a DataTable to hold the department data
			var table = new DataTable();
			table.Columns.Add("Id", typeof(Guid));
			table.Columns.Add("DeptCode", typeof(string));
			table.Columns.Add("DeptName", typeof(string));
			table.Columns.Add("SchoolId", typeof(Guid));
			table.Columns.Add("IsActive", typeof(bool));
			table.Columns.Add("CreatedBy", typeof(Guid));
			table.Columns.Add("CreatedOn", typeof(DateTime));
			table.Columns.Add("CompanyId", typeof(Guid));
			
			// Add rows to DataTable
			foreach (var dept in departments)
			{
				table.Rows.Add(
					dept.Id,
					dept.DeptCode,
					dept.DeptName,
					dept.SchoolId,
					dept.IsActive,
					dept.CreatedBy,
					dept.CompanyId
				);
			}

			// Use the same pattern as other methods with a stored procedure
			using (var p = new Proc("DeptMaster_BulkInsert"))
			{
				p["@Departments"] = table;
				p.Exec();
			}
		}
	}
}