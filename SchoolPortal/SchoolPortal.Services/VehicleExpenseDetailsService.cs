using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class VehicleExpenseDetailsService : IVehicleExpenseDetailsService
    {
        private static VehicleExpenseDetails MapVehicleExpense(DataRow r)
        {
            var v = new VehicleExpenseDetails();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) v.Id = id;
            if (r.Table.Columns.Contains("VehicleId") && Guid.TryParse(r["VehicleId"].ToString(), out var vehicleId)) v.VehicleId = vehicleId;
            if (r.Table.Columns.Contains("VehicleTypeId") && Guid.TryParse(r["VehicleTypeId"].ToString(), out var vehicleType)) v.VehicleTypeId = vehicleType;
            v.Name = r.Table.Columns.Contains("Name") ? r["Name"].ToString() ?? string.Empty : string.Empty;
            v.Description = r.Table.Columns.Contains("Description") ? r["Description"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("ExpenseDate") && DateTime.TryParse(r["ExpenseDate"].ToString(), out var expenseDate)) v.ExpenseDate = expenseDate;
            if (r.Table.Columns.Contains("ExpenseAmount") && decimal.TryParse(r["ExpenseAmount"].ToString(), out var expenseAmount)) v.ExpenseAmount = expenseAmount;
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

        public List<VehicleExpenseDetails> GetAll()
        {
            var list = new List<VehicleExpenseDetails>();
            Proc p = new Proc("VehicleExpenseDetails_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapVehicleExpense(r));
            }
            return list;
        }

        public List<VehicleExpenseDetails> GetByVehicle(Guid vehicleId)
        {
            var list = new List<VehicleExpenseDetails>();
            Proc p = new Proc("VehicleExpenseDetails_GetByVehicle");
            p["@VehicleId"] = vehicleId;
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapVehicleExpense(r));
            }
            return list;
        }

        public List<VehicleExpenseDetails> GetByCompany(Guid companyId)
        {
            var list = new List<VehicleExpenseDetails>();
            Proc p = new Proc("VehicleExpenseDetails_GetByCompany");
            p["@CompanyId"] = companyId;
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapVehicleExpense(r));
            }
            return list;
        }

        public List<VehicleExpenseDetails> GetBySchool(Guid schoolId)
        {
            var list = new List<VehicleExpenseDetails>();
            Proc p = new Proc("VehicleExpenseDetails_GetBySchool");
            p["@SchoolId"] = schoolId;
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapVehicleExpense(r));
            }
            return list;
        }

        public VehicleExpenseDetails? GetById(Guid id)
        {
            Proc p = new Proc("VehicleExpenseDetails_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return MapVehicleExpense(dt.Rows[0]);
        }

        public Guid Create(VehicleExpenseDetails vehicleExpense)
        {
            Proc p = new Proc("VehicleExpenseDetails_Create");
            p["@VehicleId"] = vehicleExpense.VehicleId;
            p["@VehicleTypeId"] = vehicleExpense.VehicleTypeId;
            p["@Name"] = vehicleExpense.Name;
            p["@Description"] = vehicleExpense.Description ?? string.Empty;
            p["@ExpenseDate"] = (object?)vehicleExpense.ExpenseDate ?? DBNull.Value;
            p["@ExpenseAmount"] = vehicleExpense.ExpenseAmount ?? 0;
            p["@CompanyId"] = vehicleExpense.CompanyId ?? Guid.Empty;
            p["@SchoolId"] = vehicleExpense.SchoolId ?? Guid.Empty;
            p["@IsActive"] = vehicleExpense.IsActive;
            p["@CreatedBy"] = vehicleExpense.CreatedBy;
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

        public bool Update(VehicleExpenseDetails vehicleExpense)
        {
            Proc p = new Proc("VehicleExpenseDetails_Update");
            p["@Id"] = vehicleExpense.Id;
            p["@VehicleId"] = vehicleExpense.VehicleId;
            p["@VehicleTypeId"] = vehicleExpense.VehicleTypeId;
            p["@Name"] = vehicleExpense.Name;
            p["@Description"] = vehicleExpense.Description ?? string.Empty;
            p["@ExpenseDate"] = (object?)vehicleExpense.ExpenseDate ?? DBNull.Value;
            p["@ExpenseAmount"] = vehicleExpense.ExpenseAmount ?? 0;
            p["@CompanyId"] = vehicleExpense.CompanyId ?? Guid.Empty;
            p["@SchoolId"] = vehicleExpense.SchoolId ?? Guid.Empty;
            p["@IsActive"] = vehicleExpense.IsActive;
            p["@ModifiedBy"] = vehicleExpense.ModifiedBy ?? Guid.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("VehicleExpenseDetails_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}