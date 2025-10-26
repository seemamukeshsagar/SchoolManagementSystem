using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using Schoolortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class SchoolService : ISchoolService
    {
        private static SchoolMaster Map(DataRow r)
        {
            var s = new SchoolMaster();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) s.Id = id;
            s.Name = r.Table.Columns.Contains("Name") ? r["Name"].ToString() ?? string.Empty : string.Empty;
            s.Description = r.Table.Columns.Contains("Description") ? r["Description"].ToString() ?? string.Empty : string.Empty;
            s.Email = r.Table.Columns.Contains("Email") ? r["Email"].ToString() ?? string.Empty : string.Empty;
            s.Address1 = r.Table.Columns.Contains("Address1") ? r["Address1"].ToString() ?? string.Empty : string.Empty;
            s.Address2 = r.Table.Columns.Contains("Address2") ? r["Address2"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("CityId") && Guid.TryParse(r["CityId"].ToString(), out var city)) s.CityId = city;
            if (r.Table.Columns.Contains("StateId") && Guid.TryParse(r["StateId"].ToString(), out var state)) s.StateId = state;
            if (r.Table.Columns.Contains("CountryId") && Guid.TryParse(r["CountryId"].ToString(), out var country)) s.CountryId = country;
            s.ZipCode = r.Table.Columns.Contains("ZipCode") ? r["ZipCode"].ToString() ?? string.Empty : string.Empty;
            s.EstablishmentYear = r.Table.Columns.Contains("EstablishmentYear") ? r["EstablishmentYear"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("JudistrictionCityId") && Guid.TryParse(r["JudistrictionCityId"].ToString(), out var juris)) s.JudistrictionCityId = juris;
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

        public List<SchoolMaster> GetAll()
        {
            var list = new List<SchoolMaster>();
            Proc p = new Proc("School_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(Map(r));
            }
            return list;
        }

        public SchoolMaster? GetById(Guid id)
        {
            Proc p = new Proc("School_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public Guid Create(SchoolMaster school)
        {
            Proc p = new Proc("School_Create");
            p["@Name"] = school.Name;
            p["@Description"] = school.Description ?? string.Empty;
            p["@Email"] = school.Email ?? string.Empty;
            p["@Address1"] = school.Address1 ?? string.Empty;
            p["@Address2"] = school.Address2 ?? string.Empty;
            p["@CityId"] = school.CityId;
            p["@StateId"] = school.StateId;
            p["@CountryId"] = school.CountryId;
            p["@ZipCode"] = school.ZipCode ?? string.Empty;
            p["@EstablishmentYear"] = school.EstablishmentYear ?? string.Empty;
            p["@JudistrictionCityId"] = school.JudistrictionCityId;
            p["@IsActive"] = school.IsActive;
            p["@CompanyId"] = school.CompanyId;
            p["@CreatedBy"] = school.CreatedBy;
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

        public bool Update(SchoolMaster school)
        {
            Proc p = new Proc("School_Update");
            p["@Id"] = school.Id;
            p["@Name"] = school.Name;
            p["@Description"] = school.Description ?? string.Empty;
            p["@Email"] = school.Email ?? string.Empty;
            p["@Address1"] = school.Address1 ?? string.Empty;
            p["@Address2"] = school.Address2 ?? string.Empty;
            p["@CityId"] = school.CityId;
            p["@StateId"] = school.StateId;
            p["@CountryId"] = school.CountryId;
            p["@ZipCode"] = school.ZipCode ?? string.Empty;
            p["@EstablishmentYear"] = school.EstablishmentYear ?? string.Empty;
            p["@JudistrictionCityId"] = school.JudistrictionCityId;
            p["@IsActive"] = school.IsActive;
            p["@ModifiedBy"] = school.ModifiedBy ?? Guid.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("School_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}
