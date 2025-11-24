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

        public IEnumerable<NonTeachingMaster> GetAll()
        {
            try
            {
                using (var p = new Proc("sp_NonTeaching_GetAll"))
                {
                    return p.Exec<NonTeachingMaster>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in NonTeachingService.GetAll");
                throw;
            }
        }

        public IEnumerable<NonTeachingMaster> GetBySchoolId(Guid schoolId)
        {
            try
            {
                using (var p = new Proc("sp_NonTeaching_GetBySchoolId"))
                {
                    p["@SchoolId"] = schoolId;
                    return p.Exec<NonTeachingMaster>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in NonTeachingService.GetBySchoolId for SchoolId: {schoolId}");
                throw;
            }
        }

        public NonTeachingMaster GetById(Guid id)
        {
            try
            {
                using (var p = new Proc("sp_NonTeaching_GetById"))
                {
                    p["@Id"] = id;
                    return p.Exec<NonTeachingMaster>().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in NonTeachingService.GetById for ID: {id}");
                throw;
            }
        }

        public int Add(NonTeachingMaster entity)
        {
            try
            {
                using (var p = new Proc("sp_NonTeaching_Insert"))
                {
                    p["@Id"] = entity.Id;
                    p["@FirstName"] = entity.FirstName;
                    p["@MiddleName"] = entity.MiddleName;
                    p["@LastName"] = entity.LastName;
                    p["@DOB"] = entity.DOB;
                    p["@DOJ"] = entity.DOJ;
                    p["@DateOfLeaving"] = entity.DateOfLeaving;
                    p["@Address"] = entity.Address;
                    p["@CityId"] = entity.CityId;
                    p["@StateId"] = entity.StateId;
                    p["@CountryId"] = entity.CountryId;
                    p["@ZipCode"] = entity.ZipCode;
                    p["@Gender"] = entity.Gender;
                    p["@MaritalStatusId"] = entity.MaritalStatusId;
                    p["@Image"] = entity.Image;
                    p["@Phone"] = entity.Phone;
                    p["@MobilePhone"] = entity.MobilePhone;
                    p["@Email"] = entity.Email;
                    p["@EmployeeCode"] = entity.EmployeeCode;
                    p["@Designation"] = entity.Designation;
                    p["@Department"] = entity.Department;
                    p["@Qualification"] = entity.Qualification;
                    p["@Salary"] = entity.Salary;
                    p["@BankAccountNumber"] = entity.BankAccountNumber;
                    p["@BankName"] = entity.BankName;
                    p["@IFSCCode"] = entity.IFSCCode;
                    p["@PAN"] = entity.PAN;
                    p["@AadharNumber"] = entity.AadharNumber;
                    p["@EmergencyContactName"] = entity.EmergencyContactName;
                    p["@EmergencyContactNumber"] = entity.EmergencyContactNumber;
                    p["@EmergencyContactRelation"] = entity.EmergencyContactRelation;
                    p["@CompanyId"] = entity.CompanyId;
                    p["@SchoolId"] = entity.SchoolId;
                    p["@IsActive"] = entity.IsActive;
                    p["@CreatedBy"] = entity.CreatedBy;

                    return p.ExecNonQuery();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in NonTeachingService.Add");
                throw;
            }
        }

        public bool Update(NonTeachingMaster entity)
        {
            try
            {
                using (var p = new Proc("sp_NonTeaching_Update"))
                {
                    p["@Id"] = entity.Id;
                    p["@FirstName"] = entity.FirstName;
                    p["@MiddleName"] = entity.MiddleName;
                    p["@LastName"] = entity.LastName;
                    p["@DOB"] = entity.DOB;
                    p["@DOJ"] = entity.DOJ;
                    p["@DateOfLeaving"] = entity.DateOfLeaving;
                    p["@Address"] = entity.Address;
                    p["@CityId"] = entity.CityId;
                    p["@StateId"] = entity.StateId;
                    p["@CountryId"] = entity.CountryId;
                    p["@ZipCode"] = entity.ZipCode;
                    p["@Gender"] = entity.Gender;
                    p["@MaritalStatusId"] = entity.MaritalStatusId;
                    p["@Image"] = entity.Image;
                    p["@Phone"] = entity.Phone;
                    p["@MobilePhone"] = entity.MobilePhone;
                    p["@Email"] = entity.Email;
                    p["@EmployeeCode"] = entity.EmployeeCode;
                    p["@Designation"] = entity.Designation;
                    p["@Department"] = entity.Department;
                    p["@Qualification"] = entity.Qualification;
                    p["@Salary"] = entity.Salary;
                    p["@BankAccountNumber"] = entity.BankAccountNumber;
                    p["@BankName"] = entity.BankName;
                    p["@IFSCCode"] = entity.IFSCCode;
                    p["@PAN"] = entity.PAN;
                    p["@AadharNumber"] = entity.AadharNumber;
                    p["@EmergencyContactName"] = entity.EmergencyContactName;
                    p["@EmergencyContactNumber"] = entity.EmergencyContactNumber;
                    p["@EmergencyContactRelation"] = entity.EmergencyContactRelation;
                    p["@IsActive"] = entity.IsActive;
                    p["@ModifiedBy"] = entity.ModifiedBy;

                    return p.ExecNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in NonTeachingService.Update for ID: {entity.Id}");
                throw;
            }
        }

        public bool Delete(Guid id, Guid deletedBy)
        {
            try
            {
                using (var p = new Proc("sp_NonTeaching_Delete"))
                {
                    p["@Id"] = id;
                    p["@DeletedBy"] = deletedBy;
                    return p.ExecNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in NonTeachingService.Delete for ID: {id}");
                throw;
            }
        }

        public bool ToggleStatus(Guid id, Guid modifiedBy)
        {
            try
            {
                using (var p = new Proc("sp_NonTeaching_ToggleStatus"))
                {
                    p["@Id"] = id;
                    p["@ModifiedBy"] = modifiedBy;
                    return p.ExecNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in NonTeachingService.ToggleStatus for ID: {id}");
                throw;
            }
        }
    }
}

// SchoolPortal.Services/Services/NonTeachingDocumentDetailsService.cs
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
    public class NonTeachingDocumentDetailsService : INonTeachingDocumentDetailsService
    {
        private readonly ILogger<NonTeachingDocumentDetailsService> _logger;
        private readonly IDbConnection _connection;

        public NonTeachingDocumentDetailsService(ILogger<NonTeachingDocumentDetailsService> logger, IDbConnection connection)
        {
            _logger = logger;
            _connection = connection;
        }

        public IEnumerable<NonTeachingDocumentDetails> GetByNonTeachingId(Guid nonTeachingId)
        {
            try
            {
                using (var p = new Proc("sp_NonTeachingDocument_GetByNonTeachingId"))
                {
                    p["@NonTeachingId"] = nonTeachingId;
                    return p.Exec<NonTeachingDocumentDetails>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in NonTeachingDocumentDetailsService.GetByNonTeachingId for ID: {nonTeachingId}");
                throw;
            }
        }

        public NonTeachingDocumentDetails GetDocumentById(Guid id)
        {
            try
            {
                using (var p = new Proc("sp_NonTeachingDocument_GetById"))
                {
                    p["@Id"] = id;
                    return p.Exec<NonTeachingDocumentDetails>().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in NonTeachingDocumentDetailsService.GetDocumentById for ID: {id}");
                throw;
            }
        }

        public bool Add(NonTeachingDocumentDetails entity)
        {
            try
            {
                using (var p = new Proc("sp_NonTeachingDocument_Insert"))
                {
                    p["@Id"] = entity.Id;
                    p["@NonTeachingId"] = entity.NonTeachingId;
                    p["@DocumentTypeId"] = entity.DocumentTypeId;
                    p["@DocumentNumber"] = entity.DocumentNumber;
                    p["@DocumentPath"] = entity.DocumentPath;
                    p["@IssueDate"] = entity.IssueDate;
                    p["@ExpiryDate"] = entity.ExpiryDate;
                    p["@IsVerified"] = entity.IsVerified;
                    p["@VerifiedBy"] = entity.VerifiedBy;
                    p["@VerifiedOn"] = entity.VerifiedOn;
                    p["@Remarks"] = entity.Remarks;
                    p["@CreatedBy"] = entity.CreatedBy;

                    return p.ExecNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in NonTeachingDocumentDetailsService.Add");
                throw;
            }
        }

        public bool Update(NonTeachingDocumentDetails entity)
        {
            try
            {
                using (var p = new Proc("sp_NonTeachingDocument_Update"))
                {
                    p["@Id"] = entity.Id;
                    p["@DocumentTypeId"] = entity.DocumentTypeId;
                    p["@DocumentNumber"] = entity.DocumentNumber;
                    p["@DocumentPath"] = entity.DocumentPath;
                    p["@IssueDate"] = entity.IssueDate;
                    p["@ExpiryDate"] = entity.ExpiryDate;
                    p["@IsVerified"] = entity.IsVerified;
                    p["@VerifiedBy"] = entity.VerifiedBy;
                    p["@VerifiedOn"] = entity.VerifiedOn;
                    p["@Remarks"] = entity.Remarks;
                    p["@ModifiedBy"] = entity.ModifiedBy;

                    return p.ExecNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in NonTeachingDocumentDetailsService.Update for ID: {entity.Id}");
                throw;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                using (var p = new Proc("sp_NonTeachingDocument_Delete"))
                {
                    p["@Id"] = id;
                    return p.ExecNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in NonTeachingDocumentDetailsService.Delete for ID: {id}");
                throw;
            }
        }
    }
}

// SchoolPortal.Services/Services/NonTeachingQualificationDetailsService.cs
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
    public class NonTeachingQualificationDetailsService : INonTeachingQualificationDetailsService
    {
        private readonly ILogger<NonTeachingQualificationDetailsService> _logger;
        private readonly IDbConnection _connection;

        public NonTeachingQualificationDetailsService(ILogger<NonTeachingQualificationDetailsService> logger, IDbConnection connection)
        {
            _logger = logger;
            _connection = connection;
        }

        public IEnumerable<NonTeachingQualificationDetails> GetByNonTeachingId(Guid nonTeachingId)
        {
            try
            {
                using (var p = new Proc("sp_NonTeachingQualification_GetByNonTeachingId"))
                {
                    p["@NonTeachingId"] = nonTeachingId;
                    return p.Exec<NonTeachingQualificationDetails>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in NonTeachingQualificationDetailsService.GetByNonTeachingId for ID: {nonTeachingId}");
                throw;
            }
        }

        public NonTeachingQualificationDetails GetQualificationById(Guid id)
        {
            try
            {
                using (var p = new Proc("sp_NonTeachingQualification_GetById"))
                {
                    p["@Id"] = id;
                    return p.Exec<NonTeachingQualificationDetails>().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in NonTeachingQualificationDetailsService.GetQualificationById for ID: {id}");
                throw;
            }
        }

        public bool Add(NonTeachingQualificationDetails entity)
        {
            try
            {
                using (var p = new Proc("sp_NonTeachingQualification_Insert"))
                {
                    p["@Id"] = entity.Id;
                    p["@NonTeachingId"] = entity.NonTeachingId;
                    p["@QualificationTypeId"] = entity.QualificationTypeId;
                    p["@Institution"] = entity.Institution;
                    p["@BoardUniversity"] = entity.BoardUniversity;
                    p["@YearOfPassing"] = entity.YearOfPassing;
                    p["@Percentage"] = entity.Percentage;
                    p["@Division"] = entity.Division;
                    p["@DocumentPath"] = entity.DocumentPath;
                    p["@IsVerified"] = entity.IsVerified;
                    p["@VerifiedBy"] = entity.VerifiedBy;
                    p["@VerifiedOn"] = entity.VerifiedOn;
                    p["@Remarks"] = entity.Remarks;
                    p["@CreatedBy"] = entity.CreatedBy;

                    return p.ExecNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in NonTeachingQualificationDetailsService.Add");
                throw;
            }
        }

        public bool Update(NonTeachingQualificationDetails entity)
        {
            try
            {
                using (var p = new Proc("sp_NonTeachingQualification_Update"))
                {
                    p["@Id"] = entity.Id;
                    p["@QualificationTypeId"] = entity.QualificationTypeId;
                    p["@Institution"] = entity.Institution;
                    p["@BoardUniversity"] = entity.BoardUniversity;
                    p["@YearOfPassing"] = entity.YearOfPassing;
                    p["@Percentage"] = entity.Percentage;
                    p["@Division"] = entity.Division;
                    p["@DocumentPath"] = entity.DocumentPath;
                    p["@IsVerified"] = entity.IsVerified;
                    p["@VerifiedBy"] = entity.VerifiedBy;
                    p["@VerifiedOn"] = entity.VerifiedOn;
                    p["@Remarks"] = entity.Remarks;
                    p["@ModifiedBy"] = entity.ModifiedBy;

                    return p.ExecNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in NonTeachingQualificationDetailsService.Update for ID: {entity.Id}");
                throw;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                using (var p = new Proc("sp_NonTeachingQualification_Delete"))
                {
                    p["@Id"] = id;
                    return p.ExecNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in NonTeachingQualificationDetailsService.Delete for ID: {id}");
                throw;
            }
        }
    }
}