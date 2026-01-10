using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.Services
{
    public class BookMasterService : IBookMasterService
    {
        private readonly IRepository<BookMaster> _repository;
        private readonly ILogger<BookMasterService>? _logger;

        public BookMasterService(
            IRepository<BookMaster> repository,
            ILogger<BookMasterService>? logger = null)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
        }

        public List<BookMaster> GetAll()
        {
            try
            {
                return _repository.GetAll().Where(b => !b.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting all book records");
                return new List<BookMaster>();
            }
        }

        public BookMaster? GetById(Guid id)
        {
            try
            {
                return _repository.GetAll().FirstOrDefault(b => b.Id == id && !b.IsDeleted);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting book by ID: {Id}", id);
                return null;
            }
        }

        public async Task<BookMaster?> GetByIdAsync(Guid id)
        {
            try
            {
                var result = _repository.GetById(id);
                return result is null || result.IsDeleted ? null : result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting book by ID (async): {Id}", id);
                return null;
            }
        }

        public Guid Create(BookMaster book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            try
            {
                book.Id = Guid.NewGuid();
                book.CreatedDate = DateTime.UtcNow;
                book.IsActive = true;
                book.IsDeleted = false;
                book.Status = "ACT";
                book.StatusMessage = "Active";
                
                _repository.Add(book);
                _repository.SaveChanges();
                
                return book.Id;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creating book record");
                return Guid.Empty;
            }
        }

        public bool Update(BookMaster book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            try
            {
                var existing = _repository.GetAll().FirstOrDefault(b => b.Id == book.Id && !b.IsDeleted);
                if (existing == null)
                {
                    _logger?.LogWarning("Book record not found for update: {Id}", book.Id);
                    return false;
                }

                existing.Title = book.Title;
                existing.AuthorId = book.AuthorId;
                existing.PublisherId = book.PublisherId;
                existing.CategoryId = book.CategoryId;
                existing.ISBNNumber = book.ISBNNumber;
                existing.Edition = book.Edition;
                existing.Price = book.Price;
                existing.NoOfCopies = book.NoOfCopies;
                existing.StockInHand = book.StockInHand;
                existing.Description = book.Description;
                existing.CompanyId = book.CompanyId;
                existing.SchoolId = book.SchoolId;
                existing.Status = book.Status ?? "UPD";
                existing.StatusMessage = book.StatusMessage ?? "Updated";
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = book.ModifiedBy;

                _repository.Update(existing);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error updating book record: {Id}", book.Id);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(BookMaster book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            try
            {
                var existing = _repository.GetById(book.Id);
                if (existing == null || existing.IsDeleted)
                {
                    _logger?.LogWarning("Book record not found for async update: {Id}", book.Id);
                    return false;
                }

                existing.Title = book.Title;
                existing.AuthorId = book.AuthorId;
                existing.PublisherId = book.PublisherId;
                existing.CategoryId = book.CategoryId;
                existing.ISBNNumber = book.ISBNNumber;
                existing.Edition = book.Edition;
                existing.Price = book.Price;
                existing.NoOfCopies = book.NoOfCopies;
                existing.StockInHand = book.StockInHand;
                existing.Description = book.Description;
                existing.CompanyId = book.CompanyId;
                existing.SchoolId = book.SchoolId;
                existing.Status = book.Status ?? "UPD";
                existing.StatusMessage = book.StatusMessage ?? "Updated";
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = book.ModifiedBy;

                _repository.Update(existing);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error in async update of book record: {Id}", book.Id);
                return false;
            }
        }

        public async Task<Guid> CreateAsync(BookMaster book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            try
            {
                book.Id = Guid.NewGuid();
                book.CreatedDate = DateTime.UtcNow;
                book.IsActive = true;
                book.IsDeleted = false;
                book.Status = "ACT";
                book.StatusMessage = "Active";
                
                _repository.Add(book);
                await _repository.SaveChangesAsync();
                
                return book.Id;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creating book record (async)");
                return Guid.Empty;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var book = _repository.GetById(id);
                if (book == null || book.IsDeleted)
                {
                    _logger?.LogWarning("Book record not found for async deletion: {Id}", id);
                    return false;
                }

                book.IsDeleted = true;
                book.ModifiedDate = DateTime.UtcNow;
                _repository.Update(book);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error deleting book record (async): {Id}", id);
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var book = _repository.GetAll().FirstOrDefault(b => b.Id == id && !b.IsDeleted);
                if (book == null)
                {
                    _logger?.LogWarning("Book record not found for deletion: {Id}", id);
                    return false;
                }

                book.IsDeleted = true;
                book.ModifiedDate = DateTime.UtcNow;
                _repository.Update(book);
                _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error deleting book record: {Id}", id);
                return false;
            }
        }
    }
}
