using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class VehicleMasterService : IVehicleMasterService
    {
        private static VehicleMaster MapVehicle(DataRow r)
        {
            var v = new VehicleMaster();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) v.Id = id;
            v.VehicleNumber = r.Table.Columns.Contains("VehicleNumber") ? r["VehicleNumber"].ToString() ?? string.Empty : string.Empty;
            v.VehicleModel = r.Table.Columns.Contains("VehicleModel") ? r["VehicleModel"].ToString() ?? string.Empty : string.Empty;
            v.VehicleMake = r.Table.Columns.Contains("VehicleMake") ? r["VehicleMake"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("VehicleTypeId") && Guid.TryParse(r["VehicleTypeId"].ToString(), out var vehicleType)) v.VehicleTypeId = vehicleType;
            v.RegistrationNumber = r.Table.Columns.Contains("RegistrationNumber") ? r["RegistrationNumber"].ToString() ?? string.Empty : string.Empty;
            v.InsuranceCompany = r.Table.Columns.Contains("InsuranceCompany") ? r["InsuranceCompany"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("InsurancePremium") && decimal.TryParse(r["InsurancePremium"].ToString(), out var insurancePremium)) v.InsurancePremium = insurancePremium;
            if (r.Table.Columns.Contains("SeatingCapacity") && int.TryParse(r["SeatingCapacity"].ToString(), out var seatingCapacity)) v.SeatingCapacity = seatingCapacity;
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

        public List<VehicleMaster> GetAll()
        {
            var list = new List<VehicleMaster>();
            Proc p = new Proc("VehicleMaster_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapVehicle(r));
            }
            return list;
        }

        public VehicleMaster? GetById(Guid id)
        {
            Proc p = new Proc("VehicleMaster_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return MapVehicle(dt.Rows[0]);
        }

        public Guid Create(VehicleMaster vehicle)
        {
            Proc p = new Proc("VehicleMaster_Create");
            p["@VehicleNumber"] = vehicle.VehicleNumber;
            p["@VehicleModel"] = vehicle.VehicleModel ?? string.Empty;
            p["@VehicleMake"] = vehicle.VehicleMake ?? string.Empty;
            p["@VehicleTypeId"] = vehicle.VehicleTypeId;
            p["@RegistrationNumber"] = vehicle.RegistrationNumber ?? string.Empty;
            p["@InsuranceCompany"] = vehicle.InsuranceCompany ?? string.Empty;
            p["@InsurancePremium"] = vehicle.InsurancePremium ?? 0;
            p["@SeatingCapacity"] = vehicle.SeatingCapacity ?? 0;
            p["@CompanyId"] = vehicle.CompanyId ?? Guid.Empty;
            p["@SchoolId"] = vehicle.SchoolId ?? Guid.Empty;
            p["@IsActive"] = vehicle.IsActive;
            p["@CreatedBy"] = vehicle.CreatedBy;
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

        public bool Update(VehicleMaster vehicle)
        {
            Proc p = new Proc("VehicleMaster_Update");
            p["@Id"] = vehicle.Id;
            p["@VehicleNumber"] = vehicle.VehicleNumber;
            p["@VehicleModel"] = vehicle.VehicleModel ?? string.Empty;
            p["@VehicleMake"] = vehicle.VehicleMake ?? string.Empty;
            p["@VehicleTypeId"] = vehicle.VehicleTypeId;
            p["@RegistrationNumber"] = vehicle.RegistrationNumber ?? string.Empty;
            p["@InsuranceCompany"] = vehicle.InsuranceCompany ?? string.Empty;
            p["@InsurancePremium"] = vehicle.InsurancePremium ?? 0;
            p["@SeatingCapacity"] = vehicle.SeatingCapacity ?? 0;
            p["@CompanyId"] = vehicle.CompanyId ?? Guid.Empty;
            p["@SchoolId"] = vehicle.SchoolId ?? Guid.Empty;
            p["@IsActive"] = vehicle.IsActive;
            p["@ModifiedBy"] = vehicle.ModifiedBy ?? Guid.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("VehicleMaster_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public string VehicleNumberById(Guid id)
        {
            Proc p = new Proc("VehicleMaster_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return string.Empty;
            var nameObj = dt.Rows[0]["VehicleNumber"];
            return nameObj?.ToString() ?? string.Empty;
        }
    }
}