using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.Services
{
    public class AuthorMasterService : IAuthorMasterService
    {
        private readonly IRepository<AuthorMaster> _repository;
        private readonly ILogger<AuthorMasterService>? _logger;

        public AuthorMasterService(
            IRepository<AuthorMaster> repository,
            ILogger<AuthorMasterService>? logger = null)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
        }

        public List<AuthorMaster> GetAll()
        {
            try
            {
                return _repository.GetAll().Where(a => !a.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting all author records");
                return new List<AuthorMaster>();
            }
        }

        public AuthorMaster? GetById(Guid id)
        {
            try
            {
                return _repository.GetAll().FirstOrDefault(a => a.Id == id && !a.IsDeleted);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting author by ID: {Id}", id);
                return null;
            }
        }

        public async Task<AuthorMaster?> GetByIdAsync(Guid id)
        {
            try
            {
                var result = _repository.GetById(id);
                return result is null || result.IsDeleted ? null : result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting author by ID (async): {Id}", id);
                return null;
            }
        }

        public Guid Create(AuthorMaster author)
        {
            if (author == null) throw new ArgumentNullException(nameof(author));

            try
            {
                author.Id = Guid.NewGuid();
                author.CreatedDate = DateTime.UtcNow;
                author.IsActive = true;
                author.IsDeleted = false;
                author.Status = "ACT";
                author.StatusMessage = "Active";
                
                _repository.Add(author);
                _repository.SaveChanges();
                
                return author.Id;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creating author record");
                return Guid.Empty;
            }
        }

        public bool Update(AuthorMaster author)
        {
            if (author == null) throw new ArgumentNullException(nameof(author));

            try
            {
                var existing = _repository.GetAll().FirstOrDefault(a => a.Id == author.Id && !a.IsDeleted);
                if (existing == null)
                {
                    _logger?.LogWarning("Author record not found for update: {Id}", author.Id);
                    return false;
                }

                existing.Name = author.Name;
                existing.Description = author.Description;
                existing.Address1 = author.Address1;
                existing.Address2 = author.Address2;
                existing.CityId = author.CityId;
                existing.StateId = author.StateId;
                existing.CountryId = author.CountryId;
                existing.ZipCode = author.ZipCode;
                existing.Phone = author.Phone;
                existing.Mobile = author.Mobile;
                existing.Email = author.Email;
                existing.CompanyId = author.CompanyId;
                existing.SchoolId = author.SchoolId;
                existing.Status = author.Status ?? "UPD";
                existing.StatusMessage = author.StatusMessage ?? "Updated";
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = author.ModifiedBy;

                _repository.Update(existing);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error updating author record: {Id}", author.Id);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(AuthorMaster author)
        {
            if (author == null) throw new ArgumentNullException(nameof(author));

            try
            {
                var existing = _repository.GetById(author.Id);
                if (existing == null || existing.IsDeleted)
                {
                    _logger?.LogWarning("Author record not found for async update: {Id}", author.Id);
                    return false;
                }

                existing.Name = author.Name;
                existing.Description = author.Description;
                existing.Address1 = author.Address1;
                existing.Address2 = author.Address2;
                existing.CityId = author.CityId;
                existing.StateId = author.StateId;
                existing.CountryId = author.CountryId;
                existing.ZipCode = author.ZipCode;
                existing.Phone = author.Phone;
                existing.Mobile = author.Mobile;
                existing.Email = author.Email;
                existing.CompanyId = author.CompanyId;
                existing.SchoolId = author.SchoolId;
                existing.Status = author.Status ?? "UPD";
                existing.StatusMessage = author.StatusMessage ?? "Updated";
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = author.ModifiedBy;

                _repository.Update(existing);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error in async update of author record: {Id}", author.Id);
                return false;
            }
        }

        public async Task<Guid> CreateAsync(AuthorMaster author)
        {
            if (author == null) throw new ArgumentNullException(nameof(author));

            try
            {
                author.Id = Guid.NewGuid();
                author.CreatedDate = DateTime.UtcNow;
                author.IsActive = true;
                author.IsDeleted = false;
                author.Status = "ACT";
                author.StatusMessage = "Active";
                
                _repository.Add(author);
                await _repository.SaveChangesAsync();
                
                return author.Id;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creating author record (async)");
                return Guid.Empty;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var author = _repository.GetById(id);
                if (author == null || author.IsDeleted)
                {
                    _logger?.LogWarning("Author record not found for async deletion: {Id}", id);
                    return false;
                }

                author.IsDeleted = true;
                author.ModifiedDate = DateTime.UtcNow;
                _repository.Update(author);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error deleting author record (async): {Id}", id);
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var author = _repository.GetAll().FirstOrDefault(a => a.Id == id && !a.IsDeleted);
                if (author == null)
                {
                    _logger?.LogWarning("Author record not found for deletion: {Id}", id);
                    return false;
                }

                author.IsDeleted = true;
                author.ModifiedDate = DateTime.UtcNow;
                _repository.Update(author);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error deleting author record: {Id}", id);
                return false;
            }
        }
    }
}
