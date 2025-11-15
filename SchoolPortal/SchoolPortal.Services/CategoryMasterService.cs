using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
	public class CategoryMasterService : ICategoryMasterService
	{
		private static CategoryMaster Map(DataRow r)
		{
			var c = new CategoryMaster();
			if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) c.Id = id;
			c.Name = r.Table.Columns.Contains("Name") ? r["Name"].ToString() ?? string.Empty : string.Empty;
			if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) c.IsActive = active;
			if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) c.IsDeleted = deleted;
			if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) c.CreatedBy = createdBy;
			if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) c.CreatedDate = createdDate;
			if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) c.ModifiedBy = modifiedBy;
			if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) c.ModifiedDate = modifiedDate;
			c.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
			c.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
			return c;
		}

		public List<CategoryMaster> GetAll()
		{
			var list = new List<CategoryMaster>();
			Proc p = new Proc("Category_GetAll");
			var dt = new DataTable();
			p.Exec(dt);
			foreach (DataRow r in dt.Rows)
			{
				list.Add(Map(r));
			}
			return list;
		}

		public CategoryMaster? GetById(Guid id)
		{
			Proc p = new Proc("Category_GetById");
			p["@Id"] = id;
			var dt = new DataTable();
			p.Exec(dt);
			if (dt.Rows.Count == 0) return null;
			return Map(dt.Rows[0]);
		}

		public Guid Create(CategoryMaster category)
		{
			Proc p = new Proc("Category_Create");
			p["@Name"] = category.Name;
			p["@IsActive"] = category.IsActive;
			p["@CreatedBy"] = category.CreatedBy;
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

		public bool Update(CategoryMaster category)
		{
			Proc p = new Proc("Category_Update");
			p["@Id"] = category.Id;
			p["@Name"] = category.Name;
			p["@IsActive"] = category.IsActive;
			p["@ModifiedBy"] = category.ModifiedBy ?? Guid.Empty;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}

		public bool Delete(Guid id)
		{
			Proc p = new Proc("Category_Delete");
			p["@Id"] = id;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}
	}
}
