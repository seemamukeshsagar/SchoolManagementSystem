using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class ItemService : IItemService
    {
        private static ItemMaster MapItem(DataRow r)
        {
            var e = new ItemMaster();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"]?.ToString(), out var id)) e.Id = id;
            e.ItemName = r.Table.Columns.Contains("ItemName") ? r["ItemName"]?.ToString() ?? string.Empty : string.Empty;
            e.Description = r.Table.Columns.Contains("Description") ? r["Description"]?.ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("ItemTypeMasterId") && Guid.TryParse(r["ItemTypeMasterId"]?.ToString(), out var itmType)) e.ItemTypeMasterId = itmType;
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"]?.ToString(), out var active)) e.IsActive = active;
            if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"]?.ToString(), out var cmpId)) e.CompanyId = cmpId;
            if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"]?.ToString(), out var schId)) e.SchoolId = schId;
            if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"]?.ToString(), out var cb)) e.CreatedBy = cb;
            if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"]?.ToString(), out var cd)) e.CreatedDate = cd;
            if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"]?.ToString(), out var mb)) e.ModifiedBy = mb;
            if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"]?.ToString(), out var md)) e.ModifiedDate = md;
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"]?.ToString(), out var del)) e.IsDeleted = del;
            e.Status = r.Table.Columns.Contains("Status") ? r["Status"]?.ToString() ?? string.Empty : string.Empty;
            e.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"]?.ToString() ?? string.Empty : string.Empty;
            return e;
        }

        public List<ItemMaster> GetAll()
        {
            var list = new List<ItemMaster>();
            Proc p = new Proc("ItemMaster_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapItem(r));
            }
            return list;
        }

        public ItemMaster? GetById(Guid id)
        {
            Proc p = new Proc("ItemMaster_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return MapItem(dt.Rows[0]);
        }

        public Guid Create(ItemMaster item)
        {
            Proc p = new Proc("ItemMaster_Create");
            p["@ItemName"] = item.ItemName;
            p["@Description"] = item.Description ?? string.Empty;
            p["@ItemTypeMasterId"] = item.ItemTypeMasterId;
            p["@IsActive"] = item.IsActive ?? false;
            p["@CompanyId"] = item.CompanyId;
            p["@SchoolId"] = item.SchoolId;
            p["@CreatedBy"] = item.CreatedBy;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count > 0)
            {
                var idObj = dt.Rows[0]["Id"];
                if (idObj != null && Guid.TryParse(idObj.ToString(), out var newId)) return newId;
            }
            return Guid.Empty;
        }

        public bool Update(ItemMaster item)
        {
            Proc p = new Proc("ItemMaster_Update");
            p["@Id"] = item.Id;
            p["@ItemName"] = item.ItemName;
            p["@Description"] = item.Description ?? string.Empty;
            p["@ItemTypeMasterId"] = item.ItemTypeMasterId;
            p["@IsActive"] = item.IsActive ?? false;
            p["@CompanyId"] = item.CompanyId;
            p["@SchoolId"] = item.SchoolId;
            p["@ModifiedBy"] = item.ModifiedBy;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("ItemMaster_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}