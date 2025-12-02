// File: SchoolPortal.Services/AcademicYearService.cs
using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class AcademicYearService : IAcademicYearService
    {
        public IEnumerable<AcademicYear> GetAll()
        {
            var list = new List<AcademicYear>();
            using (Proc p = new Proc("AcademicYear_GetAll"))
            {
                var dt = new DataTable();
                p.Exec(dt);
                foreach (DataRow r in dt.Rows)
                {
                    list.Add(MapAcademicYear(r));
                }
            }
            return list;
        }

        public IEnumerable<AcademicYear> GetAllActive()
        {
            var list = new List<AcademicYear>();
            using (Proc p = new Proc("AcademicYear_GetAllActive"))
            {
                var dt = new DataTable();
                p.Exec(dt);
                foreach (DataRow r in dt.Rows)
                {
                    list.Add(MapAcademicYear(r));
                }
            }
            return list;
        }

        public AcademicYear? GetById(Guid id)
        {
            using (Proc p = new Proc("AcademicYear_GetById"))
            {
                p["@Id"] = id;
                var dt = new DataTable();
                p.Exec(dt);
                if (dt.Rows.Count == 0) return null;
                return MapAcademicYear(dt.Rows[0]);
            }
        }

        public Guid Create(AcademicYear academicYear)
        {
            using (Proc p = new Proc("AcademicYear_Create"))
            {
                p["@Id"] = academicYear.Id != Guid.Empty ? academicYear.Id : Guid.NewGuid();
                p["@AcademicYearName"] = academicYear.AcademicYearName;
                p["@StartDate"] = academicYear.StartDate;
                p["@EndDate"] = academicYear.EndDate;
                p["@IsCurrent"] = academicYear.IsCurrent;
                p["@CreatedBy"] = academicYear.CreatedBy;
                
                var dt = new DataTable();
                p.Exec(dt);
                
                if (dt.Rows.Count > 0)
                {
                    return (Guid)dt.Rows[0]["Id"];
                }
                return Guid.Empty;
            }
        }

        public bool Update(AcademicYear academicYear)
        {
            using (Proc p = new Proc("AcademicYear_Update"))
            {
                p["@Id"] = academicYear.Id;
                p["@AcademicYearName"] = academicYear.AcademicYearName;
                p["@StartDate"] = academicYear.StartDate;
                p["@EndDate"] = academicYear.EndDate;
                p["@IsCurrent"] = academicYear.IsCurrent;
                p["@IsActive"] = academicYear.IsActive;
                p["@ModifiedBy"] = academicYear.ModifiedBy;
                
                var dt = new DataTable();
                p.Exec(dt);
                
                return dt.Rows.Count > 0;
            }
        }

        public bool Delete(Guid id)
        {
            // In a real implementation, you would pass the current user ID
            using (Proc p = new Proc("AcademicYear_Delete"))
            {
                p["@Id"] = id;
                p["@ModifiedBy"] = Guid.Empty; // Replace with actual user ID
                
                var dt = new DataTable();
                p.Exec(dt);
                
                return dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["Success"]) == 1;
            }
        }

        public bool ToggleStatus(Guid id)
        {
            // In a real implementation, you would pass the current user ID
            using (Proc p = new Proc("AcademicYear_ToggleStatus"))
            {
                p["@Id"] = id;
                p["@ModifiedBy"] = Guid.Empty; // Replace with actual user ID
                
                var dt = new DataTable();
                p.Exec(dt);
                
                return dt.Rows.Count > 0 && (bool)dt.Rows[0]["NewStatus"];
            }
        }

        private static AcademicYear MapAcademicYear(DataRow r)
        {
            if (r == null) return null;
            
            var academicYear = new AcademicYear
            {
                Id = r.Table.Columns.Contains("Id") && r["Id"] != DBNull.Value ? (Guid)r["Id"] : Guid.Empty,
                AcademicYearName = r.Table.Columns.Contains("AcademicYearName") ? r["AcademicYearName"].ToString() : string.Empty,
                StartDate = r.Table.Columns.Contains("StartDate") && r["StartDate"] != DBNull.Value ? (DateTime)r["StartDate"] : DateTime.MinValue,
                EndDate = r.Table.Columns.Contains("EndDate") && r["EndDate"] != DBNull.Value ? (DateTime)r["EndDate"] : DateTime.MinValue,
                IsCurrent = r.Table.Columns.Contains("IsCurrent") && r["IsCurrent"] != DBNull.Value && (bool)r["IsCurrent"],
                IsActive = !r.Table.Columns.Contains("IsActive") || r["IsActive"] == DBNull.Value || (bool)r["IsActive"],
                IsDeleted = r.Table.Columns.Contains("IsDeleted") && r["IsDeleted"] != DBNull.Value && (bool)r["IsDeleted"],
                CreatedBy = r.Table.Columns.Contains("CreatedBy") && r["CreatedBy"] != DBNull.Value ? (Guid)r["CreatedBy"] : Guid.Empty,
                CreatedDate = r.Table.Columns.Contains("CreatedDate") && r["CreatedDate"] != DBNull.Value ? (DateTime)r["CreatedDate"] : DateTime.UtcNow
            };

            if (r.Table.Columns.Contains("ModifiedBy") && r["ModifiedBy"] != DBNull.Value)
                academicYear.ModifiedBy = (Guid)r["ModifiedBy"];
                
            if (r.Table.Columns.Contains("ModifiedDate") && r["ModifiedDate"] != DBNull.Value)
                academicYear.ModifiedDate = (DateTime)r["ModifiedDate"];

            return academicYear;
        }
    }
}