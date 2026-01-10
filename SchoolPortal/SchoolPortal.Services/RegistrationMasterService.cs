using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.Services
{
    public class RegistrationMasterService : IRegistrationMasterService
    {
        private readonly IRepository<RegistrationMaster> _repository;
        private readonly ILogger<RegistrationMasterService>? _logger;

        public RegistrationMasterService(
            IRepository<RegistrationMaster> repository,
            ILogger<RegistrationMasterService>? logger = null)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
        }

        public List<RegistrationMaster> GetAll()
        {
            try
            {
                return _repository.GetAll().Where(r => !r.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting all registration records");
                return new List<RegistrationMaster>();
            }
        }

        public RegistrationMaster? GetById(Guid id)
        {
            try
            {
                return _repository.GetAll().FirstOrDefault(r => r.Id == id && !r.IsDeleted);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting registration by ID: {Id}", id);
                return null;
            }
        }

        public async Task<RegistrationMaster?> GetByIdAsync(Guid id)
        {
            try
            {
                var result = _repository.GetById(id);
                return result is null || result.IsDeleted ? null : result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting registration by ID (async): {Id}", id);
                return null;
            }
        }

        public Guid Create(RegistrationMaster registration)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));

            try
            {
                registration.Id = Guid.NewGuid();
                registration.CreatedDate = DateTime.UtcNow;
                registration.IsActive = true;
                registration.IsDeleted = false;
                registration.Status = "ACT";
                registration.StatusMessage = "Active";
                
                _repository.Add(registration);
                _repository.SaveChanges();
                
                return registration.Id;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creating registration record");
                return Guid.Empty;
            }
        }

        public bool Update(RegistrationMaster registration)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));

            try
            {
                var existing = _repository.GetAll().FirstOrDefault(r => r.Id == registration.Id && !r.IsDeleted);
                if (existing == null)
                {
                    _logger?.LogWarning("Registration record not found for update: {Id}", registration.Id);
                    return false;
                }

                existing.ClassId = registration.ClassId;
                existing.SessionId = registration.SessionId;
                existing.RegistrationNumber = registration.RegistrationNumber;
                existing.Status = registration.Status ?? "UPD";
                existing.StatusMessage = registration.StatusMessage ?? "Updated";
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = registration.ModifiedBy;

                _repository.Update(existing);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error updating registration record: {Id}", registration.Id);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(RegistrationMaster registration)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));

            try
            {
                var existing = _repository.GetById(registration.Id);
                if (existing == null || existing.IsDeleted)
                {
                    _logger?.LogWarning("Registration record not found for async update: {Id}", registration.Id);
                    return false;
                }

                existing.ClassId = registration.ClassId;
                existing.SessionId = registration.SessionId;
                existing.RegistrationNumber = registration.RegistrationNumber;
                existing.Status = registration.Status ?? "UPD";
                existing.StatusMessage = registration.StatusMessage ?? "Updated";
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = registration.ModifiedBy;

                _repository.Update(existing);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error in async update of registration record: {Id}", registration.Id);
                return false;
            }
        }

        public async Task<Guid> CreateAsync(RegistrationMaster registration)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));

            try
            {
                registration.Id = Guid.NewGuid();
                registration.CreatedDate = DateTime.UtcNow;
                registration.IsActive = true;
                registration.IsDeleted = false;
                registration.Status = "ACT";
                registration.StatusMessage = "Active";
                
                _repository.Add(registration);
                await _repository.SaveChangesAsync();
                
                return registration.Id;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creating registration record (async)");
                return Guid.Empty;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var registration = _repository.GetById(id);
                if (registration == null || registration.IsDeleted)
                {
                    _logger?.LogWarning("Registration record not found for async deletion: {Id}", id);
                    return false;
                }

                registration.IsDeleted = true;
                registration.ModifiedDate = DateTime.UtcNow;
                _repository.Update(registration);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error deleting registration record (async): {Id}", id);
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var registration = _repository.GetAll().FirstOrDefault(r => r.Id == id && !r.IsDeleted);
                if (registration == null)
                {
                    _logger?.LogWarning("Registration record not found for deletion: {Id}", id);
                    return false;
                }

                registration.IsDeleted = true;
                registration.ModifiedDate = DateTime.UtcNow;
                _repository.Update(registration);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error deleting registration record: {Id}", id);
                return false;
            }
        }
    }
}
