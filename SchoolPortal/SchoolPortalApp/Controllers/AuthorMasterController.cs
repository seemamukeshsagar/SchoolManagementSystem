using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortalApp.Controllers
{
    [Route("AuthorMaster")]
    public class AuthorMasterController : BaseController
    {
        private readonly IAuthorMasterService _authorMasterService;
        private new readonly ILogger<AuthorMasterController> _logger;

        public AuthorMasterController(
            IAuthorMasterService authorMasterService,
            ILogger<AuthorMasterController> logger) : base(logger)
        {
            _authorMasterService = authorMasterService ?? throw new ArgumentNullException(nameof(authorMasterService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            try
            {
                var authors = _authorMasterService.GetAll() ?? new List<AuthorMaster>();
                return View(authors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving authors");
                TempData["ErrorMessage"] = "An error occurred while retrieving authors.";
                return View(new List<AuthorMaster>());
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var author = await _authorMasterService.GetByIdAsync(id);
                if (author == null)
                {
                    return NotFound();
                }
                return View(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving author details");
                TempData["ErrorMessage"] = "An error occurred while retrieving author details.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorMaster author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            try
            {
                author.Id = Guid.NewGuid();
                author.IsActive = true;
                author.CreatedDate = DateTime.UtcNow;
                
                await _authorMasterService.CreateAsync(author);
                TempData["SuccessMessage"] = "Author created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating author");
                ModelState.AddModelError(string.Empty, "Failed to create author.");
                return View(author);
            }
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var author = await _authorMasterService.GetByIdAsync(id);
                if (author == null)
                {
                    return NotFound();
                }
                return View(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving author for edit");
                TempData["ErrorMessage"] = "An error occurred while retrieving author.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AuthorMaster author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(author);
            }

            try
            {
                author.ModifiedDate = DateTime.UtcNow;
                await _authorMasterService.UpdateAsync(author);
                TempData["SuccessMessage"] = "Author updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating author");
                ModelState.AddModelError(string.Empty, "Failed to update author.");
                return View(author);
            }
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var author = await _authorMasterService.GetByIdAsync(id);
                if (author == null)
                {
                    return NotFound();
                }

                await _authorMasterService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Author deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting author");
                TempData["ErrorMessage"] = "An error occurred while deleting author.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
