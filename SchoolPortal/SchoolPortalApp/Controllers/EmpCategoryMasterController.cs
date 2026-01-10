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
    [Route("EmpCategoryMaster")]
    public class EmpCategoryMasterController : BaseController
    {
        private readonly IEmpCategoryMasterService _empCategoryMasterService;
        private new readonly ILogger<EmpCategoryMasterController> _logger;

        public EmpCategoryMasterController(
            IEmpCategoryMasterService empCategoryMasterService,
            ILogger<EmpCategoryMasterController> logger) : base(logger)
        {
            _empCategoryMasterService = empCategoryMasterService ?? throw new ArgumentNullException(nameof(empCategoryMasterService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            try
            {
                var categories = _empCategoryMasterService.GetAll() ?? new List<EmpCategoryMaster>();
                return View(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving employee categories");
                TempData["ErrorMessage"] = "An error occurred while retrieving employee categories.";
                return View(new List<EmpCategoryMaster>());
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var category = await _empCategoryMasterService.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving employee category details");
                TempData["ErrorMessage"] = "An error occurred while retrieving employee category details.";
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
        public async Task<IActionResult> Create(EmpCategoryMaster category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            try
            {
                category.Id = Guid.NewGuid();
                category.IsActive = true;
                category.CreatedDate = DateTime.UtcNow;
                
                await _empCategoryMasterService.CreateAsync(category);
                TempData["SuccessMessage"] = "Employee category created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating employee category");
                ModelState.AddModelError(string.Empty, "Failed to create employee category.");
                return View(category);
            }
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var category = await _empCategoryMasterService.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving employee category for edit");
                TempData["ErrorMessage"] = "An error occurred while retrieving employee category.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EmpCategoryMaster category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            try
            {
                category.ModifiedDate = DateTime.UtcNow;
                await _empCategoryMasterService.UpdateAsync(category);
                TempData["SuccessMessage"] = "Employee category updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating employee category");
                ModelState.AddModelError(string.Empty, "Failed to update employee category.");
                return View(category);
            }
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var category = await _empCategoryMasterService.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                await _empCategoryMasterService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Employee category deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting employee category");
                TempData["ErrorMessage"] = "An error occurred while deleting employee category.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
