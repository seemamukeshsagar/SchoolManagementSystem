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