using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;
using SchoolPortal.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortalApp.Controllers
{
    [Route("BookMaster")]
    public class BookMasterController : BaseController
    {
        private readonly IBookMasterService _bookMasterService;
        private readonly IBookCategoryMasterService _bookCategoryService;
        private readonly IAuthorMasterService _authorService;
        private readonly IPublisherMasterService _publisherService;
        private new readonly ILogger<BookMasterController> _logger;

        public BookMasterController(
            IBookMasterService bookMasterService,
            IBookCategoryMasterService bookCategoryService,
            IAuthorMasterService authorService,
            IPublisherMasterService publisherService,
            ILogger<BookMasterController> logger) : base(logger)
        {
            _bookMasterService = bookMasterService ?? throw new ArgumentNullException(nameof(bookMasterService));
            _bookCategoryService = bookCategoryService ?? throw new ArgumentNullException(nameof(bookCategoryService));
            _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
            _publisherService = publisherService ?? throw new ArgumentNullException(nameof(publisherService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            try
            {
                var books = _bookMasterService.GetAll() ?? new List<BookMaster>();
                var categories = _bookCategoryService.GetAll() ?? new List<BookCategoryMaster>();
                var authors = _authorService.GetAll() ?? new List<AuthorMaster>();
                var publishers = _publisherService.GetAll() ?? new List<PublisherMaster>();

                var bookViewModels = books.Select(book => new BookMasterViewModel
                {
                    Id = book.Id,
                    Code = book.Code,
                    Title = book.Title,
                    Description = book.Description,
                    Image = book.Image,
                    TypeId = book.TypeId,
                    CategoryId = book.CategoryId,
                    CategoryName = categories.FirstOrDefault(c => c.Id == book.CategoryId)?.Name ?? string.Empty,
                    AuthorId = book.AuthorId,
                    AuthorName = authors.FirstOrDefault(a => a.Id == book.AuthorId)?.Name ?? string.Empty,
                    PublisherId = book.PublisherId,
                    PublisherName = publishers.FirstOrDefault(p => p.Id == book.PublisherId)?.PublisherName ?? string.Empty,
                    SupplierId = book.SupplierId,
                    Edition = book.Edition,
                    NoOfCopies = book.NoOfCopies,
                    StockInHand = book.StockInHand,
                    PublishingDate = book.PublishingDate,
                    ISBNNumber = book.ISBNNumber,
                    Price = book.Price,
                    TotalPages = book.TotalPages,
                    IsIssuable = book.IsIssuable,
                    CallNumber = book.CallNumber,
                    AccessionNumber = book.AccessionNumber,
                    IsActive = book.IsActive,
                    IsDeleted = book.IsDeleted,
                    CompanyId = book.CompanyId,
                    SchoolId = book.SchoolId,
                    CreatedBy = book.CreatedBy,
                    CreatedDate = book.CreatedDate,
                    ModifiedBy = book.ModifiedBy,
                    ModifiedDate = book.ModifiedDate,
                    Status = book.Status,
                    StatusMessage = book.StatusMessage
                }).ToList();

                return View(bookViewModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving books");
                TempData["ErrorMessage"] = "An error occurred while retrieving books.";
                return View(new List<BookMasterViewModel>());
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var book = await _bookMasterService.GetByIdAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                var categories = _bookCategoryService.GetAll() ?? new List<BookCategoryMaster>();
                var authors = _authorService.GetAll() ?? new List<AuthorMaster>();
                var publishers = _publisherService.GetAll() ?? new List<PublisherMaster>();

                var bookViewModel = new BookMasterViewModel
                {
                    Id = book.Id,
                    Code = book.Code,
                    Title = book.Title,
                    Description = book.Description,
                    Image = book.Image,
                    TypeId = book.TypeId,
                    CategoryId = book.CategoryId,
                    CategoryName = categories.FirstOrDefault(c => c.Id == book.CategoryId)?.Name ?? string.Empty,
                    AuthorId = book.AuthorId,
                    AuthorName = authors.FirstOrDefault(a => a.Id == book.AuthorId)?.Name ?? string.Empty,
                    PublisherId = book.PublisherId,
                    PublisherName = publishers.FirstOrDefault(p => p.Id == book.PublisherId)?.PublisherName ?? string.Empty,
                    SupplierId = book.SupplierId,
                    Edition = book.Edition,
                    NoOfCopies = book.NoOfCopies,
                    StockInHand = book.StockInHand,
                    PublishingDate = book.PublishingDate,
                    ISBNNumber = book.ISBNNumber,
                    Price = book.Price,
                    TotalPages = book.TotalPages,
                    IsIssuable = book.IsIssuable,
                    CallNumber = book.CallNumber,
                    AccessionNumber = book.AccessionNumber,
                    IsActive = book.IsActive,
                    IsDeleted = book.IsDeleted,
                    CompanyId = book.CompanyId,
                    SchoolId = book.SchoolId,
                    CreatedBy = book.CreatedBy,
                    CreatedDate = book.CreatedDate,
                    ModifiedBy = book.ModifiedBy,
                    ModifiedDate = book.ModifiedDate,
                    Status = book.Status,
                    StatusMessage = book.StatusMessage
                };

                return View(bookViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving book details");
                TempData["ErrorMessage"] = "An error occurred while retrieving book details.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookMaster book)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(book);
            }

            try
            {
                book.Id = Guid.NewGuid();
                book.IsActive = true;
                book.CreatedDate = DateTime.UtcNow;
                
                await _bookMasterService.CreateAsync(book);
                TempData["SuccessMessage"] = "Book created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating book");
                ModelState.AddModelError(string.Empty, "Failed to create book.");
                PopulateDropdowns();
                return View(book);
            }
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var book = await _bookMasterService.GetByIdAsync(id);
                if (book == null)
                {
                    return NotFound();
                }
                PopulateDropdowns();
                return View(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving book for edit");
                TempData["ErrorMessage"] = "An error occurred while retrieving book.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BookMaster book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(book);
            }

            try
            {
                book.ModifiedDate = DateTime.UtcNow;
                await _bookMasterService.UpdateAsync(book);
                TempData["SuccessMessage"] = "Book updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating book");
                ModelState.AddModelError(string.Empty, "Failed to update book.");
                PopulateDropdowns();
                return View(book);
            }
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var book = await _bookMasterService.GetByIdAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                await _bookMasterService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Book deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting book");
                TempData["ErrorMessage"] = "An error occurred while deleting book.";
                return RedirectToAction(nameof(Index));
            }
        }

        private void PopulateDropdowns()
        {
            try
            {
                ViewBag.Categories = _bookCategoryService.GetAll()?.Select(c => new { Value = c.Id, Text = c.Name }) ?? Enumerable.Empty<object>();
                ViewBag.Authors = _authorService.GetAll()?.Select(a => new { Value = a.Id, Text = a.Name }) ?? Enumerable.Empty<object>();
                ViewBag.Publishers = _publisherService.GetAll()?.Select(p => new { Value = p.Id, Text = p.PublisherName }) ?? Enumerable.Empty<object>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error populating dropdowns");
                ViewBag.Categories = new List<object>();
                ViewBag.Authors = new List<object>();
                ViewBag.Publishers = new List<object>();
            }
        }
    }
}
