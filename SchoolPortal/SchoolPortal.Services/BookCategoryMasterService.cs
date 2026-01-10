using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.Services
{
    public class BookCategoryMasterService : IBookCategoryMasterService
    {
        private readonly IRepository<BookCategoryMaster> _repository;
        private readonly ILogger<BookCategoryMasterService>? _logger;

        public BookCategoryMasterService(
            IRepository<BookCategoryMaster> repository,
            ILogger<BookCategoryMasterService>? logger = null)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
        }

        public List<BookCategoryMaster> GetAll()
        {
            try
            {
                return _repository.GetAll().Where(c => !c.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting all book category records");
                return new List<BookCategoryMaster>();
            }
        }

        public BookCategoryMaster? GetById(Guid id)
        {
            try
            {
                return _repository.GetAll().FirstOrDefault(c => c.Id == id && !c.IsDeleted);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting book category by ID: {Id}", id);
                return null;
            }
        }

        public async Task<BookCategoryMaster?> GetByIdAsync(Guid id)
        {
            try
            {
                var result = _repository.GetById(id);
                return result is null || result.IsDeleted ? null : result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting book category by ID (async): {Id}", id);
                return null;
            }
        }

        public Guid Create(BookCategoryMaster bookCategory)
        {
            if (bookCategory == null) throw new ArgumentNullException(nameof(bookCategory));

            try
            {
                bookCategory.Id = Guid.NewGuid();
                bookCategory.CreatedDate = DateTime.UtcNow;
                bookCategory.IsActive = true;
                bookCategory.IsDeleted = false;
                bookCategory.Status = "ACT";
                bookCategory.StatusMessage = "Active";
                
                _repository.Add(bookCategory);
                _repository.SaveChanges();
                
                return bookCategory.Id;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creating book category record");
                return Guid.Empty;
            }
        }

        public bool Update(BookCategoryMaster bookCategory)
        {
            if (bookCategory == null) throw new ArgumentNullException(nameof(bookCategory));

            try
            {
                var existing = _repository.GetAll().FirstOrDefault(c => c.Id == bookCategory.Id && !c.IsDeleted);
                if (existing == null)
                {
                    _logger?.LogWarning("Book category record not found for update: {Id}", bookCategory.Id);
                    return false;
                }

                existing.Name = bookCategory.Name;
                existing.Description = bookCategory.Description;
                existing.CompanyId = bookCategory.CompanyId;
                existing.SchoolId = bookCategory.SchoolId;
                existing.Status = bookCategory.Status ?? "UPD";
                existing.StatusMessage = bookCategory.StatusMessage ?? "Updated";
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = bookCategory.ModifiedBy;

                _repository.Update(existing);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error updating book category record: {Id}", bookCategory.Id);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(BookCategoryMaster bookCategory)
        {
            if (bookCategory == null) throw new ArgumentNullException(nameof(bookCategory));

            try
            {
                var existing = _repository.GetById(bookCategory.Id);
                if (existing == null || existing.IsDeleted)
                {
                    _logger?.LogWarning("Book category record not found for async update: {Id}", bookCategory.Id);
                    return false;
                }

                existing.Name = bookCategory.Name;
                existing.Description = bookCategory.Description;
                existing.CompanyId = bookCategory.CompanyId;
                existing.SchoolId = bookCategory.SchoolId;
                existing.Status = bookCategory.Status ?? "UPD";
                existing.StatusMessage = bookCategory.StatusMessage ?? "Updated";
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = bookCategory.ModifiedBy;

                _repository.Update(existing);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error in async update of book category record: {Id}", bookCategory.Id);
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var bookCategory = _repository.GetAll().FirstOrDefault(c => c.Id == id && !c.IsDeleted);
                if (bookCategory == null)
                {
                    _logger?.LogWarning("Book category record not found for deletion: {Id}", id);
                    return false;
                }

                bookCategory.IsDeleted = true;
                bookCategory.ModifiedDate = DateTime.UtcNow;
                _repository.Update(bookCategory);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error deleting book category record: {Id}", id);
                return false;
            }
        }
    }
}
