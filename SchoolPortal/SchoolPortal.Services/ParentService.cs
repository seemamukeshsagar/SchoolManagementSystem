using System;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class ParentService : IParentService
    {
        private static ParentMaster Map(DataRow r)
        {
            var p = new ParentMaster();
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) p.Id = id;
            if (r.Table.Columns.Contains("StudentGUID") && Guid.TryParse(r["StudentGUID"].ToString(), out var studentGuid)) p.StudentGUID = studentGuid;
            p.ParentFirstName = r.Table.Columns.Contains("ParentFirstName") ? r["ParentFirstName"].ToString() ?? string.Empty : string.Empty;
            p.ParentLastName = r.Table.Columns.Contains("ParentLastName") ? r["ParentLastName"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("ParentDOB") && r["ParentDOB"] != DBNull.Value && DateTime.TryParse(r["ParentDOB"].ToString(), out var parentDOB)) p.ParentDOB = parentDOB;
            if (r.Table.Columns.Contains("QualificationId") && Guid.TryParse(r["QualificationId"].ToString(), out var qualificationId)) p.QualificationId = qualificationId;
            p.Occupation = r.Table.Columns.Contains("Occupation") ? r["Occupation"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("AnnualIncome") && r["AnnualIncome"] != DBNull.Value && decimal.TryParse(r["AnnualIncome"].ToString(), out var annualIncome)) p.AnnualIncome = annualIncome;
            if (r.Table.Columns.Contains("DesignationId") && Guid.TryParse(r["DesignationId"].ToString(), out var designationId)) p.DesignationId = designationId;
            p.Phone = r.Table.Columns.Contains("Phone") ? r["Phone"].ToString() ?? string.Empty : string.Empty;
            p.Mobile = r.Table.Columns.Contains("Mobile") ? r["Mobile"].ToString() ?? string.Empty : string.Empty;
            p.Email = r.Table.Columns.Contains("Email") ? r["Email"].ToString() ?? string.Empty : string.Empty;
            p.Address1 = r.Table.Columns.Contains("Address1") ? r["Address1"].ToString() ?? string.Empty : string.Empty;
            p.Address2 = r.Table.Columns.Contains("Address2") ? r["Address2"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("CityId") && Guid.TryParse(r["CityId"].ToString(), out var cityId)) p.CityId = cityId;
            if (r.Table.Columns.Contains("StateId") && Guid.TryParse(r["StateId"].ToString(), out var stateId)) p.StateId = stateId;
            if (r.Table.Columns.Contains("CountryId") && Guid.TryParse(r["CountryId"].ToString(), out var countryId)) p.CountryId = countryId;
            p.ZipCode = r.Table.Columns.Contains("ZipCode") ? r["ZipCode"].ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("RelationTypeId") && Guid.TryParse(r["RelationTypeId"].ToString(), out var relationTypeId)) p.RelationTypeId = relationTypeId;
            if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) p.SchoolId = schoolId;
            if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) p.CompanyId = companyId;
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var isActive)) p.IsActive = isActive;
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var isDeleted)) p.IsDeleted = isDeleted;
            return p;
        }

        public ParentMaster? GetByStudentId(Guid studentId)
        {
            try
            {
                Proc p = new Proc("Parent_GetByStudentId");
                p["@StudentGUID"] = studentId;
                var dt = new DataTable();
                p.Exec(dt);
                if (dt.Rows.Count == 0) return null;
                return Map(dt.Rows[0]);
            }
            catch
            {
                // If stored procedure doesn't exist, try direct query
                try
                {
                    using (var conn = ConnectionManager.DefaultConnectionManager.GetConnection())
                    {
                        var cmd = new System.Data.SqlClient.SqlCommand(
                            "SELECT TOP 1 * FROM dbo.ParentMaster WHERE StudentGUID = @StudentGUID AND IsDeleted = 0",
                            conn);
                        cmd.Parameters.AddWithValue("@StudentGUID", studentId);
                        conn.Open();
                        var adapter = new System.Data.SqlClient.SqlDataAdapter(cmd);
                        var dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                            return Map(dt.Rows[0]);
                    }
                }
                catch
                {
                    // Return null if query fails
                }
                return null;
            }
        }

        public void CreateForStudent(
            Guid studentId,
            Guid schoolId,
            Guid companyId,
            Guid createdBy,
            string? parentFirstName,
            string? parentLastName,
            DateTime? parentDOB,
            Guid? relationTypeId,
            Guid? qualificationId,
            string? occupation,
            decimal? annualIncome,
            Guid? designationId,
            string? phone,
            string? email,
            string? address1,
            string? address2,
            Guid? countryId,
            Guid? stateId,
            Guid? cityId,
            string? zipCode,
            bool isActive
        )
        {
            // Basic guards: require key fields to avoid FK violations
            if (studentId == Guid.Empty || schoolId == Guid.Empty || companyId == Guid.Empty || createdBy == Guid.Empty)
                return;
            if (string.IsNullOrWhiteSpace(parentFirstName)) return;
            if (!relationTypeId.HasValue || !qualificationId.HasValue || !designationId.HasValue) return;
            if (!countryId.HasValue || !stateId.HasValue || !cityId.HasValue) return;

            Proc p = new Proc("Parent_Create");
            p["@StudentGUID"] = studentId;
            p["@ParentFirstName"] = parentFirstName ?? string.Empty;
            p["@ParentLastName"] = parentLastName ?? string.Empty;
            p["@ParentDOB"] = (object?)parentDOB ?? DBNull.Value;
            p["@QualificationId"] = qualificationId.Value;
            p["@Occupation"] = occupation ?? string.Empty;
            p["@AnnualIncome"] = (object?)annualIncome ?? DBNull.Value;
            p["@DesignationId"] = designationId.Value;
            p["@Phone"] = phone ?? string.Empty;
            p["@Mobile"] = string.Empty;
            p["@Email"] = email ?? string.Empty;
            p["@Address1"] = address1 ?? string.Empty;
            p["@Address2"] = address2 ?? string.Empty;
            p["@CityId"] = cityId.Value;
            p["@StateId"] = stateId.Value;
            p["@CountryId"] = countryId.Value;
            p["@ZipCode"] = zipCode ?? string.Empty;
            // Default office address to same as home
            p["@OfficeAddress1"] = string.Empty;
            p["@OfficeAddress2"] = string.Empty;
            p["@OfficeCityId"] = cityId.Value;
            p["@OfficeStateId"] = stateId.Value;
            p["@OfficeCountryId"] = countryId.Value;
            p["@OfficeZipCode"] = string.Empty;
            p["@OfficePhone"] = string.Empty;
            p["@Image"] = string.Empty;
            p["@RelationTypeId"] = relationTypeId.Value;
            p["@SchoolId"] = schoolId;
            p["@CompanyId"] = companyId;
            p["@IsActive"] = isActive;
            p["@IsDeleted"] = false;
            p["@CreatedBy"] = createdBy;
            p["@CreatedDate"] = DateTime.UtcNow;
            p["@Status"] = "INC";
            p["@StatusMessage"] = "In Process....";

            var dt = new DataTable();
            p.Exec(dt);
        }
    }
}
