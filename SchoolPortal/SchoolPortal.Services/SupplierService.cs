using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class SupplierService : ISupplierService
    {
        private static SupplierMaster MapSupplier(DataRow r)
        {
            var s = new SupplierMaster();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) s.Id = id;
            s.Name = r.Table.Columns.Contains("Name") ? r["Name"].ToString() ?? string.Empty : string.Empty;
            s.Description = r.Table.Columns.Contains("Description") ? r["Description"].ToString() ?? string.Empty : string.Empty;
            s.Address1 = r.Table.Columns.Contains("Address1") ? r["Address1"].ToString() ?? string.Empty : string.Empty;
            s.Address2 = r.Table.Columns.Contains("Address2") ? r["Address2"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("CityId") && Guid.TryParse(r["CityId"].ToString(), out var city)) s.CityId = city;
            if (r.Table.Columns.Contains("StateId") && Guid.TryParse(r["StateId"].ToString(), out var state)) s.StateId = state;
            if (r.Table.Columns.Contains("CountryId") && Guid.TryParse(r["CountryId"].ToString(), out var country)) s.CountryId = country;
            s.ZipCode = r.Table.Columns.Contains("ZipCode") ? r["ZipCode"].ToString() ?? string.Empty : string.Empty;
            s.PhonbeNumber = r.Table.Columns.Contains("PhonbeNumber") ? r["PhonbeNumber"].ToString() ?? string.Empty : string.Empty;
            s.MobileNumber = r.Table.Columns.Contains("MobileNumber") ? r["MobileNumber"].ToString() ?? string.Empty : string.Empty;
            s.EmailId = r.Table.Columns.Contains("EmailId") ? r["EmailId"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var company)) s.CompanyId = company;
            if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var school)) s.SchoolId = school;
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) s.IsActive = active;
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) s.IsDeleted = deleted;
            if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) s.CreatedBy = createdBy;
            if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) s.CreatedDate = createdDate;
            if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) s.ModifiedBy = modifiedBy;
            if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) s.ModifiedDate = modifiedDate;
            s.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
            s.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
            return s;
        }

        public List<SupplierMaster> GetAll()
        {
            var list = new List<SupplierMaster>();
            Proc p = new Proc("Supplier_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapSupplier(r));
            }
            return list;
        }

        public SupplierMaster? GetById(Guid id)
        {
            Proc p = new Proc("Supplier_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return MapSupplier(dt.Rows[0]);
        }

        public Guid Create(SupplierMaster supplier)
        {
            Proc p = new Proc("Supplier_Create");
            p["@Name"] = supplier.Name;
            p["@Description"] = supplier.Description ?? string.Empty;
            p["@Address1"] = supplier.Address1 ?? string.Empty;
            p["@Address2"] = supplier.Address2 ?? string.Empty;
            p["@CityId"] = supplier.CityId;
            p["@StateId"] = supplier.StateId;
            p["@CountryId"] = supplier.CountryId;
            p["@ZipCode"] = supplier.ZipCode ?? string.Empty;
            p["@PhonbeNumber"] = supplier.PhonbeNumber ?? string.Empty;
            p["@MobileNumber"] = supplier.MobileNumber ?? string.Empty;
            p["@EmailId"] = supplier.EmailId ?? string.Empty;
            p["@CompanyId"] = supplier.CompanyId;
            p["@SchoolId"] = supplier.SchoolId;
            p["@IsActive"] = supplier.IsActive;
            p["@CreatedBy"] = supplier.CreatedBy;
            var dt = new DataTable();
            p.Exec(dt);
            // Expected: first row contains new Id as column 'Id'
            if (dt.Rows.Count > 0)
            {
                var idObj = dt.Rows[0]["Id"];
                if (idObj != null && Guid.TryParse(idObj.ToString(), out var newIdFromSelect))
                {
                    return newIdFromSelect;
                }
            }
            return Guid.Empty;
        }

        public bool Update(SupplierMaster supplier)
        {
            Proc p = new Proc("Supplier_Update");
            p["@Id"] = supplier.Id;
            p["@Name"] = supplier.Name;
            p["@Description"] = supplier.Description ?? string.Empty;
            p["@Address1"] = supplier.Address1 ?? string.Empty;
            p["@Address2"] = supplier.Address2 ?? string.Empty;
            p["@CityId"] = supplier.CityId;
            p["@StateId"] = supplier.StateId;
            p["@CountryId"] = supplier.CountryId;
            p["@ZipCode"] = supplier.ZipCode ?? string.Empty;
            p["@PhonbeNumber"] = supplier.PhonbeNumber ?? string.Empty;
            p["@MobileNumber"] = supplier.MobileNumber ?? string.Empty;
            p["@EmailId"] = supplier.EmailId ?? string.Empty;
            p["@CompanyId"] = supplier.CompanyId;
            p["@SchoolId"] = supplier.SchoolId;
            p["@IsActive"] = supplier.IsActive;
            p["@ModifiedBy"] = supplier.ModifiedBy ?? Guid.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("Supplier_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public string SupplierNameById(Guid id)
        {
            Proc p = new Proc("Supplier_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return string.Empty;
            var nameObj = dt.Rows[0]["Name"];
            return nameObj?.ToString() ?? string.Empty;
        }
    }
}