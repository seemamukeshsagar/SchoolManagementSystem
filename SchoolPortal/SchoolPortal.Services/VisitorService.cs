using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
	public class VisitorService : IVisitorService
	{
		private static VisitorMaster Map(DataRow r)
		{
			var v = new VisitorMaster();
			if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"]?.ToString(), out var id)) v.Id = id;
			v.VehicleNumber = r.Table.Columns.Contains("VehicleNumber") ? r["VehicleNumber"]?.ToString() ?? string.Empty : string.Empty;
			v.VehicleName = r.Table.Columns.Contains("VehicleName") ? r["VehicleName"]?.ToString() ?? string.Empty : string.Empty;
			if (r.Table.Columns.Contains("DateOfEntry") && DateTime.TryParse(r["DateOfEntry"]?.ToString(), out var doe)) v.DateOfEntry = doe;
			if (r.Table.Columns.Contains("ArrivalTime") && TimeSpan.TryParse(r["ArrivalTime"]?.ToString(), out var at)) v.ArrivalTime = at;
			if (r.Table.Columns.Contains("ExitTime") && TimeSpan.TryParse(r["ExitTime"]?.ToString(), out var et)) v.ExitTime = et;
			v.Purpose = r.Table.Columns.Contains("Purpose") ? r["Purpose"]?.ToString() ?? string.Empty : string.Empty;
			v.ContactPerson = r.Table.Columns.Contains("ContactPerson") ? r["ContactPerson"]?.ToString() ?? string.Empty : string.Empty;
			v.Address1 = r.Table.Columns.Contains("Address1") ? r["Address1"]?.ToString() ?? string.Empty : string.Empty;
			v.Address2 = r.Table.Columns.Contains("Address2") ? r["Address2"]?.ToString() ?? string.Empty : string.Empty;
			if (r.Table.Columns.Contains("CityId") && Guid.TryParse(r["CityId"]?.ToString(), out var city)) v.CityId = city;
			if (r.Table.Columns.Contains("StateId") && Guid.TryParse(r["StateId"]?.ToString(), out var state)) v.StateId = state;
			if (r.Table.Columns.Contains("CountryId") && Guid.TryParse(r["CountryId"]?.ToString(), out var country)) v.CountryId = country;
			v.ZipCode = r.Table.Columns.Contains("ZipCode") ? r["ZipCode"]?.ToString() ?? string.Empty : string.Empty;
			if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"]?.ToString(), out var comp)) v.CompanyId = comp;
			if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"]?.ToString(), out var sch)) v.SchoolId = sch;
			if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"]?.ToString(), out var active)) v.IsActive = active;
			if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"]?.ToString(), out var deleted)) v.IsDeleted = deleted;
			if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"]?.ToString(), out var createdBy)) v.CreatedBy = createdBy;
			if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"]?.ToString(), out var createdDate)) v.CreatedDate = createdDate;
			if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"]?.ToString(), out var modifiedBy)) v.ModifiedBy = modifiedBy;
			if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"]?.ToString(), out var modifiedDate)) v.ModifiedDate = modifiedDate;
			v.Status = r.Table.Columns.Contains("Status") ? r["Status"]?.ToString() ?? string.Empty : string.Empty;
			v.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"]?.ToString() ?? string.Empty : string.Empty;
			return v;
		}

		public List<VisitorMaster> GetAll()
		{
			var list = new List<VisitorMaster>();
			Proc p = new Proc("Visitor_GetAll");
			var dt = new DataTable();
			p.Exec(dt);
			foreach (DataRow r in dt.Rows) list.Add(Map(r));
			return list;
		}

		public VisitorMaster? GetById(Guid id)
		{
			Proc p = new Proc("Visitor_GetById");
			p["@Id"] = id;
			var dt = new DataTable();
			p.Exec(dt);
			if (dt.Rows.Count == 0) return null;
			return Map(dt.Rows[0]);
		}

		public Guid Create(VisitorMaster entity)
		{
			Proc p = new Proc("Visitor_Create");
			p["@VehicleNumber"] = entity.VehicleNumber ?? string.Empty;
			p["@VehicleName"] = entity.VehicleName ?? string.Empty;
			p["@DateOfEntry"] = entity.DateOfEntry;
			p["@ArrivalTime"] = entity.ArrivalTime;
			p["@ExitTime"] = entity.ExitTime;
			p["@Purpose"] = entity.Purpose ?? string.Empty;
			p["@ContactPerson"] = entity.ContactPerson ?? string.Empty;
			p["@Address1"] = entity.Address1 ?? string.Empty;
			p["@Address2"] = entity.Address2 ?? string.Empty;
			p["@CityId"] = entity.CityId;
			p["@StateId"] = entity.StateId;
			p["@CountryId"] = entity.CountryId;
			p["@ZipCode"] = entity.ZipCode ?? string.Empty;
			p["@CompanyId"] = entity.CompanyId ?? (object)DBNull.Value;
			p["@SchoolId"] = entity.SchoolId ?? (object)DBNull.Value;
			p["@IsActive"] = entity.IsActive;
			p["@CreatedBy"] = entity.CreatedBy;
			var dt = new DataTable();
			p.Exec(dt);
			if (dt.Rows.Count > 0 && Guid.TryParse(dt.Rows[0]["Id"]?.ToString(), out var newId)) return newId;
			return Guid.Empty;
		}

		public bool Update(VisitorMaster entity)
		{
			Proc p = new Proc("Visitor_Update");
			p["@Id"] = entity.Id;
			p["@VehicleNumber"] = entity.VehicleNumber ?? string.Empty;
			p["@VehicleName"] = entity.VehicleName ?? string.Empty;
			p["@DateOfEntry"] = entity.DateOfEntry;
			p["@ArrivalTime"] = entity.ArrivalTime;
			p["@ExitTime"] = entity.ExitTime;
			p["@Purpose"] = entity.Purpose ?? string.Empty;
			p["@ContactPerson"] = entity.ContactPerson ?? string.Empty;
			p["@Address1"] = entity.Address1 ?? string.Empty;
			p["@Address2"] = entity.Address2 ?? string.Empty;
			p["@CityId"] = entity.CityId;
			p["@StateId"] = entity.StateId;
			p["@CountryId"] = entity.CountryId;
			p["@ZipCode"] = entity.ZipCode ?? string.Empty;
			p["@CompanyId"] = entity.CompanyId ?? (object)DBNull.Value;
			p["@SchoolId"] = entity.SchoolId ?? (object)DBNull.Value;
			p["@IsActive"] = entity.IsActive;
			p["@ModifiedBy"] = entity.ModifiedBy ?? Guid.Empty;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}

		public bool Delete(Guid id)
		{
			Proc p = new Proc("Visitor_Delete");
			p["@Id"] = id;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}
	}
}