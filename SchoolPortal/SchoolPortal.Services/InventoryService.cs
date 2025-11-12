using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class InventoryService : IInventoryService
    {
        private static InventoryMaster Map(DataRow r)
        {
            var e = new InventoryMaster();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"]?.ToString(), out var id)) e.Id = id;
            e.Name = r.Table.Columns.Contains("Name") ? r["Name"]?.ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("ItemId") && Guid.TryParse(r["ItemId"]?.ToString(), out var itemId)) e.ItemId = itemId;
            if (r.Table.Columns.Contains("LocationId") && Guid.TryParse(r["LocationId"]?.ToString(), out var locId)) e.LocationId = locId;
            if (r.Table.Columns.Contains("Quantity") && int.TryParse(r["Quantity"]?.ToString(), out var qty)) e.Quantity = qty;
            if (r.Table.Columns.Contains("CostPerItem") && decimal.TryParse(r["CostPerItem"]?.ToString(), out var cpi)) e.CostPerItem = cpi;
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"]?.ToString(), out var active)) e.IsActive = active;
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"]?.ToString(), out var del)) e.IsDeleted = del;
            if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"]?.ToString(), out var cmpId)) e.CompanyId = cmpId;
            if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"]?.ToString(), out var schId)) e.SchoolId = schId;
            if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"]?.ToString(), out var cb)) e.CreatedBy = cb;
            if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"]?.ToString(), out var cd)) e.CreatedDate = cd;
            if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"]?.ToString(), out var mb)) e.ModifiedBy = mb;
            if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"]?.ToString(), out var md)) e.ModifiedDate = md;
            e.Status = r.Table.Columns.Contains("Status") ? r["Status"]?.ToString() ?? string.Empty : string.Empty;
            e.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"]?.ToString() ?? string.Empty : string.Empty;
            return e;
        }

        public List<InventoryMaster> GetAll()
        {
            var list = new List<InventoryMaster>();
            var p = new Proc("InventoryMaster_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows) list.Add(Map(r));
            return list;
        }

        public InventoryMaster? GetById(Guid id)
        {
            var p = new Proc("InventoryMaster_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public Guid Create(InventoryMaster e)
        {
            var p = new Proc("InventoryMaster_Create");
            p["@Name"] = e.Name;
            p["@ItemId"] = e.ItemId;
            p["@LocationId"] = e.LocationId;
            p["@Quantity"] = e.Quantity ?? 0;
            p["@CostPerItem"] = e.CostPerItem ?? 0m;
            p["@IsActive"] = e.IsActive ?? true;
            p["@CompanyId"] = e.CompanyId;
            p["@SchoolId"] = e.SchoolId;
            p["@CreatedBy"] = e.CreatedBy;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count > 0)
            {
                var idObj = dt.Rows[0]["Id"];
                if (idObj != null && Guid.TryParse(idObj.ToString(), out var newId)) return newId;
            }
            return Guid.Empty;
        }

        public bool Update(InventoryMaster e)
        {
            var p = new Proc("InventoryMaster_Update");
            p["@Id"] = e.Id;
            p["@Name"] = e.Name;
            p["@ItemId"] = e.ItemId;
            p["@LocationId"] = e.LocationId;
            p["@Quantity"] = e.Quantity ?? 0;
            p["@CostPerItem"] = e.CostPerItem ?? 0m;
            p["@IsActive"] = e.IsActive ?? true;
            p["@CompanyId"] = e.CompanyId;
            p["@SchoolId"] = e.SchoolId;
            p["@ModifiedBy"] = e.ModifiedBy == Guid.Empty ? (object)DBNull.Value : e.ModifiedBy;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            var p = new Proc("InventoryMaster_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}