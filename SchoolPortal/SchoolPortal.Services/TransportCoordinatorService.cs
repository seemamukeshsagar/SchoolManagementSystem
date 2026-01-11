using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class TransportCoordinatorService : ITransportCoordinatorService
    {
        private static TransportCoordinator Map(DataRow r)
        {
            var c = new TransportCoordinator
            {
                Id = r.Field<Guid>("Id"),
                FirstName = r.Field<string>("FirstName"),
                LastName = r.Field<string>("LastName"),
                DateOfBirth = r.Table.Columns.Contains("DateOfBirth") ? r.Field<DateTime?>("DateOfBirth") : null,
                FathersName = r.Table.Columns.Contains("FathersName") ? r.Field<string>("FathersName") : string.Empty,
                MothersName = r.Table.Columns.Contains("MothersName") ? r.Field<string>("MothersName") : string.Empty,
                QualificationId = r.Table.Columns.Contains("QualificationId") ? r.Field<Guid>("QualificationId") : Guid.Empty,
                Address1 = r.Table.Columns.Contains("Address1") ? r.Field<string>("Address1") : string.Empty,
                Address2 = r.Table.Columns.Contains("Address2") ? r.Field<string>("Address2") : string.Empty,
                CityId = r.Table.Columns.Contains("CityId") ? r.Field<Guid>("CityId") : Guid.Empty,
                StateId = r.Table.Columns.Contains("StateId") ? r.Field<Guid>("StateId") : Guid.Empty,
                CountryId = r.Table.Columns.Contains("CountryId") ? r.Field<Guid>("CountryId") : Guid.Empty,
                ZipCode = r.Table.Columns.Contains("ZipCode") ? r.Field<string>("ZipCode") : string.Empty,
                MobileNumber = r.Table.Columns.Contains("MobileNumber") ? r.Field<string>("MobileNumber") : string.Empty,
                PhoneNumber = r.Table.Columns.Contains("PhoneNumber") ? r.Field<string>("PhoneNumber") : string.Empty,
                CoordinatorImage = r.Table.Columns.Contains("CoordinatorImage") ? r.Field<string>("CoordinatorImage") : string.Empty,
                EmployeeId = r.Table.Columns.Contains("EmployeeId") ? r.Field<string>("EmployeeId") : string.Empty,
                JoiningDate = r.Table.Columns.Contains("JoiningDate") ? r.Field<DateTime?>("JoiningDate") : null,
                Department = r.Table.Columns.Contains("Department") ? r.Field<string>("Department") : string.Empty,
                Designation = r.Table.Columns.Contains("Designation") ? r.Field<string>("Designation") : string.Empty,
                EmergencyContactName = r.Table.Columns.Contains("EmergencyContactName") ? r.Field<string>("EmergencyContactName") : string.Empty,
                EmergencyContactNumber = r.Table.Columns.Contains("EmergencyContactNumber") ? r.Field<string>("EmergencyContactNumber") : string.Empty,
                Email = r.Table.Columns.Contains("Email") ? r.Field<string>("Email") : string.Empty,
                CompanyId = r.Table.Columns.Contains("CompanyId") ? r.Field<Guid>("CompanyId") : Guid.Empty,
                SchoolId = r.Table.Columns.Contains("SchoolId") ? r.Field<Guid>("SchoolId") : Guid.Empty,
                IsActive = r.Table.Columns.Contains("IsActive") ? r.Field<bool>("IsActive") : true,
                IsDeleted = r.Table.Columns.Contains("IsDeleted") ? r.Field<bool>("IsDeleted") : false,
                CreatedBy = r.Table.Columns.Contains("CreatedBy") ? r.Field<Guid>("CreatedBy") : Guid.Empty,
                CreatedDate = r.Table.Columns.Contains("CreatedDate") ? r.Field<DateTime>("CreatedDate") : DateTime.UtcNow,
                ModifiedBy = r.Table.Columns.Contains("ModifiedBy") ? r.Field<Guid?>("ModifiedBy") : null,
                ModifiedDate = r.Table.Columns.Contains("ModifiedDate") ? r.Field<DateTime?>("ModifiedDate") : null,
                Status = r.Table.Columns.Contains("Status") ? r.Field<string>("Status") : string.Empty,
                StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r.Field<string>("StatusMessage") : string.Empty
            };
            return c;
        }

        public TransportCoordinator? GetByKey(Guid companyId, Guid schoolId, string firstName, string lastName)
        {
            Proc p = new Proc("TransportCoordinator_GetByKey");
            p["@CompanyId"] = companyId;
            p["@SchoolId"] = schoolId;
            p["@FirstName"] = firstName ?? string.Empty;
            p["@LastName"] = lastName ?? string.Empty;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            var r = dt.Rows[0];
            return Map(r);
        }

        public List<TransportCoordinator> GetAll()
        {
            var list = new List<TransportCoordinator>();
            Proc p = new Proc("TransportCoordinator_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(Map(r));
            }
            return list;
        }

        public TransportCoordinator? GetById(Guid id)
        {
            Proc p = new Proc("TransportCoordinator_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }
    }
}
