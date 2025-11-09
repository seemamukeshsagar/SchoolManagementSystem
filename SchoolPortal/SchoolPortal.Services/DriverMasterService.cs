using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class DriverMasterService : IDriverMasterService
    {
        private static DriverMaster Map(DataRow r)
        {
            var d = new DriverMaster
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
                DriverImage = r.Table.Columns.Contains("DriverImage") ? r.Field<string>("DriverImage") : string.Empty,
                LicenceNumber = r.Table.Columns.Contains("LicenceNumber") ? r.Field<string>("LicenceNumber") : string.Empty,
                LicenceIssueDate = r.Table.Columns.Contains("LicenceIssueDate") ? r.Field<DateTime?>("LicenceIssueDate") : null,
                LicenceValidUptoDate = r.Table.Columns.Contains("LicenceValidUptoDate") ? r.Field<DateTime?>("LicenceValidUptoDate") : null,
                LicenceDescription = r.Table.Columns.Contains("LicenceDescription") ? r.Field<string>("LicenceDescription") : string.Empty,
                LicenceImage = r.Table.Columns.Contains("LicenceImage") ? r.Field<string>("LicenceImage") : string.Empty,
                LicenceType = r.Table.Columns.Contains("LicenceType") ? r.Field<string>("LicenceType") : string.Empty,
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
            return d;
        }

        public Guid Create(DriverMaster driver)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));

            Proc p = new Proc("DriverMaster_Create");
            p["@FirstName"] = driver.FirstName ?? string.Empty;
            p["@LastName"] = driver.LastName ?? string.Empty;
            p["@DateOfBirth"] = (object?)driver.DateOfBirth ?? DBNull.Value;
            p["@FathersName"] = driver.FathersName ?? string.Empty;
            p["@MothersName"] = driver.MothersName ?? string.Empty;
            p["@QualificationId"] = driver.QualificationId == Guid.Empty ? (object)DBNull.Value : driver.QualificationId;
            p["@Address1"] = driver.Address1 ?? string.Empty;
            p["@Address2"] = driver.Address2 ?? string.Empty;
            p["@CityId"] = driver.CityId == Guid.Empty ? (object)DBNull.Value : driver.CityId;
            p["@StateId"] = driver.StateId == Guid.Empty ? (object)DBNull.Value : driver.StateId;
            p["@CountryId"] = driver.CountryId == Guid.Empty ? (object)DBNull.Value : driver.CountryId;
            p["@ZipCode"] = driver.ZipCode ?? string.Empty;
            p["@MobileNumber"] = driver.MobileNumber ?? string.Empty;
            p["@PhoneNumber"] = driver.PhoneNumber ?? string.Empty;
            p["@DriverImage"] = driver.DriverImage ?? string.Empty;
            p["@LicenceNumber"] = driver.LicenceNumber ?? string.Empty;
            p["@LicenceIssueDate"] = (object?)driver.LicenceIssueDate ?? DBNull.Value;
            p["@LicenceValidUptoDate"] = (object?)driver.LicenceValidUptoDate ?? DBNull.Value;
            p["@LicenceDescription"] = driver.LicenceDescription ?? string.Empty;
            p["@LicenceImage"] = driver.LicenceImage ?? string.Empty;
            p["@LicenceType"] = driver.LicenceType ?? string.Empty;
            p["@CompanyId"] = driver.CompanyId;
            p["@SchoolId"] = driver.SchoolId;
            p["@IsActive"] = driver.IsActive;
            p["@IsDeleted"] = driver.IsDeleted;
            p["@CreatedBy"] = driver.CreatedBy;
            p["@CreatedDate"] = driver.CreatedDate;
            p["@Status"] = driver.Status ?? string.Empty;
            p["@StatusMessage"] = driver.StatusMessage ?? string.Empty;

            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count > 0)
            {
                var idObj = dt.Rows[0]["Id"];
                if (idObj != null && Guid.TryParse(idObj.ToString(), out var newId))
                {
                    return newId;
                }
            }
            return Guid.Empty;
        }

        public DriverMaster? GetByKey(Guid companyId, Guid schoolId, string firstName, string lastName)
        {
            Proc p = new Proc("DriverMaster_GetByKey");
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

        public List<DriverMaster> GetAll()
        {
            var list = new List<DriverMaster>();
            Proc p = new Proc("DriverMaster_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(Map(r));
            }
            return list;
        }

        public DriverMaster? GetById(Guid id)
        {
            Proc p = new Proc("DriverMaster_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public bool Update(DriverMaster driver)
        {
            Proc p = new Proc("DriverMaster_Update");
            p["@Id"] = driver.Id;
            p["@FirstName"] = driver.FirstName ?? string.Empty;
            p["@LastName"] = driver.LastName ?? string.Empty;
            p["@DateOfBirth"] = (object?)driver.DateOfBirth ?? DBNull.Value;
            p["@FathersName"] = driver.FathersName ?? string.Empty;
            p["@MothersName"] = driver.MothersName ?? string.Empty;
            p["@QualificationId"] = driver.QualificationId == Guid.Empty ? (object)DBNull.Value : driver.QualificationId;
            p["@Address1"] = driver.Address1 ?? string.Empty;
            p["@Address2"] = driver.Address2 ?? string.Empty;
            p["@CityId"] = driver.CityId == Guid.Empty ? (object)DBNull.Value : driver.CityId;
            p["@StateId"] = driver.StateId == Guid.Empty ? (object)DBNull.Value : driver.StateId;
            p["@CountryId"] = driver.CountryId == Guid.Empty ? (object)DBNull.Value : driver.CountryId;
            p["@ZipCode"] = driver.ZipCode ?? string.Empty;
            p["@MobileNumber"] = driver.MobileNumber ?? string.Empty;
            p["@PhoneNumber"] = driver.PhoneNumber ?? string.Empty;
            p["@DriverImage"] = driver.DriverImage ?? string.Empty;
            p["@LicenceNumber"] = driver.LicenceNumber ?? string.Empty;
            p["@LicenceIssueDate"] = (object?)driver.LicenceIssueDate ?? DBNull.Value;
            p["@LicenceValidUptoDate"] = (object?)driver.LicenceValidUptoDate ?? DBNull.Value;
            p["@LicenceDescription"] = driver.LicenceDescription ?? string.Empty;
            p["@LicenceImage"] = driver.LicenceImage ?? string.Empty;
            p["@LicenceType"] = driver.LicenceType ?? string.Empty;
            p["@IsActive"] = driver.IsActive;
            p["@IsDeleted"] = driver.IsDeleted;
            p["@ModifiedBy"] = driver.ModifiedBy ?? Guid.Empty;
            p["@ModifiedDate"] = driver.ModifiedDate ?? DateTime.UtcNow;
            p["@Status"] = driver.Status ?? string.Empty;
            p["@StatusMessage"] = driver.StatusMessage ?? string.Empty;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }

        public bool Delete(Guid id)
        {
            Proc p = new Proc("DriverMaster_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}
