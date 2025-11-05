using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class CompanyService : ICompanyService
    {
        private static CompanyMaster MapCompany(DataRow r)
        {
            var c = new CompanyMaster();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) c.Id = id;
            c.CompanyName = r.Table.Columns.Contains("CompanyName") ? r["CompanyName"].ToString() ?? string.Empty : string.Empty;
            c.Description = r.Table.Columns.Contains("Description") ? r["Description"].ToString() ?? string.Empty : string.Empty;
            c.Address = r.Table.Columns.Contains("Address") ? r["Address"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("CityId") && Guid.TryParse(r["CityId"].ToString(), out var city)) c.CityId = city;
            if (r.Table.Columns.Contains("StateId") && Guid.TryParse(r["StateId"].ToString(), out var state)) c.StateId = state;
            if (r.Table.Columns.Contains("CountryId") && Guid.TryParse(r["CountryId"].ToString(), out var country)) c.CountryId = country;
            c.ZipCode = r.Table.Columns.Contains("ZipCode") ? r["ZipCode"].ToString() ?? string.Empty : string.Empty;
            c.Email = r.Table.Columns.Contains("Email") ? r["Email"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) c.IsActive = active;
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) c.IsDeleted = deleted;
            if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) c.CreatedBy = createdBy;
            if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) c.CreatedDate = createdDate;
            if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) c.ModifiedBy = modifiedBy;
            if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) c.ModifiedDate = modifiedDate;
            c.EstablishmentYear = r.Table.Columns.Contains("EstablishmentYear") ? r["EstablishmentYear"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("JudistrictionArea") && Guid.TryParse(r["JudistrictionArea"].ToString(), out var juris)) c.JudistrictionArea = juris;
            c.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
            c.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
            return c;
        }

        public List<CompanyMaster> GetAll()
        {
            var list = new List<CompanyMaster>();
            Proc p = new Proc("Company_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapCompany(r));
            }
            return list;
        }

        public CompanyMaster? GetById(Guid id)
        {
            Proc p = new Proc("Company_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return MapCompany(dt.Rows[0]);
        }

        public Guid Create(CompanyMaster company)
        {
            Proc p = new Proc("Company_Create");
            p["@CompanyName"] = company.CompanyName;
            p["@Description"] = company.Description ?? string.Empty;
            p["@Address"] = company.Address ?? string.Empty;
            p["@CityId"] = company.CityId;
            p["@StateId"] = company.StateId;
            p["@CountryId"] = company.CountryId;
            p["@ZipCode"] = company.ZipCode ?? string.Empty;
            p["@Email"] = company.Email ?? string.Empty;
            p["@IsActive"] = company.IsActive;
            p["@CreatedBy"] = company.CreatedBy;
            p["@EstablishmentYear"] = company.EstablishmentYear ?? string.Empty;
            p["@JudistrictionArea"] = company.JudistrictionArea;
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

        public bool Update(CompanyMaster company)
        {
            Proc p = new Proc("Company_Update");
            p["@Id"] = company.Id;
            p["@CompanyName"] = company.CompanyName;
            p["@Description"] = company.Description ?? string.Empty;
            p["@Address"] = company.Address ?? string.Empty;
            p["@CityId"] = company.CityId;
            p["@StateId"] = company.StateId;
            p["@CountryId"] = company.CountryId;
            p["@ZipCode"] = company.ZipCode ?? string.Empty;
            p["@Email"] = company.Email ?? string.Empty;
            p["@IsActive"] = company.IsActive;
            p["@ModifiedBy"] = company.ModifiedBy ?? Guid.Empty;
            p["@EstablishmentYear"] = company.EstablishmentYear ?? string.Empty;
            p["@JudistrictionArea"] = company.JudistrictionArea;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("Company_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public string CompanyNameById(Guid id)
        {
            Proc p = new Proc("Company_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return string.Empty;
            var nameObj = dt.Rows[0]["CompanyName"];
            return nameObj?.ToString() ?? string.Empty;
        }
    }
}
