using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.Services
{
    public class EmpCategoryMasterService : IEmpCategoryMasterService
    {
        private readonly IRepository<EmpCategoryMaster> _repository;
        private readonly ILogger<EmpCategoryMasterService>? _logger;

        public EmpCategoryMasterService(
            IRepository<EmpCategoryMaster> repository,
            ILogger<EmpCategoryMasterService>? logger = null)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
        }

        public List<EmpCategoryMaster> GetAll()
        {
            try
            {
                return _repository.GetAll().Where(c => !c.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting all employee category records");
                return new List<EmpCategoryMaster>();
            }
        }

        public EmpCategoryMaster? GetById(Guid id)
        {
            try
            {
                return _repository.GetAll().FirstOrDefault(c => c.Id == id && !c.IsDeleted);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting employee category by ID: {Id}", id);
                return null;
            }
        }

        public async Task<EmpCategoryMaster?> GetByIdAsync(Guid id)
        {
            try
            {
                var result = _repository.GetById(id);
                return result is null || result.IsDeleted ? null : result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting employee category by ID (async): {Id}", id);
                return null;
            }
        }

        public Guid Create(EmpCategoryMaster category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            try
            {
                category.Id = Guid.NewGuid();
                category.CreatedDate = DateTime.UtcNow;
                category.IsActive = true;
                category.IsDeleted = false;
                category.Status = "ACT";
                category.StatusMessage = "Active";
                
                _repository.Add(category);
                _repository.SaveChanges();
                
                return category.Id;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creating employee category record");
                return Guid.Empty;
            }
        }

        public bool Update(EmpCategoryMaster category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            try
            {
                var existing = _repository.GetAll().FirstOrDefault(c => c.Id == category.Id && !c.IsDeleted);
                if (existing == null)
                {
                    _logger?.LogWarning("Employee category record not found for update: {Id}", category.Id);
                    return false;
                }

                existing.CategoryName = category.CategoryName;
                existing.CategoryDescription = category.CategoryDescription;
                existing.CompanyId = category.CompanyId;
                existing.SchoolId = category.SchoolId;
                existing.Status = category.Status ?? "UPD";
                existing.StatusMessage = category.StatusMessage ?? "Updated";
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = category.ModifiedBy;

                _repository.Update(existing);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error updating employee category record: {Id}", category.Id);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(EmpCategoryMaster category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            try
            {
                var existing = _repository.GetById(category.Id);
                if (existing == null || existing.IsDeleted)
                {
                    _logger?.LogWarning("Employee category record not found for async update: {Id}", category.Id);
                    return false;
                }

                existing.CategoryName = category.CategoryName;
                existing.CategoryDescription = category.CategoryDescription;
                existing.CompanyId = category.CompanyId;
                existing.SchoolId = category.SchoolId;
                existing.Status = category.Status ?? "UPD";
                existing.StatusMessage = category.StatusMessage ?? "Updated";
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = category.ModifiedBy;

                _repository.Update(existing);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error in async update of employee category record: {Id}", category.Id);
                return false;
            }
        }

        public async Task<Guid> CreateAsync(EmpCategoryMaster category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            try
            {
                category.Id = Guid.NewGuid();
                category.CreatedDate = DateTime.UtcNow;
                category.IsActive = true;
                category.IsDeleted = false;
                category.Status = "ACT";
                category.StatusMessage = "Active";
                
                _repository.Add(category);
                await _repository.SaveChangesAsync();
                
                return category.Id;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creating employee category record (async)");
                return Guid.Empty;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var category = _repository.GetById(id);
                if (category == null || category.IsDeleted)
                {
                    _logger?.LogWarning("Employee category record not found for async deletion: {Id}", id);
                    return false;
                }

                category.IsDeleted = true;
                category.ModifiedDate = DateTime.UtcNow;
                _repository.Update(category);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error deleting employee category record (async): {Id}", id);
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var category = _repository.GetAll().FirstOrDefault(c => c.Id == id && !c.IsDeleted);
                if (category == null)
                {
                    _logger?.LogWarning("Employee category record not found for deletion: {Id}", id);
                    return false;
                }

                category.IsDeleted = true;
                category.ModifiedDate = DateTime.UtcNow;
                _repository.Update(category);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error deleting employee category record: {Id}", id);
                return false;
            }
        }
    }
}
