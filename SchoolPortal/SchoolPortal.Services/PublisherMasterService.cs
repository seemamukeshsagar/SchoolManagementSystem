using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.Services
{
    public class PublisherMasterService : IPublisherMasterService
    {
        private readonly IRepository<PublisherMaster> _repository;
        private readonly ILogger<PublisherMasterService>? _logger;

        public PublisherMasterService(
            IRepository<PublisherMaster> repository,
            ILogger<PublisherMasterService>? logger = null)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
        }

        public List<PublisherMaster> GetAll()
        {
            try
            {
                return _repository.GetAll().Where(p => !p.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting all publisher records");
                return new List<PublisherMaster>();
            }
        }

        public PublisherMaster? GetById(Guid id)
        {
            try
            {
                return _repository.GetAll().FirstOrDefault(p => p.Id == id && !p.IsDeleted);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting publisher by ID: {Id}", id);
                return null;
            }
        }

        public async Task<PublisherMaster?> GetByIdAsync(Guid id)
        {
            try
            {
                var result = _repository.GetById(id);
                return result is null || result.IsDeleted ? null : result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting publisher by ID (async): {Id}", id);
                return null;
            }
        }

        public Guid Create(PublisherMaster publisher)
        {
            if (publisher == null) throw new ArgumentNullException(nameof(publisher));

            try
            {
                publisher.Id = Guid.NewGuid();
                publisher.CreatedDate = DateTime.UtcNow;
                publisher.IsActive = true;
                publisher.IsDeleted = false;
                publisher.Status = "ACT";
                publisher.StatusMessage = "Active";
                
                _repository.Add(publisher);
                _repository.SaveChanges();
                
                return publisher.Id;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creating publisher record");
                return Guid.Empty;
            }
        }

        public bool Update(PublisherMaster publisher)
        {
            if (publisher == null) throw new ArgumentNullException(nameof(publisher));

            try
            {
                var existing = _repository.GetAll().FirstOrDefault(p => p.Id == publisher.Id && !p.IsDeleted);
                if (existing == null)
                {
                    _logger?.LogWarning("Publisher record not found for update: {Id}", publisher.Id);
                    return false;
                }

                existing.PublisherName = publisher.PublisherName;
                existing.Description = publisher.Description;
                existing.Address1 = publisher.Address1;
                existing.Address2 = publisher.Address2;
                existing.CityId = publisher.CityId;
                existing.StateId = publisher.StateId;
                existing.CountryId = publisher.CountryId;
                existing.ZipCode = publisher.ZipCode;
                existing.PhoneNumber = publisher.PhoneNumber;
                existing.MobileNumber = publisher.MobileNumber;
                existing.EmailId = publisher.EmailId;
                existing.CompanyId = publisher.CompanyId;
                existing.SchoolId = publisher.SchoolId;
                existing.Status = publisher.Status ?? "UPD";
                existing.StatusMessage = publisher.StatusMessage ?? "Updated";
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = publisher.ModifiedBy;

                _repository.Update(existing);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error updating publisher record: {Id}", publisher.Id);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(PublisherMaster publisher)
        {
            if (publisher == null) throw new ArgumentNullException(nameof(publisher));

            try
            {
                var existing = _repository.GetById(publisher.Id);
                if (existing == null || existing.IsDeleted)
                {
                    _logger?.LogWarning("Publisher record not found for async update: {Id}", publisher.Id);
                    return false;
                }

                existing.PublisherName = publisher.PublisherName;
                existing.Description = publisher.Description;
                existing.Address1 = publisher.Address1;
                existing.Address2 = publisher.Address2;
                existing.CityId = publisher.CityId;
                existing.StateId = publisher.StateId;
                existing.CountryId = publisher.CountryId;
                existing.ZipCode = publisher.ZipCode;
                existing.PhoneNumber = publisher.PhoneNumber;
                existing.MobileNumber = publisher.MobileNumber;
                existing.EmailId = publisher.EmailId;
                existing.CompanyId = publisher.CompanyId;
                existing.SchoolId = publisher.SchoolId;
                existing.Status = publisher.Status ?? "UPD";
                existing.StatusMessage = publisher.StatusMessage ?? "Updated";
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = publisher.ModifiedBy;

                _repository.Update(existing);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error in async update of publisher record: {Id}", publisher.Id);
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var publisher = _repository.GetAll().FirstOrDefault(p => p.Id == id && !p.IsDeleted);
                if (publisher == null)
                {
                    _logger?.LogWarning("Publisher record not found for deletion: {Id}", id);
                    return false;
                }

                publisher.IsDeleted = true;
                publisher.ModifiedDate = DateTime.UtcNow;
                _repository.Update(publisher);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error deleting publisher record: {Id}", id);
                return false;
            }
        }
    }
}
