using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class SchoolContactService : ISchoolContactService
    {
        private static SchoolContactMaster Map(DataRow r)
        {
            var s = new SchoolContactMaster();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) s.Id = id;
            if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) s.SchoolId = schoolId;
            s.FirstName = r.Table.Columns.Contains("FirstName") ? r["FirstName"].ToString() ?? string.Empty : string.Empty;
            s.LastName = r.Table.Columns.Contains("LastName") ? r["LastName"].ToString() ?? string.Empty : string.Empty;
            s.Email = r.Table.Columns.Contains("Email") ? r["Email"].ToString() ?? string.Empty : string.Empty;
            s.Phone = r.Table.Columns.Contains("Phone") ? r["Phone"].ToString() ?? string.Empty : string.Empty;
            s.MobilePhone = r.Table.Columns.Contains("MobilePhone") ? r["MobilePhone"].ToString() ?? string.Empty : string.Empty;
            s.AddressLine1 = r.Table.Columns.Contains("AddressLine1") ? r["AddressLine1"].ToString() ?? string.Empty : string.Empty;
            s.AddressLine2 = r.Table.Columns.Contains("AddressLine2") ? r["AddressLine2"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("CityId") && Guid.TryParse(r["CityId"].ToString(), out var city)) s.CityId = city;
            if (r.Table.Columns.Contains("StateId") && Guid.TryParse(r["StateId"].ToString(), out var state)) s.StateId = state;
            if (r.Table.Columns.Contains("CountryId") && Guid.TryParse(r["CountryId"].ToString(), out var country)) s.CountryId = country;
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

        public List<SchoolContactMaster> GetAll()
        {
            var list = new List<SchoolContactMaster>();
            Proc p = new Proc("SchoolContact_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(Map(r));
            }
            return list;
        }

        public SchoolContactMaster? GetById(Guid id)
        {
            Proc p = new Proc("SchoolContact_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public Guid Create(SchoolContactMaster contact)
        {
            Proc p = new Proc("SchoolContact_Create");
            p["@SchoolId"] = contact.SchoolId;
            p["@FirstName"] = contact.FirstName ?? string.Empty;
            p["@LastName"] = contact.LastName ?? string.Empty;
            p["@Email"] = contact.Email ?? string.Empty;
            p["@Phone"] = contact.Phone ?? string.Empty;
            p["@MobilePhone"] = contact.MobilePhone ?? string.Empty;
            p["@AddressLine1"] = contact.AddressLine1 ?? string.Empty;
            p["@AddressLine2"] = contact.AddressLine2 ?? string.Empty;
            p["@CityId"] = contact.CityId;
            p["@StateId"] = contact.StateId;
            p["@CountryId"] = contact.CountryId;
            p["@IsActive"] = contact.IsActive;
            p["@CreatedBy"] = contact.CreatedBy;
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

        public bool Update(SchoolContactMaster contact)
        {
            Proc p = new Proc("SchoolContact_Update");
            p["@Id"] = contact.Id;
            p["@SchoolId"] = contact.SchoolId;
            p["@FirstName"] = contact.FirstName ?? string.Empty;
            p["@LastName"] = contact.LastName ?? string.Empty;
            p["@Email"] = contact.Email ?? string.Empty;
            p["@Phone"] = contact.Phone ?? string.Empty;
            p["@MobilePhone"] = contact.MobilePhone ?? string.Empty;
            p["@AddressLine1"] = contact.AddressLine1 ?? string.Empty;
            p["@AddressLine2"] = contact.AddressLine2 ?? string.Empty;
            p["@CityId"] = contact.CityId;
            p["@StateId"] = contact.StateId;
            p["@CountryId"] = contact.CountryId;
            p["@IsActive"] = contact.IsActive;
            p["@ModifiedBy"] = contact.ModifiedBy ?? Guid.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("SchoolContact_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}
