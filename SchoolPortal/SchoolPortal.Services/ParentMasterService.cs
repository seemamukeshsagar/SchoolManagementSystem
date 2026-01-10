using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.Services
{
    public class ParentMasterService : IParentMasterService
    {
        private readonly IRepository<ParentMaster> _repository;
        private readonly ILogger<ParentMasterService>? _logger;

        public ParentMasterService(
            IRepository<ParentMaster> repository,
            ILogger<ParentMasterService>? logger = null)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
        }

        public List<ParentMaster> GetAll()
        {
            try
            {
                return _repository.GetAll().Where(p => !p.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting all parent records");
                return new List<ParentMaster>();
            }
        }

        public ParentMaster? GetById(Guid id)
        {
            try
            {
                return _repository.GetAll().FirstOrDefault(p => p.Id == id && !p.IsDeleted);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting parent by ID: {Id}", id);
                return null;
            }
        }

        public async Task<ParentMaster?> GetByIdAsync(Guid id)
        {
            try
            {
                var result = _repository.GetById(id);
                return result is null || result.IsDeleted ? null : result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting parent by ID (async): {Id}", id);
                return null;
            }
        }

        public Guid Create(ParentMaster parent)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            try
            {
                parent.Id = Guid.NewGuid();
                parent.CreatedDate = DateTime.UtcNow;
                parent.IsActive = true;
                parent.IsDeleted = false;
                parent.Status = "ACT";
                parent.StatusMessage = "Active";
                
                _repository.Add(parent);
                _repository.SaveChanges();
                
                return parent.Id;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creating parent record");
                return Guid.Empty;
            }
        }

        public bool Update(ParentMaster parent)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            try
            {
                var existing = _repository.GetAll().FirstOrDefault(p => p.Id == parent.Id && !p.IsDeleted);
                if (existing == null)
                {
                    _logger?.LogWarning("Parent record not found for update: {Id}", parent.Id);
                    return false;
                }

                existing.ParentFirstName = parent.ParentFirstName;
                existing.ParentLastName = parent.ParentLastName;
                existing.Address1 = parent.Address1;
                existing.CityId = parent.CityId;
                existing.StateId = parent.StateId;
                existing.CountryId = parent.CountryId;
                existing.ZipCode = parent.ZipCode;
                existing.Phone = parent.Phone;
                existing.Mobile = parent.Mobile;
                existing.Email = parent.Email;
                existing.Occupation = parent.Occupation;
                existing.CompanyId = parent.CompanyId;
                existing.SchoolId = parent.SchoolId;
                existing.Status = parent.Status ?? "UPD";
                existing.StatusMessage = parent.StatusMessage ?? "Updated";
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = parent.ModifiedBy;

                _repository.Update(existing);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error updating parent record: {Id}", parent.Id);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(ParentMaster parent)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            try
            {
                var existing = _repository.GetById(parent.Id);
                if (existing == null || existing.IsDeleted)
                {
                    _logger?.LogWarning("Parent record not found for async update: {Id}", parent.Id);
                    return false;
                }

                existing.ParentFirstName = parent.ParentFirstName;
                existing.ParentLastName = parent.ParentLastName;
                existing.Address1 = parent.Address1;
                existing.CityId = parent.CityId;
                existing.StateId = parent.StateId;
                existing.CountryId = parent.CountryId;
                existing.ZipCode = parent.ZipCode;
                existing.Phone = parent.Phone;
                existing.Mobile = parent.Mobile;
                existing.Email = parent.Email;
                existing.Occupation = parent.Occupation;
                existing.CompanyId = parent.CompanyId;
                existing.SchoolId = parent.SchoolId;
                existing.Status = parent.Status ?? "UPD";
                existing.StatusMessage = parent.StatusMessage ?? "Updated";
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = parent.ModifiedBy;

                _repository.Update(existing);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error in async update of parent record: {Id}", parent.Id);
                return false;
            }
        }

        public async Task<Guid> CreateAsync(ParentMaster parent)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            try
            {
                parent.Id = Guid.NewGuid();
                parent.CreatedDate = DateTime.UtcNow;
                parent.IsActive = true;
                parent.IsDeleted = false;
                parent.Status = "ACT";
                parent.StatusMessage = "Active";
                
                _repository.Add(parent);
                await _repository.SaveChangesAsync();
                
                return parent.Id;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creating parent record (async)");
                return Guid.Empty;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var parent = _repository.GetById(id);
                if (parent == null || parent.IsDeleted)
                {
                    _logger?.LogWarning("Parent record not found for async deletion: {Id}", id);
                    return false;
                }

                parent.IsDeleted = true;
                parent.ModifiedDate = DateTime.UtcNow;
                _repository.Update(parent);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error deleting parent record (async): {Id}", id);
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var parent = _repository.GetAll().FirstOrDefault(p => p.Id == id && !p.IsDeleted);
                if (parent == null)
                {
                    _logger?.LogWarning("Parent record not found for deletion: {Id}", id);
                    return false;
                }

                parent.IsDeleted = true;
                parent.ModifiedDate = DateTime.UtcNow;
                _repository.Update(parent);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error deleting parent record: {Id}", id);
                return false;
            }
        }
    }
}
