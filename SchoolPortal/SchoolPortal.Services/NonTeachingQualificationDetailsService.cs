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
            Proc p = new Proc("sp_NonTeachingQualification_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
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
                    var dt = new DataTable();
                    p.Exec(dt);
                    if (dt.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
            Proc p = new Proc("sp_NonTeachingQualification_Delete");
            p["@Id"] = id;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1;
        }
    }
}