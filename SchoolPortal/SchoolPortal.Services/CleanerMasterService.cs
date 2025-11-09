using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class CleanerMasterService : ICleanerMasterService
    {
        private static CleanerMaster Map(DataRow r)
        {
            var e = new CleanerMaster
            {
                Id = r.Field<Guid>("Id"),
                Name = r.Table.Columns.Contains("Name") ? r.Field<string>("Name") : string.Empty,
                Image = r.Table.Columns.Contains("Image") ? r.Field<string>("Image") : string.Empty,
                FatherName = r.Table.Columns.Contains("FatherName") ? r.Field<string>("FatherName") : string.Empty,
                Description = r.Table.Columns.Contains("Description") ? r.Field<string>("Description") : string.Empty,
                IsActive = r.Table.Columns.Contains("IsActive") && r.Field<bool>("IsActive"),
                IsDeleted = r.Table.Columns.Contains("IsDeleted") && r.Field<bool>("IsDeleted"),
                CompanyId = r.Table.Columns.Contains("CompanyId") ? r.Field<Guid>("CompanyId") : Guid.Empty,
                SchoolId = r.Table.Columns.Contains("SchoolId") ? r.Field<Guid>("SchoolId") : Guid.Empty,
                CreatedBy = r.Table.Columns.Contains("CreatedBy") ? r.Field<Guid>("CreatedBy") : Guid.Empty,
                CreatedDate = r.Table.Columns.Contains("CreatedDate") ? r.Field<DateTime>("CreatedDate") : DateTime.UtcNow,
                ModifiedBy = r.Table.Columns.Contains("ModifiedBy") ? r.Field<Guid?>("ModifiedBy") : null,
                ModifiedDate = r.Table.Columns.Contains("ModifiedDate") ? r.Field<DateTime?>("ModifiedDate") : null,
                Status = r.Table.Columns.Contains("Status") ? r.Field<string>("Status") : string.Empty,
                StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r.Field<string>("StatusMessage") : string.Empty
            };
            return e;
        }
        public Guid Create(CleanerMaster cleaner)
        {
            if (cleaner == null) throw new ArgumentNullException(nameof(cleaner));

            // Ensure Id is generated at DB or here as needed
            Proc p = new Proc("CleanerMaster_Create");
            p["@Name"] = cleaner.Name ?? string.Empty;
            p["@Image"] = cleaner.Image ?? string.Empty;
            p["@FatherName"] = cleaner.FatherName ?? string.Empty;
            p["@Description"] = cleaner.Description ?? string.Empty;
            p["@IsActive"] = cleaner.IsActive;
            p["@IsDeleted"] = cleaner.IsDeleted;
            p["@CompanyId"] = cleaner.CompanyId;
            p["@SchoolId"] = cleaner.SchoolId;
            p["@CreatedBy"] = cleaner.CreatedBy;
            p["@CreatedDate"] = cleaner.CreatedDate;
            p["@Status"] = cleaner.Status ?? string.Empty;
            p["@StatusMessage"] = cleaner.StatusMessage ?? string.Empty;

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

        public CleanerMaster? GetByKey(Guid companyId, Guid schoolId, string name)
        {
            Proc p = new Proc("CleanerMaster_GetByKey");
            p["@CompanyId"] = companyId;
            p["@SchoolId"] = schoolId;
            p["@Name"] = name ?? string.Empty;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public List<CleanerMaster> GetAll()
        {
            var list = new List<CleanerMaster>();
            Proc p = new Proc("CleanerMaster_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(Map(r));
            }
            return list;
        }

        public CleanerMaster? GetById(Guid id)
        {
            Proc p = new Proc("CleanerMaster_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public bool Update(CleanerMaster cleaner)
        {
            Proc p = new Proc("CleanerMaster_Update");
            p["@Id"] = cleaner.Id;
            p["@Name"] = cleaner.Name ?? string.Empty;
            p["@Image"] = cleaner.Image ?? string.Empty;
            p["@FatherName"] = cleaner.FatherName ?? string.Empty;
            p["@Description"] = cleaner.Description ?? string.Empty;
            p["@IsActive"] = cleaner.IsActive;
            p["@IsDeleted"] = cleaner.IsDeleted;
            p["@ModifiedBy"] = cleaner.ModifiedBy ?? Guid.Empty;
            p["@ModifiedDate"] = cleaner.ModifiedDate ?? DateTime.UtcNow;
            p["@Status"] = cleaner.Status ?? string.Empty;
            p["@StatusMessage"] = cleaner.StatusMessage ?? string.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("CleanerMaster_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}
