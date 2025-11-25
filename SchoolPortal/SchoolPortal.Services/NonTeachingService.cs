// SchoolPortal.Services/Services/NonTeachingService.cs
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SchoolPortal.Services.Services
{
    public class NonTeachingService : INonTeachingService
    {
        private readonly ILogger<NonTeachingService> _logger;
        private readonly IDbConnection _connection;

        public NonTeachingService(ILogger<NonTeachingService> logger, IDbConnection connection)
        {
            _logger = logger;
            _connection = connection;
        }

        public async Task<IEnumerable<NonTeachingMaster>> GetAllAsync()
        {
            try
            {
                using (var p = new Proc("sp_NonTeaching_GetAll"))
                {
                    var result = await p.ExecAsync<NonTeachingMaster>();
                    return result ?? new List<NonTeachingMaster>();
                }
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "Database error in NonTeachingService.GetAllAsync");
                throw new ApplicationException("An error occurred while retrieving non-teaching staff. Please try again.", sqlEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in NonTeachingService.GetAllAsync");
                throw new ApplicationException("An unexpected error occurred. Please try again later.", ex);
            }
        }

        public async Task<IEnumerable<NonTeachingMaster>> GetBySchoolIdAsync(Guid schoolId)
        {
            if (schoolId == Guid.Empty)
            {
                throw new ArgumentException("School ID cannot be empty", nameof(schoolId));
            }

            try
            {
                using (var p = new Proc("sp_NonTeaching_GetBySchoolId"))
                {
                    p["@SchoolId"] = schoolId;
                    var result = await p.ExecAsync<NonTeachingMaster>();
                    return result ?? new List<NonTeachingMaster>();
                }
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, $"Database error in NonTeachingService.GetBySchoolId for SchoolId: {schoolId}");
                throw new ApplicationException("An error occurred while retrieving non-teaching staff for the school. Please try again.", sqlEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error in NonTeachingService.GetBySchoolId for SchoolId: {schoolId}");
                throw new ApplicationException("An unexpected error occurred. Please try again later.", ex);
            }
        }

        public async Task<NonTeachingMaster> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID cannot be empty", nameof(id));
            }

            try
            {
                using (var p = new Proc("sp_NonTeaching_GetById"))
                {
                    p["@Id"] = id;
                    var result = await p.ExecAsync<NonTeachingMaster>();
                    return result?.FirstOrDefault();
                }
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, $"Database error in NonTeachingService.GetById for ID: {id}");
                throw new ApplicationException("An error occurred while retrieving the non-teaching staff details. Please try again.", sqlEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error in NonTeachingService.GetById for ID: {id}");
                throw new ApplicationException("An unexpected error occurred. Please try again later.", ex);
            }
        }

        public async Task<int> AddAsync(NonTeachingMaster entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            ValidateNonTeachingEntity(entity);

            try
            {
                using (var p = new Proc("sp_NonTeaching_Insert"))
                {
                    p["@Id"] = entity.Id;
                    p["@FirstName"] = entity.FirstName;
                    p["@MiddleName"] = entity.MiddleName ?? (object)DBNull.Value;
                    p["@LastName"] = entity.LastName;
                    p["@DOB"] = entity.DOB;
                    p["@DOJ"] = entity.DOJ;
                    p["@DateOfLeaving"] = entity.DateOfLeaving ?? (object)DBNull.Value;
                    p["@Address"] = entity.Address ?? (object)DBNull.Value;
                    p["@CityId"] = entity.CityId ?? (object)DBNull.Value;
                    p["@StateId"] = entity.StateId ?? (object)DBNull.Value;
                    p["@CountryId"] = entity.CountryId ?? (object)DBNull.Value;
                    p["@ZipCode"] = entity.ZipCode ?? (object)DBNull.Value;
                    p["@Gender"] = entity.Gender ?? (object)DBNull.Value;
                    p["@MaritalStatusId"] = entity.MaritalStatusId ?? (object)DBNull.Value;
                    p["@Image"] = entity.Image ?? (object)DBNull.Value;
                    p["@Phone"] = entity.Phone ?? (object)DBNull.Value;
                    p["@MobilePhone"] = entity.MobilePhone ?? (object)DBNull.Value;
                    p["@Email"] = entity.Email ?? (object)DBNull.Value;
                    p["@EmployeeCode"] = entity.EmployeeCode ?? (object)DBNull.Value;
                    p["@Designation"] = entity.Designation ?? (object)DBNull.Value;
                    p["@Department"] = entity.Department ?? (object)DBNull.Value;
                    p["@Qualification"] = entity.Qualification ?? (object)DBNull.Value;
                    p["@Salary"] = entity.Salary ?? (object)DBNull.Value;
                    p["@BankAccountNumber"] = entity.BankAccountNumber ?? (object)DBNull.Value;
                    p["@BankName"] = entity.BankName ?? (object)DBNull.Value;
                    p["@IFSCCode"] = entity.IFSCCode ?? (object)DBNull.Value;
                    p["@PAN"] = entity.PAN ?? (object)DBNull.Value;
                    p["@AadharNumber"] = entity.AadharNumber ?? (object)DBNull.Value;
                    p["@EmergencyContactName"] = entity.EmergencyContactName ?? (object)DBNull.Value;
                    p["@EmergencyContactNumber"] = entity.EmergencyContactNumber ?? (object)DBNull.Value;
                    p["@EmergencyContactRelation"] = entity.EmergencyContactRelation ?? (object)DBNull.Value;
                    p["@CompanyId"] = entity.CompanyId;
                    p["@SchoolId"] = entity.SchoolId;
                    p["@IsActive"] = entity.IsActive;
                    p["@CreatedBy"] = entity.CreatedBy;

                    var result = await p.ExecNonQueryAsync();
                    return result;
                }
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 2627) // Unique constraint violation
            {
                _logger.LogError(sqlEx, "Duplicate entry in NonTeachingService.Add");
                throw new ApplicationException("A non-teaching staff with the same employee code or email already exists.", sqlEx);
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "Database error in NonTeachingService.Add");
                throw new ApplicationException("An error occurred while adding the non-teaching staff. Please try again.", sqlEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in NonTeachingService.Add");
                throw new ApplicationException("An unexpected error occurred. Please try again later.", ex);
            }
        }

        public async Task<bool> UpdateAsync(NonTeachingMaster entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (entity.Id == Guid.Empty)
            {
                throw new ArgumentException("ID cannot be empty", nameof(entity.Id));
            }

            ValidateNonTeachingEntity(entity);

            try
            {
                using (var p = new Proc("sp_NonTeaching_Update"))
                {
                    p["@Id"] = entity.Id;
                    p["@FirstName"] = entity.FirstName;
                    p["@MiddleName"] = entity.MiddleName ?? (object)DBNull.Value;
                    p["@LastName"] = entity.LastName;
                    p["@DOB"] = entity.DOB;
                    p["@DOJ"] = entity.DOJ;
                    p["@DateOfLeaving"] = entity.DateOfLeaving ?? (object)DBNull.Value;
                    p["@Address"] = entity.Address ?? (object)DBNull.Value;
                    p["@CityId"] = entity.CityId ?? (object)DBNull.Value;
                    p["@StateId"] = entity.StateId ?? (object)DBNull.Value;
                    p["@CountryId"] = entity.CountryId ?? (object)DBNull.Value;
                    p["@ZipCode"] = entity.ZipCode ?? (object)DBNull.Value;
                    p["@Gender"] = entity.Gender ?? (object)DBNull.Value;
                    p["@MaritalStatusId"] = entity.MaritalStatusId ?? (object)DBNull.Value;
                    p["@Image"] = entity.Image ?? (object)DBNull.Value;
                    p["@Phone"] = entity.Phone ?? (object)DBNull.Value;
                    p["@MobilePhone"] = entity.MobilePhone ?? (object)DBNull.Value;
                    p["@Email"] = entity.Email ?? (object)DBNull.Value;
                    p["@EmployeeCode"] = entity.EmployeeCode ?? (object)DBNull.Value;
                    p["@Designation"] = entity.Designation ?? (object)DBNull.Value;
                    p["@Department"] = entity.Department ?? (object)DBNull.Value;
                    p["@Qualification"] = entity.Qualification ?? (object)DBNull.Value;
                    p["@Salary"] = entity.Salary ?? (object)DBNull.Value;
                    p["@BankAccountNumber"] = entity.BankAccountNumber ?? (object)DBNull.Value;
                    p["@BankName"] = entity.BankName ?? (object)DBNull.Value;
                    p["@IFSCCode"] = entity.IFSCCode ?? (object)DBNull.Value;
                    p["@PAN"] = entity.PAN ?? (object)DBNull.Value;
                    p["@AadharNumber"] = entity.AadharNumber ?? (object)DBNull.Value;
                    p["@EmergencyContactName"] = entity.EmergencyContactName ?? (object)DBNull.Value;
                    p["@EmergencyContactNumber"] = entity.EmergencyContactNumber ?? (object)DBNull.Value;
                    p["@EmergencyContactRelation"] = entity.EmergencyContactRelation ?? (object)DBNull.Value;
                    p["@IsActive"] = entity.IsActive;
                    p["@ModifiedBy"] = entity.ModifiedBy;

                    var result = await p.ExecNonQueryAsync();
                    return result > 0;
                }
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 2627) // Unique constraint violation
            {
                _logger.LogError(sqlEx, $"Duplicate entry in NonTeachingService.Update for ID: {entity.Id}");
                throw new ApplicationException("A non-teaching staff with the same employee code or email already exists.", sqlEx);
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, $"Database error in NonTeachingService.Update for ID: {entity.Id}");
                throw new ApplicationException("An error occurred while updating the non-teaching staff. Please try again.", sqlEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error in NonTeachingService.Update for ID: {entity.Id}");
                throw new ApplicationException("An unexpected error occurred. Please try again later.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Guid id, Guid? deletedBy)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID cannot be empty", nameof(id));
            }

            try
            {
                using (var p = new Proc("sp_NonTeaching_Delete"))
                {
                    p["@Id"] = id;
                    p["@DeletedBy"] = deletedBy ?? (object)DBNull.Value;
                    var result = await p.ExecNonQueryAsync();
                    return result > 0;
                }
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, $"Database error in NonTeachingService.Delete for ID: {id}");
                throw new ApplicationException("An error occurred while deleting the non-teaching staff. Please try again.", sqlEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error in NonTeachingService.Delete for ID: {id}");
                throw new ApplicationException("An unexpected error occurred. Please try again later.", ex);
            }
        }

        public async Task<bool> ToggleStatusAsync(Guid id, Guid? modifiedBy)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID cannot be empty", nameof(id));
            }

            try
            {
                using (var p = new Proc("sp_NonTeaching_ToggleStatus"))
                {
                    p["@Id"] = id;
                    p["@ModifiedBy"] = modifiedBy;
                    var result = await p.ExecNonQueryAsync();
                    return result > 0;
                }
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, $"Database error in ToggleStatus for Non-Teaching Staff ID: {id}");
                throw new ApplicationException("An error occurred while updating the status. Please try again.", sqlEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error in NonTeachingService.ToggleStatus for ID: {id}");
                throw new ApplicationException("An unexpected error occurred. Please try again later.", ex);
            }
        }
    }
}

// SchoolPortal.Services/Services/NonTeachingDocumentDetailsService.cs
//using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Logging;
//using SchoolPortal.DBAccess;
//using SchoolPortal.Entities.Models;
//using SchoolPortal.Services.IServices;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;

//namespace SchoolPortal.Services.Services
//{
//    public class NonTeachingDocumentDetailsService : INonTeachingDocumentDetailsService
//    {
//        private readonly ILogger<NonTeachingDocumentDetailsService> _logger;
//        private readonly IDbConnection _connection;

//        public NonTeachingDocumentDetailsService(ILogger<NonTeachingDocumentDetailsService> logger, IDbConnection connection)
//        {
//            _logger = logger;
//            _connection = connection;
//        }

//        public IEnumerable<NonTeachingDocumentDetails> GetByNonTeachingId(Guid nonTeachingId)
//        {
//            try
//            {
//                using (var p = new Proc("sp_NonTeachingDocument_GetByNonTeachingId"))
//                {
//                    p["@NonTeachingId"] = nonTeachingId;
//                    return p.Exec<NonTeachingDocumentDetails>();
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, $"Error in NonTeachingDocumentDetailsService.GetByNonTeachingId for ID: {nonTeachingId}");
//                throw;
//            }
//        }

//        public NonTeachingDocumentDetails GetDocumentById(Guid id)
//        {
//            try
//            {
//                using (var p = new Proc("sp_NonTeachingDocument_GetById"))
//                {
//                    p["@Id"] = id;
//                    return p.Exec<NonTeachingDocumentDetails>().FirstOrDefault();
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, $"Error in NonTeachingDocumentDetailsService.GetDocumentById for ID: {id}");
//                throw;
//            }
//        }

//        public bool Add(NonTeachingDocumentDetails entity)
//        {
//            try
//            {
//                using (var p = new Proc("sp_NonTeachingDocument_Insert"))
//                {
//                    p["@Id"] = entity.Id;
//                    p["@NonTeachingId"] = entity.NonTeachingId;
//                    p["@DocumentTypeId"] = entity.DocumentTypeId;
//                    p["@DocumentNumber"] = entity.DocumentNumber;
//                    p["@DocumentPath"] = entity.DocumentPath;
//                    p["@IssueDate"] = entity.IssueDate;
//                    p["@ExpiryDate"] = entity.ExpiryDate;
//                    p["@IsVerified"] = entity.IsVerified;
//                    p["@VerifiedBy"] = entity.VerifiedBy;
//                    p["@VerifiedOn"] = entity.VerifiedOn;
//                    p["@Remarks"] = entity.Remarks;
//                    p["@CreatedBy"] = entity.CreatedBy;

//                    return p.ExecNonQuery() > 0;
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error in NonTeachingDocumentDetailsService.Add");
//                throw;
//            }
//        }

//        public bool Update(NonTeachingDocumentDetails entity)
//        {
//            try
//            {
//                using (var p = new Proc("sp_NonTeachingDocument_Update"))
//                {
//                    p["@Id"] = entity.Id;
//                    p["@DocumentTypeId"] = entity.DocumentTypeId;
//                    p["@DocumentNumber"] = entity.DocumentNumber;
//                    p["@DocumentPath"] = entity.DocumentPath;
//                    p["@IssueDate"] = entity.IssueDate;
//                    p["@ExpiryDate"] = entity.ExpiryDate;
//                    p["@IsVerified"] = entity.IsVerified;
//                    p["@VerifiedBy"] = entity.VerifiedBy;
//                    p["@VerifiedOn"] = entity.VerifiedOn;
//                    p["@Remarks"] = entity.Remarks;
//                    p["@ModifiedBy"] = entity.ModifiedBy;

//                    return p.ExecNonQuery() > 0;
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, $"Error in NonTeachingDocumentDetailsService.Update for ID: {entity.Id}");
//                throw;
//            }
//        }

//        public bool Delete(Guid id)
//        {
//            try
//            {
//                using (var p = new Proc("sp_NonTeachingDocument_Delete"))
//                {
//                    p["@Id"] = id;
//                    return p.ExecNonQuery() > 0;
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, $"Error in NonTeachingDocumentDetailsService.Delete for ID: {id}");
//                throw;
//            }
//        }
//    }
//}

//// SchoolPortal.Services/Services/NonTeachingQualificationDetailsService.cs
//using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Logging;
//using SchoolPortal.DBAccess;
//using SchoolPortal.Entities.Models;
//using SchoolPortal.Services.IServices;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;

//namespace SchoolPortal.Services.Services
//{
//    public class NonTeachingQualificationDetailsService : INonTeachingQualificationDetailsService
//    {
//        private readonly ILogger<NonTeachingQualificationDetailsService> _logger;
//        private readonly IDbConnection _connection;

//        public NonTeachingQualificationDetailsService(ILogger<NonTeachingQualificationDetailsService> logger, IDbConnection connection)
//        {
//            _logger = logger;
//            _connection = connection;
//        }

//        public IEnumerable<NonTeachingQualificationDetails> GetByNonTeachingId(Guid nonTeachingId)
//        {
//            try
//            {
//                using (var p = new Proc("sp_NonTeachingQualification_GetByNonTeachingId"))
//                {
//                    p["@NonTeachingId"] = nonTeachingId;
//                    return p.Exec<NonTeachingQualificationDetails>();
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, $"Error in NonTeachingQualificationDetailsService.GetByNonTeachingId for ID: {nonTeachingId}");
//                throw;
//            }
//        }

//        public NonTeachingQualificationDetails GetQualificationById(Guid id)
//        {
//            try
//            {
//                using (var p = new Proc("sp_NonTeachingQualification_GetById"))
//                {
//                    p["@Id"] = id;
//                    return p.Exec<NonTeachingQualificationDetails>().FirstOrDefault();
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, $"Error in NonTeachingQualificationDetailsService.GetQualificationById for ID: {id}");
//                throw;
//            }
//        }

//        public bool Add(NonTeachingQualificationDetails entity)
//        {
//            try
//            {
//                using (var p = new Proc("sp_NonTeachingQualification_Insert"))
//                {
//                    p["@Id"] = entity.Id;
//                    p["@NonTeachingId"] = entity.NonTeachingId;
//                    p["@QualificationTypeId"] = entity.QualificationTypeId;
//                    p["@Institution"] = entity.Institution;
//                    p["@BoardUniversity"] = entity.BoardUniversity;
//                    p["@YearOfPassing"] = entity.YearOfPassing;
//                    p["@Percentage"] = entity.Percentage;
//                    p["@Division"] = entity.Division;
//                    p["@DocumentPath"] = entity.DocumentPath;
//                    p["@IsVerified"] = entity.IsVerified;
//                    p["@VerifiedBy"] = entity.VerifiedBy;
//                    p["@VerifiedOn"] = entity.VerifiedOn;
//                    p["@Remarks"] = entity.Remarks;
//                    p["@CreatedBy"] = entity.CreatedBy;

//                    return p.ExecNonQuery() > 0;
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error in NonTeachingQualificationDetailsService.Add");
//                throw;
//            }
//        }

//        public bool Update(NonTeachingQualificationDetails entity)
//        {
//            try
//            {
//                using (var p = new Proc("sp_NonTeachingQualification_Update"))
//                {
//                    p["@Id"] = entity.Id;
//                    p["@QualificationTypeId"] = entity.QualificationTypeId;
//                    p["@Institution"] = entity.Institution;
//                    p["@BoardUniversity"] = entity.BoardUniversity;
//                    p["@YearOfPassing"] = entity.YearOfPassing;
//                    p["@Percentage"] = entity.Percentage;
//                    p["@Division"] = entity.Division;
//                    p["@DocumentPath"] = entity.DocumentPath;
//                    p["@IsVerified"] = entity.IsVerified;
//                    p["@VerifiedBy"] = entity.VerifiedBy;
//                    p["@VerifiedOn"] = entity.VerifiedOn;
//                    p["@Remarks"] = entity.Remarks;
//                    p["@ModifiedBy"] = entity.ModifiedBy;

//                    return p.ExecNonQuery() > 0;
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, $"Error in NonTeachingQualificationDetailsService.Update for ID: {entity.Id}");
//                throw;
//            }
//        }

//        public bool Delete(Guid id)
//        {
//            try
//            {
//                using (var p = new Proc("sp_NonTeachingQualification_Delete"))
//                {
//                    p["@Id"] = id;
//                    return p.ExecNonQuery() > 0;
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, $"Error in NonTeachingQualificationDetailsService.Delete for ID: {id}");
//                throw;
//            }
//        }
//    }
//}