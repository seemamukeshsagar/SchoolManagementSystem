using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class VendorService : IVendorService
    {
        private static VendorMaster MapVendor(DataRow r)
        {
            var v = new VendorMaster();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) v.Id = id;
            v.VendorName = r.Table.Columns.Contains("VendorName") ? r["VendorName"].ToString() ?? string.Empty : string.Empty;
            v.Description = r.Table.Columns.Contains("Description") ? r["Description"].ToString() ?? string.Empty : string.Empty;
            v.Address1 = r.Table.Columns.Contains("Address1") ? r["Address1"].ToString() ?? string.Empty : string.Empty;
            v.Address2 = r.Table.Columns.Contains("Address2") ? r["Address2"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("CityId") && Guid.TryParse(r["CityId"].ToString(), out var city)) v.CityId = city;
            if (r.Table.Columns.Contains("StateId") && Guid.TryParse(r["StateId"].ToString(), out var state)) v.StateId = state;
            if (r.Table.Columns.Contains("CountryId") && Guid.TryParse(r["CountryId"].ToString(), out var country)) v.CountryId = country;
            v.ZipCode = r.Table.Columns.Contains("ZipCode") ? r["ZipCode"].ToString() ?? string.Empty : string.Empty;
            v.ContactNumber = r.Table.Columns.Contains("ContactNumber") ? r["ContactNumber"].ToString() ?? string.Empty : string.Empty;
            v.MobileNumber = r.Table.Columns.Contains("MobileNumber") ? r["MobileNumber"].ToString() ?? string.Empty : string.Empty;
            v.EmailId = r.Table.Columns.Contains("EmailId") ? r["EmailId"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var company)) v.CompanyId = company;
            if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var school)) v.SchoolId = school;
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) v.IsActive = active;
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) v.IsDeleted = deleted;
            if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) v.CreatedBy = createdBy;
            if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) v.CreatedDate = createdDate;
            if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) v.ModifiedBy = modifiedBy;
            if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) v.ModifiedDate = modifiedDate;
            v.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
            v.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
            return v;
        }

        public List<VendorMaster> GetAll()
        {
            var list = new List<VendorMaster>();
            Proc p = new Proc("Vendor_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapVendor(r));
            }
            return list;
        }

        public VendorMaster? GetById(Guid id)
        {
            Proc p = new Proc("Vendor_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return MapVendor(dt.Rows[0]);
        }

        public Guid Create(VendorMaster vendor)
        {
            Proc p = new Proc("Vendor_Create");
            p["@VendorName"] = vendor.VendorName;
            p["@Description"] = vendor.Description ?? string.Empty;
            p["@Address1"] = vendor.Address1 ?? string.Empty;
            p["@Address2"] = vendor.Address2 ?? string.Empty;
            p["@CityId"] = vendor.CityId;
            p["@StateId"] = vendor.StateId;
            p["@CountryId"] = vendor.CountryId;
            p["@ZipCode"] = vendor.ZipCode ?? string.Empty;
            p["@ContactNumber"] = vendor.ContactNumber ?? string.Empty;
            p["@MobileNumber"] = vendor.MobileNumber ?? string.Empty;
            p["@EmailId"] = vendor.EmailId ?? string.Empty;
            p["@CompanyId"] = vendor.CompanyId ?? Guid.Empty;
            p["@SchoolId"] = vendor.SchoolId ?? Guid.Empty;
            p["@IsActive"] = vendor.IsActive;
            p["@CreatedBy"] = vendor.CreatedBy;
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

        public bool Update(VendorMaster vendor)
        {
            Proc p = new Proc("Vendor_Update");
            p["@Id"] = vendor.Id;
            p["@VendorName"] = vendor.VendorName;
            p["@Description"] = vendor.Description ?? string.Empty;
            p["@Address1"] = vendor.Address1 ?? string.Empty;
            p["@Address2"] = vendor.Address2 ?? string.Empty;
            p["@CityId"] = vendor.CityId;
            p["@StateId"] = vendor.StateId;
            p["@CountryId"] = vendor.CountryId;
            p["@ZipCode"] = vendor.ZipCode ?? string.Empty;
            p["@ContactNumber"] = vendor.ContactNumber ?? string.Empty;
            p["@MobileNumber"] = vendor.MobileNumber ?? string.Empty;
            p["@EmailId"] = vendor.EmailId ?? string.Empty;
            p["@CompanyId"] = vendor.CompanyId ?? Guid.Empty;
            p["@SchoolId"] = vendor.SchoolId ?? Guid.Empty;
            p["@IsActive"] = vendor.IsActive;
            p["@ModifiedBy"] = vendor.ModifiedBy ?? Guid.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("Vendor_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public string VendorNameById(Guid id)
        {
            Proc p = new Proc("Vendor_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return string.Empty;
            var nameObj = dt.Rows[0]["VendorName"];
            return nameObj?.ToString() ?? string.Empty;
        }
    }
}