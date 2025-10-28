using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using Schoolortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class DeptDesigDetailsService : IDeptDesigDetailsService
    {
        private static DeptDesigDetails MapDeptDesigDetails(DataRow r)
        {
            var d = new DeptDesigDetails();
            if (r.Table.Columns.Contains("Id") && r["Id"] != DBNull.Value && Guid.TryParse(r["Id"].ToString(), out var id)) 
                d.Id = id;
            
            if (r.Table.Columns.Contains("DepartmentId") && r["DepartmentId"] != DBNull.Value && Guid.TryParse(r["DepartmentId"].ToString(), out var deptId)) 
                d.DepartmentId = deptId;
            
            if (r.Table.Columns.Contains("DesignationId") && r["DesignationId"] != DBNull.Value && Guid.TryParse(r["DesignationId"].ToString(), out var desigId)) 
                d.DesignationId = desigId;
            
            if (r.Table.Columns.Contains("CompanyId") && r["CompanyId"] != DBNull.Value && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) 
                d.CompanyId = companyId;
            
            if (r.Table.Columns.Contains("SchoolId") && r["SchoolId"] != DBNull.Value && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) 
                d.SchoolId = schoolId;
            
            if (r.Table.Columns.Contains("IsActive") && r["IsActive"] != DBNull.Value && bool.TryParse(r["IsActive"].ToString(), out var isActive)) 
                d.IsActive = isActive;
            
            if (r.Table.Columns.Contains("IsDeleted") && r["IsDeleted"] != DBNull.Value && bool.TryParse(r["IsDeleted"].ToString(), out var isDeleted)) 
                d.IsDeleted = isDeleted;
            
            if (r.Table.Columns.Contains("CreatedBy") && r["CreatedBy"] != DBNull.Value && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) 
                d.CreatedBy = createdBy;
            
            if (r.Table.Columns.Contains("CreatedDate") && r["CreatedDate"] != DBNull.Value && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) 
                d.CreatedDate = createdDate;
            
            if (r.Table.Columns.Contains("ModifiedBy") && r["ModifiedBy"] != DBNull.Value && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy))
                d.ModifiedBy = modifiedBy;
            
            if (r.Table.Columns.Contains("ModifiedDate") && r["ModifiedDate"] != DBNull.Value && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate))
                d.ModifiedDate = modifiedDate;
            
            d.Status = r.Table.Columns.Contains("Status") && r["Status"] != DBNull.Value ? r["Status"].ToString() ?? string.Empty : string.Empty;
            d.StatusMessage = r.Table.Columns.Contains("StatusMessage") && r["StatusMessage"] != DBNull.Value ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
            
            return d;
        }

        public List<DeptDesigDetails> GetAll()
        {
            var list = new List<DeptDesigDetails>();
            Proc p = new Proc("DeptDesigDetails_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapDeptDesigDetails(r));
            }
            return list;
        }

        public DeptDesigDetails? GetById(Guid id)
        {
            Proc p = new Proc("DeptDesigDetails_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return MapDeptDesigDetails(dt.Rows[0]);
        }

        public Guid Create(DeptDesigDetails deptDesigDetails)
        {
            Proc p = new Proc("DeptDesigDetails_Create");
            p["@DepartmentId"] = deptDesigDetails.DepartmentId;
            p["@DesignationId"] = deptDesigDetails.DesignationId;
            p["@CompanyId"] = deptDesigDetails.CompanyId;
            p["@SchoolId"] = deptDesigDetails.SchoolId;
            p["@IsActive"] = deptDesigDetails.IsActive;
            p["@CreatedBy"] = deptDesigDetails.CreatedBy;
            p["@Status"] = deptDesigDetails.Status ?? string.Empty;
            p["@StatusMessage"] = deptDesigDetails.StatusMessage ?? string.Empty;

            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count > 0 && dt.Rows[0]["Id"] != DBNull.Value && dt.Rows[0]["Id"] != null)
            {
                return Guid.TryParse(dt.Rows[0]["Id"].ToString(), out var id) ? id : Guid.Empty;
            }
            return Guid.Empty;
        }

        public bool Update(DeptDesigDetails deptDesigDetails)
        {
            Proc p = new Proc("DeptDesigDetails_Update");
            p["@Id"] = deptDesigDetails.Id;
            p["@DepartmentId"] = deptDesigDetails.DepartmentId;
            p["@DesignationId"] = deptDesigDetails.DesignationId;
            p["@CompanyId"] = deptDesigDetails.CompanyId;
            p["@SchoolId"] = deptDesigDetails.SchoolId;
            p["@IsActive"] = deptDesigDetails.IsActive;
            p["@ModifiedBy"] = deptDesigDetails.ModifiedBy ?? (object)DBNull.Value;
            p["@Status"] = deptDesigDetails.Status ?? string.Empty;
            p["@StatusMessage"] = deptDesigDetails.StatusMessage ?? string.Empty;

            p.Exec();
            var returnValueParam = p.Parameters.Contains("@RETURN_VALUE") ? p.Parameters["@RETURN_VALUE"] : null;
            if (returnValueParam != null && returnValueParam.Value != null)
            {
                int code = Convert.ToInt32(returnValueParam.Value);
                return code == 1;
            }
            return false;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("DeptDesigDetails_Delete");
            p["@Id"] = id;
            p.Exec();
            var returnValueParam = p.Parameters.Contains("@RETURN_VALUE") ? p.Parameters["@RETURN_VALUE"] : null;
            if (returnValueParam != null && returnValueParam.Value != null)
            {
                int code = Convert.ToInt32(returnValueParam.Value);
                return code == 1;
            }
            return false;
        }
    }
}