using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.Services
{
    public class StudentReportCardMasterService : IStudentReportCardMasterService
    {
        private readonly IRepository<StudentReportCardMaster> _repository;
        private readonly ILogger<StudentReportCardMasterService>? _logger;

        public StudentReportCardMasterService(
            IRepository<StudentReportCardMaster> repository,
            ILogger<StudentReportCardMasterService>? logger = null)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
        }

        public List<StudentReportCardMaster> GetAll()
        {
            try
            {
                return _repository.GetAll().Where(s => !s.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting all student report card records");
                return new List<StudentReportCardMaster>();
            }
        }

        public StudentReportCardMaster? GetById(Guid id)
        {
            try
            {
                return _repository.GetAll().FirstOrDefault(s => s.Id == id && !s.IsDeleted);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting student report card by ID: {Id}", id);
                return null;
            }
        }

        public async Task<StudentReportCardMaster?> GetByIdAsync(Guid id)
        {
            try
            {
                var result = _repository.GetById(id);
                return result is null || result.IsDeleted ? null : result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting student report card by ID (async): {Id}", id);
                return null;
            }
        }

        public Guid Create(StudentReportCardMaster reportCard)
        {
            if (reportCard == null) throw new ArgumentNullException(nameof(reportCard));

            try
            {
                reportCard.Id = Guid.NewGuid();
                reportCard.CreatedDate = DateTime.UtcNow;
                reportCard.IsActive = true;
                reportCard.IsDeleted = false;
                reportCard.Status = "ACT";
                reportCard.StatusMessage = "Active";
                
                _repository.Add(reportCard);
                _repository.SaveChanges();
                
                return reportCard.Id;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creating student report card record");
                return Guid.Empty;
            }
        }

        public bool Update(StudentReportCardMaster reportCard)
        {
            if (reportCard == null) throw new ArgumentNullException(nameof(reportCard));

            try
            {
                var existing = _repository.GetAll().FirstOrDefault(s => s.Id == reportCard.Id && !s.IsDeleted);
                if (existing == null)
                {
                    _logger?.LogWarning("Student report card record not found for update: {Id}", reportCard.Id);
                    return false;
                }

                existing.StudentId = reportCard.StudentId;
                existing.ClassId = reportCard.ClassId;
                existing.SectionId = reportCard.SectionId;
                existing.SessionId = reportCard.SessionId;
                existing.ReportCardType = reportCard.ReportCardType;
                existing.ReportCardValue = reportCard.ReportCardValue;
                existing.Period = reportCard.Period;
                existing.CompanyId = reportCard.CompanyId;
                existing.SchoolId = reportCard.SchoolId;
                existing.Status = reportCard.Status ?? "UPD";
                existing.StatusMessage = reportCard.StatusMessage ?? "Updated";
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = reportCard.ModifiedBy;

                _repository.Update(existing);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error updating student report card record: {Id}", reportCard.Id);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(StudentReportCardMaster reportCard)
        {
            if (reportCard == null) throw new ArgumentNullException(nameof(reportCard));

            try
            {
                var existing = _repository.GetById(reportCard.Id);
                if (existing == null || existing.IsDeleted)
                {
                    _logger?.LogWarning("Student report card record not found for async update: {Id}", reportCard.Id);
                    return false;
                }

                existing.StudentId = reportCard.StudentId;
                existing.ClassId = reportCard.ClassId;
                existing.SectionId = reportCard.SectionId;
                existing.SessionId = reportCard.SessionId;
                existing.ReportCardType = reportCard.ReportCardType;
                existing.ReportCardValue = reportCard.ReportCardValue;
                existing.Period = reportCard.Period;
                existing.CompanyId = reportCard.CompanyId;
                existing.SchoolId = reportCard.SchoolId;
                existing.Status = reportCard.Status ?? "UPD";
                existing.StatusMessage = reportCard.StatusMessage ?? "Updated";
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = reportCard.ModifiedBy;

                _repository.Update(existing);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error in async update of student report card record: {Id}", reportCard.Id);
                return false;
            }
        }

        public async Task<Guid> CreateAsync(StudentReportCardMaster reportCard)
        {
            if (reportCard == null) throw new ArgumentNullException(nameof(reportCard));

            try
            {
                reportCard.Id = Guid.NewGuid();
                reportCard.CreatedDate = DateTime.UtcNow;
                reportCard.IsActive = true;
                reportCard.IsDeleted = false;
                reportCard.Status = "ACT";
                reportCard.StatusMessage = "Active";
                
                _repository.Add(reportCard);
                await _repository.SaveChangesAsync();
                
                return reportCard.Id;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creating student report card record (async)");
                return Guid.Empty;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var reportCard = _repository.GetById(id);
                if (reportCard == null || reportCard.IsDeleted)
                {
                    _logger?.LogWarning("Student report card record not found for async deletion: {Id}", id);
                    return false;
                }

                reportCard.IsDeleted = true;
                reportCard.ModifiedDate = DateTime.UtcNow;
                _repository.Update(reportCard);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error deleting student report card record (async): {Id}", id);
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var reportCard = _repository.GetAll().FirstOrDefault(s => s.Id == id && !s.IsDeleted);
                if (reportCard == null)
                {
                    _logger?.LogWarning("Student report card record not found for deletion: {Id}", id);
                    return false;
                }

                reportCard.IsDeleted = true;
                reportCard.ModifiedDate = DateTime.UtcNow;
                _repository.Update(reportCard);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error deleting student report card record: {Id}", id);
                return false;
            }
        }
    }
}
