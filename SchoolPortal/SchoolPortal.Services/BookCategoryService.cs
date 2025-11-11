using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class BookCategoryService : IBookCategoryService
    {
        private static BookCategoryMaster MapBookCategory(DataRow r)
        {
            var c = new BookCategoryMaster();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) c.Id = id;
            c.Name = r.Table.Columns.Contains("Name") ? r["Name"].ToString() ?? string.Empty : string.Empty;
            c.Description = r.Table.Columns.Contains("Description") ? r["Description"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) c.IsActive = active;
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) c.IsDeleted = deleted;
            if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) c.CompanyId = companyId;
            if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) c.SchoolId = schoolId;
            if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) c.CreatedBy = createdBy;
            if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) c.CreatedDate = createdDate;
            if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) c.ModifiedBy = modifiedBy;
            if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) c.ModifiedDate = modifiedDate;
            c.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
            c.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
            return c;
        }

        public List<BookCategoryMaster> GetAll()
        {
            var list = new List<BookCategoryMaster>();
            Proc p = new Proc("BookCategory_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapBookCategory(r));
            }
            return list;
        }

        public BookCategoryMaster? GetById(Guid id)
        {
            Proc p = new Proc("BookCategory_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return MapBookCategory(dt.Rows[0]);
        }

        public Guid Create(BookCategoryMaster bookCategory)
        {
            Proc p = new Proc("BookCategory_Create");
            p["@Name"] = bookCategory.Name;
            p["@Description"] = bookCategory.Description ?? string.Empty;
            p["@IsActive"] = bookCategory.IsActive;
            p["@CompanyId"] = bookCategory.CompanyId;
            p["@SchoolId"] = bookCategory.SchoolId;
            p["@CreatedBy"] = bookCategory.CreatedBy;
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

        public bool Update(BookCategoryMaster bookCategory)
        {
            Proc p = new Proc("BookCategory_Update");
            p["@Id"] = bookCategory.Id;
            p["@Name"] = bookCategory.Name;
            p["@Description"] = bookCategory.Description ?? string.Empty;
            p["@IsActive"] = bookCategory.IsActive;
            p["@CompanyId"] = bookCategory.CompanyId;
            p["@SchoolId"] = bookCategory.SchoolId;
            p["@ModifiedBy"] = bookCategory.ModifiedBy ?? Guid.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("BookCategory_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public string CategoryNameById(Guid id)
        {
            Proc p = new Proc("BookCategory_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return string.Empty;
            var nameObj = dt.Rows[0]["Name"];
            return nameObj?.ToString() ?? string.Empty;
        }
    }
}