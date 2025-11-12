using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class ItemTypeService : IItemTypeService
    {
        private static ItemTypeMaster MapItemType(DataRow r)
        {
            var it = new ItemTypeMaster();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) it.Id = id;
            it.Name = r.Table.Columns.Contains("Name") ? r["Name"].ToString() ?? string.Empty : string.Empty;
            it.Description = r.Table.Columns.Contains("Description") ? r["Description"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) it.IsActive = active;
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) it.IsDeleted = deleted;
            if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) it.CompanyId = companyId;
            if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) it.SchoolId = schoolId;
            if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) it.CreatedBy = createdBy;
            if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) it.CreatedDate = createdDate;
            if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) it.ModifiedBy = modifiedBy;
            if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) it.ModifiedDate = modifiedDate;
            it.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
            it.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
            return it;
        }

        public List<ItemTypeMaster> GetAll()
        {
            var list = new List<ItemTypeMaster>();
            Proc p = new Proc("ItemType_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapItemType(r));
            }
            return list;
        }

        public ItemTypeMaster? GetById(Guid id)
        {
            Proc p = new Proc("ItemType_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return MapItemType(dt.Rows[0]);
        }

        public Guid Create(ItemTypeMaster itemType)
        {
            Proc p = new Proc("ItemType_Create");
            p["@Name"] = itemType.Name;
            p["@Description"] = itemType.Description ?? string.Empty;
            p["@IsActive"] = itemType.IsActive ?? true;
            p["@CompanyId"] = itemType.CompanyId;
            p["@SchoolId"] = itemType.SchoolId;
            p["@CreatedBy"] = itemType.CreatedBy;
            var dt = new DataTable();
            p.Exec(dt);
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

        public bool Update(ItemTypeMaster itemType)
        {
            Proc p = new Proc("ItemType_Update");
            p["@Id"] = itemType.Id;
            p["@Name"] = itemType.Name;
            p["@Description"] = itemType.Description ?? string.Empty;
            p["@IsActive"] = itemType.IsActive ?? true;
            p["@CompanyId"] = itemType.CompanyId;
            p["@SchoolId"] = itemType.SchoolId;
            p["@ModifiedBy"] = itemType.ModifiedBy == Guid.Empty ? (object)DBNull.Value : itemType.ModifiedBy;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("ItemType_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public string ItemTypeNameById(Guid id)
        {
            Proc p = new Proc("ItemType_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return string.Empty;
            var nameObj = dt.Rows[0]["Name"];
            return nameObj?.ToString() ?? string.Empty;
        }
    }
}