using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class VehicleTypeMasterService : IVehicleTypeMasterService
    {
        private static VehicleTypeMaster MapVehicleType(DataRow r)
        {
            var v = new VehicleTypeMaster();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) v.Id = id;
            v.VehicleType = r.Table.Columns.Contains("VehicleType") ? r["VehicleType"].ToString() ?? string.Empty : string.Empty;
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

        public List<VehicleTypeMaster> GetAll()
        {
            var list = new List<VehicleTypeMaster>();
            Proc p = new Proc("VehicleTypeMaster_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapVehicleType(r));
            }
            return list;
        }

        public List<VehicleTypeMaster> GetByCompany(Guid companyId)
        {
            var list = new List<VehicleTypeMaster>();
            Proc p = new Proc("VehicleTypeMaster_GetByCompany");
            p["@CompanyId"] = companyId;
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapVehicleType(r));
            }
            return list;
        }

        public List<VehicleTypeMaster> GetBySchool(Guid schoolId)
        {
            var list = new List<VehicleTypeMaster>();
            Proc p = new Proc("VehicleTypeMaster_GetBySchool");
            p["@SchoolId"] = schoolId;
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapVehicleType(r));
            }
            return list;
        }

        public VehicleTypeMaster? GetById(Guid id)
        {
            Proc p = new Proc("VehicleTypeMaster_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return MapVehicleType(dt.Rows[0]);
        }

        public Guid Create(VehicleTypeMaster vehicleType)
        {
            Proc p = new Proc("VehicleTypeMaster_Create");
            p["@VehicleType"] = vehicleType.VehicleType;
            p["@CompanyId"] = vehicleType.CompanyId ?? Guid.Empty;
            p["@SchoolId"] = vehicleType.SchoolId ?? Guid.Empty;
            p["@IsActive"] = vehicleType.IsActive;
            p["@CreatedBy"] = vehicleType.CreatedBy;
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

        public bool Update(VehicleTypeMaster vehicleType)
        {
            Proc p = new Proc("VehicleTypeMaster_Update");
            p["@Id"] = vehicleType.Id;
            p["@VehicleType"] = vehicleType.VehicleType;
            p["@CompanyId"] = vehicleType.CompanyId ?? Guid.Empty;
            p["@SchoolId"] = vehicleType.SchoolId ?? Guid.Empty;
            p["@IsActive"] = vehicleType.IsActive;
            p["@ModifiedBy"] = vehicleType.ModifiedBy ?? Guid.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("VehicleTypeMaster_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public string VehicleTypeNameById(Guid id)
        {
            Proc p = new Proc("VehicleTypeMaster_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return string.Empty;
            var nameObj = dt.Rows[0]["VehicleType"];
            return nameObj?.ToString() ?? string.Empty;
        }
    }
}