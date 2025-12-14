// SchoolPortal.Services/Services/EmpAttendanceService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using SchoolPortal.Data;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;


namespace SchoolPortal.Services.Services
{
    public class EmpAttendanceService : IEmpAttendanceService
    {
         private readonly IRepository<EmpAttendanceDetails> _repository;

        public EmpAttendanceService(IRepository<EmpAttendanceDetails> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<EmpAttendanceDetails> GetAll()
        {
            try
            {
                return _repository.GetAll().Where(a => !a.IsDeleted).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EmpAttendanceDetails? GetById(Guid id)
        {
            try
            {
                return _repository.GetAll().FirstOrDefault(a => a.Id == id && !a.IsDeleted);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Guid Create(EmpAttendanceDetails attendance)
        {
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
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        public bool Update(EmpAttendanceDetails attendance)
        {
            try
            {
                var existing = _repository.GetAll().FirstOrDefault(a => a.Id == attendance.Id && !a.IsDeleted);
                if (existing == null)
                    return false;
                existing.EmployeeId = attendance.EmployeeId;
                existing.AttendenceDate = attendance.AttendenceDate;
                existing.AttendenceMarked = attendance.AttendenceMarked;
                existing.AttendenceLeaveTypeId = attendance.AttendenceLeaveTypeId;
                existing.IsHalfDay = attendance.IsHalfDay;
                existing.AttendenceTime = attendance.AttendenceTime;
                existing.Status = attendance.Status;
                existing.StatusMessage = attendance.StatusMessage;
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = attendance.ModifiedBy;
                _repository.Update(existing);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(EmpAttendanceDetails attendance)
        {
            try
            {
                // Using synchronous GetById since IRepository doesn't have GetByIdAsync
                var existing = _repository.GetById(attendance.Id);
                if (existing == null)
                    return false;
                
                existing.EmployeeId = attendance.EmployeeId;
                existing.AttendenceDate = attendance.AttendenceDate;
                existing.AttendenceMarked = attendance.AttendenceMarked;
                existing.AttendenceLeaveTypeId = attendance.AttendenceLeaveTypeId;
                existing.IsHalfDay = attendance.IsHalfDay;
                existing.AttendenceTime = attendance.AttendenceTime;
                existing.Status = attendance.Status;
                existing.StatusMessage = attendance.StatusMessage;
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = attendance.ModifiedBy;
                
                _repository.Update(existing);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var attendance = _repository.GetAll().FirstOrDefault(a => a.Id == id && !a.IsDeleted);
                if (attendance == null)
                    return false;
                attendance.IsDeleted = true;
                attendance.ModifiedDate = DateTime.UtcNow;
                _repository.Update(attendance);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<EmpAttendanceDetails> GetByIdAsync(Guid id)
        {
            try
            {
                // Using synchronous GetById since IRepository doesn't have GetByIdAsync
                return await Task.FromResult(_repository.GetById(id));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}