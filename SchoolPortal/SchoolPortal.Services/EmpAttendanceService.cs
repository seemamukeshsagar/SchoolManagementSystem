// Update EmpAttendanceService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolPortal.Data;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services.Services
{
    public class EmpAttendanceService : IEmpAttendanceService
    {
        private readonly IRepository<EmpAttendanceDetails> _repository;
        private readonly ILogger<EmpAttendanceService>? _logger;

        public EmpAttendanceService(
            IRepository<EmpAttendanceDetails> repository,
            ILogger<EmpAttendanceService>? logger = null)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
        }

        public List<EmpAttendanceDetails> GetAll()
        {
            try
            {
                return _repository.GetAll().Where(a => !a.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting all attendance records");
                return new List<EmpAttendanceDetails>();
            }
        }

        public EmpAttendanceDetails? GetById(Guid id)
        {
            try
            {
                return _repository.GetAll().FirstOrDefault(a => a.Id == id && !a.IsDeleted);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting attendance by ID: {Id}", id);
                return null;
            }
        }

        public async Task<EmpAttendanceDetails?> GetByIdAsync(Guid id)
        {
            try
            {
                var result = _repository.GetById(id);
                return result is null || result.IsDeleted ? null : result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting attendance by ID (async): {Id}", id);
                return null;
            }
        }

        public Guid Create(EmpAttendanceDetails attendance)
        {
            if (attendance == null) throw new ArgumentNullException(nameof(attendance));

            try
            {
                attendance.Id = Guid.NewGuid();
                attendance.CreatedDate = DateTime.UtcNow;
                attendance.IsActive = true;
                attendance.IsDeleted = false;
                attendance.Status = "ACT";
                attendance.StatusMessage = "Active";
                
                _repository.Add(attendance);
                _repository.SaveChanges();
                
                return attendance.Id;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creating attendance record");
                return Guid.Empty;
            }
        }

        public bool Update(EmpAttendanceDetails attendance)
        {
            if (attendance == null) throw new ArgumentNullException(nameof(attendance));

            try
            {
                var existing = _repository.GetAll().FirstOrDefault(a => a.Id == attendance.Id && !a.IsDeleted);
                if (existing == null)
                {
                    _logger?.LogWarning("Attendance record not found for update: {Id}", attendance.Id);
                    return false;
                }

                existing.EmployeeId = attendance.EmployeeId;
                existing.AttendenceDate = attendance.AttendenceDate;
                existing.AttendenceMarked = attendance.AttendenceMarked;
                existing.AttendenceLeaveTypeId = attendance.AttendenceLeaveTypeId;
                existing.IsHalfDay = attendance.IsHalfDay;
                existing.AttendenceTime = attendance.AttendenceTime ?? string.Empty;
                existing.Status = attendance.Status ?? "UPD";
                existing.StatusMessage = attendance.StatusMessage ?? "Updated";
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = attendance.ModifiedBy;

                _repository.Update(existing);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error updating attendance record: {Id}", attendance.Id);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(EmpAttendanceDetails attendance)
        {
            if (attendance == null) throw new ArgumentNullException(nameof(attendance));

            try
            {
                var existing = _repository.GetById(attendance.Id);
                if (existing == null || existing.IsDeleted)
                {
                    _logger?.LogWarning("Attendance record not found for async update: {Id}", attendance.Id);
                    return false;
                }

                existing.EmployeeId = attendance.EmployeeId;
                existing.AttendenceDate = attendance.AttendenceDate;
                existing.AttendenceMarked = attendance.AttendenceMarked;
                existing.AttendenceLeaveTypeId = attendance.AttendenceLeaveTypeId;
                existing.IsHalfDay = attendance.IsHalfDay;
                existing.AttendenceTime = attendance.AttendenceTime ?? string.Empty;
                existing.Status = attendance.Status ?? "UPD";
                existing.StatusMessage = attendance.StatusMessage ?? "Updated";
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = attendance.ModifiedBy;

                _repository.Update(existing);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error in async update of attendance record: {Id}", attendance.Id);
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var attendance = _repository.GetAll().FirstOrDefault(a => a.Id == id && !a.IsDeleted);
                if (attendance == null)
                {
                    _logger?.LogWarning("Attendance record not found for deletion: {Id}", id);
                    return false;
                }

                attendance.IsDeleted = true;
                attendance.ModifiedDate = DateTime.UtcNow;
                _repository.Update(attendance);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error deleting attendance record: {Id}", id);
                return false;
            }
        }
    }
}