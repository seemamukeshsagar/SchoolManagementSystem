using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Logging;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using System.Threading.Tasks;

namespace SchoolPortal.Services
{
    public class ClassService : IClassService
    {
        private readonly ILogger<ClassService> _logger;

        public ClassService(ILogger<ClassService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<ClassMaster> GetAllActive()
        {
            try
            {
                var list = new List<ClassMaster>();
                var dt = new DataTable();
                
                using (var p = new Proc("ClassMaster_GetAll"))
                {
                    p.Exec(dt);
                    
                    foreach (DataRow row in dt.Rows)
                    {
                        var item = Map(row);
                        if (item.IsActive)
                        {
                            list.Add(item);
                        }
                    }
                }
                
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active classes");
                return new List<ClassMaster>();
            }
        }

        private static ClassMaster Map(DataRow r)
        {
            if (r == null) throw new ArgumentNullException(nameof(r));
            if (r.Table == null) throw new ArgumentException("DataRow must have a valid Table", nameof(r));

            var c = new ClassMaster();
            if (r.Table.Columns.Contains("Id") && r["Id"] != DBNull.Value && Guid.TryParse(r["Id"]?.ToString(), out var id)) 
                c.Id = id;
                
            c.Name = r.Table.Columns.Contains("Name") ? r["Name"]?.ToString() ?? string.Empty : string.Empty;
            c.ExamAssessment = r.Table.Columns.Contains("ExamAssessment") ? r["ExamAssessment"]?.ToString() ?? string.Empty : string.Empty;
            
            if (r.Table.Columns.Contains("IsGradePointApplicable") && r["IsGradePointApplicable"] != DBNull.Value && bool.TryParse(r["IsGradePointApplicable"]?.ToString(), out var gpa)) 
                c.IsGradePointApplicable = gpa;
                
            if (r.Table.Columns.Contains("IsActive") && r["IsActive"] != DBNull.Value && bool.TryParse(r["IsActive"]?.ToString(), out var active)) 
                c.IsActive = active;
                
            if (r.Table.Columns.Contains("IsDeleted") && r["IsDeleted"] != DBNull.Value && bool.TryParse(r["IsDeleted"]?.ToString(), out var deleted)) 
                c.IsDeleted = deleted;
                
            if (r.Table.Columns.Contains("CompanyId") && r["CompanyId"] != DBNull.Value && Guid.TryParse(r["CompanyId"]?.ToString(), out var companyId)) 
                c.CompanyId = companyId;
                
            if (r.Table.Columns.Contains("SchoolId") && r["SchoolId"] != DBNull.Value && Guid.TryParse(r["SchoolId"]?.ToString(), out var schoolId)) 
                c.SchoolId = schoolId;
                
            if (r.Table.Columns.Contains("CreatedBy") && r["CreatedBy"] != DBNull.Value && Guid.TryParse(r["CreatedBy"]?.ToString(), out var createdBy)) 
                c.CreatedBy = createdBy;
                
            if (r.Table.Columns.Contains("CreatedDate") && r["CreatedDate"] != DBNull.Value && DateTime.TryParse(r["CreatedDate"]?.ToString(), out var createdDate)) 
                c.CreatedDate = createdDate;
                
            if (r.Table.Columns.Contains("ModifiedBy") && r["ModifiedBy"] != DBNull.Value && Guid.TryParse(r["ModifiedBy"]?.ToString(), out var modifiedBy)) 
                c.ModifiedBy = modifiedBy;
                
            if (r.Table.Columns.Contains("ModifiedDate") && r["ModifiedDate"] != DBNull.Value && DateTime.TryParse(r["ModifiedDate"]?.ToString(), out var modifiedDate)) 
                c.ModifiedDate = modifiedDate;
                
            if (r.Table.Columns.Contains("OrderBy") && r["OrderBy"] != DBNull.Value && int.TryParse(r["OrderBy"]?.ToString(), out var orderBy)) 
                c.OrderBy = orderBy;
                
            c.Status = r.Table.Columns.Contains("Status") ? r["Status"]?.ToString() ?? string.Empty : string.Empty;
            c.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"]?.ToString() ?? string.Empty : string.Empty;
            
            return c;
        }

        // Helper methods to map DataTable to model classes
        private List<ClassMaster> MapToClassMasterList(DataTable dt)
        {
            var list = new List<ClassMaster>();
            foreach (DataRow row in dt.Rows)
            {
                var item = new ClassMaster
                {
                    Id = row["Id"] != DBNull.Value ? (Guid)row["Id"] : Guid.Empty,
                    Name = row["Name"]?.ToString() ?? string.Empty,
                    ExamAssessment = row["ExamAssessment"]?.ToString() ?? string.Empty,
                    IsGradePointApplicable = row["IsGradePointApplicable"] != DBNull.Value ? (bool)row["IsGradePointApplicable"] : false,
                    IsActive = row["IsActive"] != DBNull.Value ? (bool)row["IsActive"] : false,
                    IsDeleted = row["IsDeleted"] != DBNull.Value ? (bool)row["IsDeleted"] : false,
                    CompanyId = row["CompanyId"] != DBNull.Value ? (Guid)row["CompanyId"] : Guid.Empty,
                    SchoolId = row["SchoolId"] != DBNull.Value ? (Guid)row["SchoolId"] : Guid.Empty,
                    CreatedBy = row["CreatedBy"] != DBNull.Value ? (Guid)row["CreatedBy"] : Guid.Empty,
                    CreatedDate = row["CreatedDate"] != DBNull.Value ? (DateTime)row["CreatedDate"] : DateTime.MinValue,
                    ModifiedBy = row["ModifiedBy"] != DBNull.Value ? (Guid)row["ModifiedBy"] : Guid.Empty,
                    ModifiedDate = row["ModifiedDate"] != DBNull.Value ? (DateTime)row["ModifiedDate"] : DateTime.MinValue,
                    OrderBy = row["OrderBy"] != DBNull.Value ? (int)row["OrderBy"] : 0,
                    Status = row["Status"]?.ToString() ?? string.Empty,
                    StatusMessage = row["StatusMessage"]?.ToString() ?? string.Empty
                };
                list.Add(item);
            }
            return list;
        }

        public List<ClassMaster> GetAll()
        {
            var list = new List<ClassMaster>();
            using (var p = new Proc("Class_GetAll"))
            {
                var dt = new DataTable();
                p.Exec(dt);
                foreach (DataRow r in dt.Rows)
                {
                    if (r != null)
                    {
                        list.Add(Map(r));
                    }
                }
            }
            return list;
        }

        public async Task<List<ClassMaster>> GetAllAsync()
        {
            try
            {
                return await Task.Run(() =>
                {
                    var list = new List<ClassMaster>();
                    using (var p = new Proc("Class_GetAll"))
                    {
                        var dt = new DataTable();
                        p.Exec(dt);
                        foreach (DataRow r in dt.Rows)
                        {
                            if (r != null)
                            {
                                list.Add(Map(r));
                            }
                        }
                    }
                    return list;
                }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error in GetAllAsync");
                throw new ClassServiceException("Error retrieving classes", ex);
            }
        }

        public async Task<List<ClassMaster>> GetAllAsync(Guid? schoolId)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var list = new List<ClassMaster>();
                    using (var p = new Proc("Class_GetAll"))
                    {
                        p["@SchoolId"] = schoolId;
                        var dt = new DataTable();
                        p.Exec(dt);
                        foreach (DataRow r in dt.Rows)
                        {
                            if (r != null)
                            {
                                list.Add(Map(r));
                            }
                        }
                    }
                    return list;
                }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error in GetAllAsync with schoolId");
                throw new ClassServiceException("Error retrieving classes by school", ex);
            }
        }

        public List<ClassMaster> GetAll(Guid? schoolId)
        {
            var list = new List<ClassMaster>();
            using (var p = new Proc("Class_GetAll"))
            {
                p["@SchoolId"] = schoolId;
                var dt = new DataTable();
                p.Exec(dt);
                foreach (DataRow r in dt.Rows)
                {
                    if (r != null)
                    {
                        list.Add(Map(r));
                    }
                }
            }
            return list;
        }

        public ClassMaster? GetById(Guid id)
        {
            using (var p = new Proc("Class_GetById"))
            {
                p["@Id"] = id;
                var dt = new DataTable();
                p.Exec(dt);
                if (dt.Rows.Count == 0 || dt.Rows[0] == null) 
                    return null;
                return Map(dt.Rows[0]);
            }
        }

        public async Task<ClassMaster?> GetByIdAsync(Guid id)
        {
            using (var p = new Proc("Class_GetById"))
            {
                p["@Id"] = id;
                var dt = new DataTable();
                await Task.Run(() => p.Exec(dt)).ConfigureAwait(false);

                if (dt.Rows.Count > 0 && dt.Rows[0] != null)
                {
                    return Map(dt.Rows[0]);
                }
                return null;
            }
        }

        public Guid Create(ClassMaster cls)
        {
            if (cls == null)
                throw new ArgumentNullException(nameof(cls));

            using (var p = new Proc("Class_Create"))
            {
                p["@Name"] = cls.Name ?? throw new ArgumentNullException(nameof(cls.Name));
                p["@ExamAssessment"] = cls.ExamAssessment ?? string.Empty;
                p["@IsGradePointApplicable"] = cls.IsGradePointApplicable ?? false;
                p["@IsDeleted"] = false;
                p["@CompanyId"] = cls.CompanyId;
                p["@SchoolId"] = cls.SchoolId;
                p["@CreatedBy"] = cls.CreatedBy;
                p["@CreatedDate"] = DateTime.UtcNow;
                p["@OrderBy"] = cls.OrderBy;

                var result = p.ExecScalar();
                return result != null && result != DBNull.Value ? (Guid)result : Guid.Empty;
            }
        }

        public bool Update(ClassMaster cls)
        {
            if (cls == null)
                throw new ArgumentNullException(nameof(cls));

            using (var p = new Proc("Class_Update"))
            {
                p["@Id"] = cls.Id;
                p["@Name"] = cls.Name ?? throw new ArgumentNullException(nameof(cls.Name));
                p["@ExamAssessment"] = cls.ExamAssessment ?? string.Empty;
                p["@IsGradePointApplicable"] = cls.IsGradePointApplicable ?? false;
                p["@IsActive"] = cls.IsActive;
                p["@SchoolId"] = cls.SchoolId;
                p["@ModifiedBy"] = cls.ModifiedBy;
                p["@ModifiedDate"] = DateTime.UtcNow;
                p["@OrderBy"] = cls.OrderBy;

                var result = p.ExecNonQuery();
                return result > 0;
            }
        }

        public bool Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty", nameof(id));

            using (var p = new Proc("Class_Delete"))
            {
                p["@Id"] = id;
                var result = p.ExecNonQuery();
                return result > 0;
            }
        }

        public string ClassNameById(Guid id)
        {
            if (id == Guid.Empty)
                return string.Empty;

            using (var p = new Proc("Class_GetNameById"))
            {
                p["@Id"] = id;
                var result = p.ExecScalar();
                return result?.ToString() ?? string.Empty;
            }
        }

        public async Task<IEnumerable<ClassMaster>> GetAllActiveAsync()
        {
            try
            {
                return await Task.Run(() => GetAllActive().ToList()).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllActiveAsync");
                return new List<ClassMaster>();
            }
        }
    }

    public class ClassServiceException : Exception
    {
        public ClassServiceException() { }
        public ClassServiceException(string message) : base(message) { }
        public ClassServiceException(string message, Exception inner) : base(message, inner) { }
    }
}
