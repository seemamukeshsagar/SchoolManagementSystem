using System;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class ParentService : IParentService
    {
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
