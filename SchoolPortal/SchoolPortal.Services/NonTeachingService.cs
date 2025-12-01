using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using System.Linq;
using System.Data.Common;

namespace SchoolPortal.Services.Services
{
    public class NonTeachingService : INonTeachingService
    {
        private readonly ILogger<NonTeachingService> _logger;
        private readonly IDbConnection _connection;
        private readonly INonTeachingDocumentDetailsService _documentService;
        private readonly INonTeachingQualificationDetailsService _qualificationService;

        public NonTeachingService(
            ILogger<NonTeachingService> logger,
            IDbConnection connection,
            INonTeachingDocumentDetailsService documentService,
            INonTeachingQualificationDetailsService qualificationService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _documentService = documentService ?? throw new ArgumentNullException(nameof(documentService));
            _qualificationService = qualificationService ?? throw new ArgumentNullException(nameof(qualificationService));
        }

        // Implement the interface methods
        public IEnumerable<NonTeachingMaster> GetAll()
        {
            try
            {
                var nonTeachingList = new List<NonTeachingMaster>();
                using (var proc = new Proc("sp_NonTeaching_GetAll"))
                {
                    var dt = new DataTable();
                    proc.Exec(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        nonTeachingList.Add(MapToNonTeaching(row));
                    }
                }
                return nonTeachingList;
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "Database error in NonTeachingService.GetAll");
                throw new ApplicationException("An error occurred while retrieving non-teaching staff.", sqlEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in NonTeachingService.GetAll");
                throw new ApplicationException("An unexpected error occurred.", ex);
            }
        }

        public NonTeachingMaster? GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID cannot be empty", nameof(id));

            try
            {
                Proc p = new Proc("sp_NonTeaching_GetById");
                p["@Id"] = id;
                var dt = new DataTable();
                p.Exec(dt);
                if (dt.Rows.Count == 0) return null;
                return MapToNonTeaching(dt.Rows[0]);
            }
            catch
            {
                return null;
            }
        }

        public int Add(NonTeachingMaster entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            ValidateNonTeachingEntity(entity);

            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    Proc proc = new Proc("sp_NonTeaching_Insert");
                    
                        // Add parameters
                        // Inside the Add method, in the using block where you create the Proc object:
                        proc["@Id"] = entity.Id;
                        proc["@FirstName"] = entity.FirstName;
                        proc["@MiddleName"] = entity.MiddleName ?? (object)DBNull.Value;
                        proc["@LastName"] = entity.LastName;
                        proc["@DOB"] = entity.DOB;
                        proc["@DOJ"] = entity.DOJ;
                        proc["@DateOfLeaving"] = entity.DateOfLeaving ?? (object)DBNull.Value;
                        proc["@Address"] = entity.Address ?? (object)DBNull.Value;
                        proc["@CityId"] = entity.CityId ?? (object)DBNull.Value;
                        proc["@StateId"] = entity.StateId ?? (object)DBNull.Value;
                        proc["@CountryId"] = entity.CountryId ?? (object)DBNull.Value;
                        proc["@ZipCode"] = entity.ZipCode ?? (object)DBNull.Value;
                        proc["@Gender"] = entity.Gender ?? (object)DBNull.Value;
                        proc["@MaritalStatusId"] = entity.MaritalStatusId ?? (object)DBNull.Value;
                        proc["@Image"] = entity.Image ?? (object)DBNull.Value;
                        proc["@Phone"] = entity.Phone ?? (object)DBNull.Value;
                        proc["@MobilePhone"] = entity.MobilePhone ?? (object)DBNull.Value;
                        proc["@Email"] = entity.Email ?? (object)DBNull.Value;
                        proc["@EmployeeCode"] = entity.EmployeeCode ?? (object)DBNull.Value;
                        proc["@Designation"] = entity.Designation ?? (object)DBNull.Value;
                        proc["@Department"] = entity.Department ?? (object)DBNull.Value;
                        proc["@Qualification"] = entity.Qualification ?? (object)DBNull.Value;
                        proc["@Salary"] = entity.Salary ?? (object)DBNull.Value;
                        proc["@BankAccountNumber"] = entity.BankAccountNumber ?? (object)DBNull.Value;
                        proc["@BankName"] = entity.BankName ?? (object)DBNull.Value;
                        proc["@IFSCCode"] = entity.IFSCCode ?? (object)DBNull.Value;
                        proc["@PAN"] = entity.PAN ?? (object)DBNull.Value;
                        proc["@AadharNumber"] = entity.AadharNumber ?? (object)DBNull.Value;
                        proc["@EmergencyContactName"] = entity.EmergencyContactName ?? (object)DBNull.Value;
                        proc["@EmergencyContactNumber"] = entity.EmergencyContactNumber ?? (object)DBNull.Value;
                        proc["@EmergencyContactRelation"] = entity.EmergencyContactRelation ?? (object)DBNull.Value;
                        proc["@CompanyId"] = entity.CompanyId;
                        proc["@SchoolId"] = entity.SchoolId;
                        proc["@IsActive"] = entity.IsActive;
                        proc["@CreatedBy"] = entity.CreatedBy;

                    var dt = new DataTable();
                    proc.Exec(dt);
                    return dt.Rows.Count;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool Update(NonTeachingMaster entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            ValidateNonTeachingEntity(entity);

            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    Proc proc = new Proc("sp_NonTeaching_Update");
                    
                        proc["@Id"] = entity.Id;
                        proc["@FirstName"] = entity.FirstName;
                        proc["@MiddleName"] = entity.MiddleName ?? (object)DBNull.Value;
                        proc["@LastName"] = entity.LastName;
                        proc["@DOB"] = entity.DOB;
                        proc["@DOJ"] = entity.DOJ;
                        proc["@DateOfLeaving"] = entity.DateOfLeaving ?? (object)DBNull.Value;
                        proc["@Address"] = entity.Address ?? (object)DBNull.Value;
                        proc["@CityId"] = entity.CityId ?? (object)DBNull.Value;
                        proc["@StateId"] = entity.StateId ?? (object)DBNull.Value;
                        proc["@CountryId"] = entity.CountryId ?? (object)DBNull.Value;
                        proc["@ZipCode"] = entity.ZipCode ?? (object)DBNull.Value;
                        proc["@Gender"] = entity.Gender ?? (object)DBNull.Value;
                        proc["@MaritalStatusId"] = entity.MaritalStatusId ?? (object)DBNull.Value;
                        proc["@Image"] = entity.Image ?? (object)DBNull.Value;
                        proc["@Phone"] = entity.Phone ?? (object)DBNull.Value;
                        proc["@MobilePhone"] = entity.MobilePhone ?? (object)DBNull.Value;
                        proc["@Email"] = entity.Email ?? (object)DBNull.Value;
                        proc["@EmployeeCode"] = entity.EmployeeCode ?? (object)DBNull.Value;
                        proc["@Designation"] = entity.Designation ?? (object)DBNull.Value;
                        proc["@Department"] = entity.Department ?? (object)DBNull.Value;
                        proc["@Qualification"] = entity.Qualification ?? (object)DBNull.Value;
                        proc["@Salary"] = entity.Salary ?? (object)DBNull.Value;
                        proc["@BankAccountNumber"] = entity.BankAccountNumber ?? (object)DBNull.Value;
                        proc["@BankName"] = entity.BankName ?? (object)DBNull.Value;
                        proc["@IFSCCode"] = entity.IFSCCode ?? (object)DBNull.Value;
                        proc["@PAN"] = entity.PAN ?? (object)DBNull.Value;
                        proc["@AadharNumber"] = entity.AadharNumber ?? (object)DBNull.Value;
                        proc["@EmergencyContactName"] = entity.EmergencyContactName ?? (object)DBNull.Value;
                        proc["@EmergencyContactNumber"] = entity.EmergencyContactNumber ?? (object)DBNull.Value;
                        proc["@EmergencyContactRelation"] = entity.EmergencyContactRelation ?? (object)DBNull.Value;
                        proc["@IsActive"] = entity.IsActive;
                        proc["@ModifiedBy"] = entity.ModifiedBy == Guid.Empty ? (object)DBNull.Value : entity.ModifiedBy;

                    var dt = new DataTable();
                    proc.Exec(dt);
                    return dt.Rows.Count > 0;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool Delete(Guid id, Guid? currentUserId)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID cannot be empty", nameof(id));

            try
            {
                var entity = GetById(id);
                if (entity == null)
                    return false;

                entity.IsDeleted = true;
                entity.ModifiedBy = currentUserId ?? throw new UnauthorizedAccessException("User not authenticated");
                entity.ModifiedDate = DateTime.UtcNow;

                return Update(entity);
            }
            catch (Exception ex) when (ex is not ArgumentException)
            {
                _logger.LogError(ex, "Error deleting non-teaching staff with ID {Id}", id);
                throw new ApplicationException($"An error occurred while deleting non-teaching staff with ID {id}", ex);
            }
        }

        private NonTeachingMaster MapToNonTeaching(DataRow r)
        {
            if (r == null) throw new ArgumentNullException(nameof(r));
            if (r.Table == null) throw new ArgumentException("DataRow.Table cannot be null", nameof(r));

            var t = new NonTeachingMaster
            {
                Id = r.Table.Columns.Contains("Id") && r["Id"] != DBNull.Value ? (Guid)r["Id"] : Guid.Empty,
                FirstName = r.Table.Columns.Contains("FirstName") && r["FirstName"] != DBNull.Value ? r["FirstName"]?.ToString() ?? string.Empty : string.Empty,
                MiddleName = r.Table.Columns.Contains("MiddleName") && r["MiddleName"] != DBNull.Value ? r["MiddleName"]?.ToString() : null,
                LastName = r.Table.Columns.Contains("LastName") && r["LastName"] != DBNull.Value ? r["LastName"]?.ToString() ?? string.Empty : string.Empty,
                Email = r.Table.Columns.Contains("Email") && r["Email"] != DBNull.Value ? r["Email"]?.ToString() : null,
                Phone = r.Table.Columns.Contains("Phone") && r["Phone"] != DBNull.Value ? r["Phone"]?.ToString() : null,
                MobilePhone = r.Table.Columns.Contains("MobilePhone") && r["MobilePhone"] != DBNull.Value ? r["MobilePhone"]?.ToString() : null,
                Designation = r.Table.Columns.Contains("Designation") && r["Designation"] != DBNull.Value ? r["Designation"]?.ToString() : null,
                Department = r.Table.Columns.Contains("Department") && r["Department"] != DBNull.Value ? r["Department"]?.ToString() : null,
                IsActive = r.Table.Columns.Contains("IsActive") && r["IsActive"] != DBNull.Value && Convert.ToBoolean(r["IsActive"]),
                EmployeeCode = r.Table.Columns.Contains("EmployeeCode") && r["EmployeeCode"] != DBNull.Value ? r["EmployeeCode"]?.ToString() : null,
                DOB = r.Table.Columns.Contains("DOB") && r["DOB"] != DBNull.Value && r["DOB"] != null ? (DateTime?)Convert.ToDateTime(r["DOB"]) : null,
                DOJ = r.Table.Columns.Contains("DOJ") && r["DOJ"] != DBNull.Value && r["DOJ"] != null ? (DateTime?)Convert.ToDateTime(r["DOJ"]) : null,
                DateOfLeaving = r.Table.Columns.Contains("DateOfLeaving") && r["DateOfLeaving"] != DBNull.Value && r["DateOfLeaving"] != null ? (DateTime?)Convert.ToDateTime(r["DateOfLeaving"]) : null,
                Address = r.Table.Columns.Contains("Address") && r["Address"] != DBNull.Value ? r["Address"]?.ToString() : null,
                CityId = r.Table.Columns.Contains("CityId") && r["CityId"] != DBNull.Value && Guid.TryParse(r["CityId"]?.ToString(), out var cityId) ? cityId : Guid.Empty,
                StateId = r.Table.Columns.Contains("StateId") && r["StateId"] != DBNull.Value && Guid.TryParse(r["StateId"]?.ToString(), out var stateId) ? stateId : Guid.Empty,
                CountryId = r.Table.Columns.Contains("CountryId") && r["CountryId"] != DBNull.Value && Guid.TryParse(r["CountryId"]?.ToString(), out var countryId) ? countryId : Guid.Empty,
                ZipCode = r.Table.Columns.Contains("ZipCode") && r["ZipCode"] != DBNull.Value ? r["ZipCode"]?.ToString() : null,
                Gender = r.Table.Columns.Contains("Gender") && r["Gender"] != DBNull.Value ? r["Gender"]?.ToString() : null,
                MaritalStatusId = r.Table.Columns.Contains("MaritalStatusId") && r["MaritalStatusId"] != DBNull.Value && Guid.TryParse(r["MaritalStatusId"]?.ToString(), out var maritalStatusId) ? maritalStatusId : Guid.Empty,
                Image = r.Table.Columns.Contains("Image") && r["Image"] != DBNull.Value ? (byte[])r["Image"] : null,
                Qualification = r.Table.Columns.Contains("Qualification") && r["Qualification"] != DBNull.Value ? r["Qualification"]?.ToString() : null,
                Salary = r.Table.Columns.Contains("Salary") && r["Salary"] != DBNull.Value && decimal.TryParse(r["Salary"]?.ToString(), out var salary) ? salary : (decimal?)null,
                BankAccountNumber = r.Table.Columns.Contains("BankAccountNumber") && r["BankAccountNumber"] != DBNull.Value ? r["BankAccountNumber"]?.ToString() : null,
                BankName = r.Table.Columns.Contains("BankName") && r["BankName"] != DBNull.Value ? r["BankName"]?.ToString() : null,
                IFSCCode = r.Table.Columns.Contains("IFSCCode") && r["IFSCCode"] != DBNull.Value ? r["IFSCCode"]?.ToString() : null,
                PAN = r.Table.Columns.Contains("PAN") && r["PAN"] != DBNull.Value ? r["PAN"]?.ToString() : null,
                AadharNumber = r.Table.Columns.Contains("AadharNumber") && r["AadharNumber"] != DBNull.Value ? r["AadharNumber"]?.ToString() : null,
                EmergencyContactName = r.Table.Columns.Contains("EmergencyContactName") && r["EmergencyContactName"] != DBNull.Value ? r["EmergencyContactName"]?.ToString() : null,
                EmergencyContactNumber = r.Table.Columns.Contains("EmergencyContactNumber") && r["EmergencyContactNumber"] != DBNull.Value ? r["EmergencyContactNumber"]?.ToString() : null,
                EmergencyContactRelation = r.Table.Columns.Contains("EmergencyContactRelation") && r["EmergencyContactRelation"] != DBNull.Value ? r["EmergencyContactRelation"]?.ToString() : null,
                CompanyId = r.Table.Columns.Contains("CompanyId") && r["CompanyId"] != DBNull.Value && r["CompanyId"] is Guid companyIdGuid ? companyIdGuid : Guid.Empty,
                SchoolId = r.Table.Columns.Contains("SchoolId") && r["SchoolId"] != DBNull.Value && r["SchoolId"] is Guid schoolIdGuid ? schoolIdGuid : Guid.Empty,
                CreatedBy = r.Table.Columns.Contains("CreatedBy") && r["CreatedBy"] != DBNull.Value && r["CreatedBy"] is Guid createdByGuid ? createdByGuid : Guid.Empty,
                CreatedDate = r.Table.Columns.Contains("CreatedDate") && r["CreatedDate"] != DBNull.Value && r["CreatedDate"] is DateTime createdDate ? createdDate : DateTime.UtcNow,
                ModifiedBy = r.Table.Columns.Contains("ModifiedBy") && r["ModifiedBy"] != DBNull.Value && r["ModifiedBy"] is Guid modifiedByGuid ? modifiedByGuid : (Guid?)null,
                ModifiedDate = r.Table.Columns.Contains("ModifiedDate") && r["ModifiedDate"] != DBNull.Value && r["ModifiedDate"] is DateTime modifiedDate ? modifiedDate : (DateTime?)null
            };

            return t;
        }

        private void ValidateNonTeachingEntity(NonTeachingMaster entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (string.IsNullOrWhiteSpace(entity.FirstName))
                throw new ArgumentException("First name is required", nameof(entity.FirstName));
                
            if (string.IsNullOrWhiteSpace(entity.LastName))
                throw new ArgumentException("Last name is required", nameof(entity.LastName));
                
            if (entity.DOB == default || entity.DOB > DateTime.Today.AddYears(-18))
                throw new ArgumentException("Date of birth is required and must be at least 18 years ago", nameof(entity.DOB));
                
            if (entity.DOJ == default || entity.DOJ < entity.DOB)
                throw new ArgumentException("Date of joining is required and must be after date of birth", nameof(entity.DOJ));
                
            if (entity.DateOfLeaving.HasValue && entity.DateOfLeaving < entity.DOJ)
                throw new ArgumentException("Date of leaving cannot be before date of joining", nameof(entity.DateOfLeaving));
                
            if (string.IsNullOrWhiteSpace(entity.Email))
                throw new ArgumentException("Email is required", nameof(entity.Email));
                
            if (!string.IsNullOrWhiteSpace(entity.Email) && !IsValidEmail(entity.Email))
                throw new ArgumentException("Invalid email format", nameof(entity.Email));
                
            if (entity.CompanyId == Guid.Empty)
                throw new ArgumentException("Company ID is required", nameof(entity.CompanyId));
                
            if (entity.SchoolId == Guid.Empty)
                throw new ArgumentException("School ID is required", nameof(entity.SchoolId));
                
            if (entity.CreatedBy == Guid.Empty)
                throw new ArgumentException("CreatedBy is required", nameof(entity.CreatedBy));
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private Guid GetCurrentUserId()
        {
            // Implement this method to get the current user's ID
            // This is just a placeholder - replace with your actual implementation
            return Guid.NewGuid(); // Replace with actual user ID
        }
    }
}