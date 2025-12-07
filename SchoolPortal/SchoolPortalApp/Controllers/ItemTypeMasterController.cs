using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortalApp.Controllers
{
    [Route("ItemType")]
    public class ItemTypeMasterController : BaseController
    {
        private readonly IItemTypeService _service;
        private new readonly ILogger<ItemTypeMasterController> _logger;

        public ItemTypeMasterController(IItemTypeService service, ILogger<ItemTypeMasterController> logger)
        {
            _service = service;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var list = _service.GetAll();
            var result = list.Select(item => new ItemTypeListItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                IsActive = item.IsActive ?? false,
            }).ToList();
            return View(result);
        }

        [HttpGet]
        [Route("Details/{id}")]
        public IActionResult Details(Guid id)
        {
            var item = _service.GetById(id);
            if (item == null) return NotFound();

            var vm = new ItemTypeDetailsViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description ?? string.Empty,
                IsActive = item.IsActive ?? false,
            };
            return View(vm);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var vm = new ItemTypeViewModel();
            return View(vm);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ItemTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = CurrentUserId;
            var companyId = CurrentCompanyId;
            var schoolId = CurrentSchoolId;
            if (!userId.HasValue || !companyId.HasValue || !schoolId.HasValue)
            {
                ModelState.AddModelError(string.Empty, "Company/School information not found. Please login again.");
                return View(model);
            }

            var entity = new ItemTypeMaster
            {
                Id = Guid.Empty,
                Name = model.Name,
                Description = model.Description ?? string.Empty,
                IsActive = model.IsActive,
                CompanyId = companyId.Value,
                SchoolId = schoolId.Value,
                CreatedBy = userId.Value,
                CreatedDate = DateTime.UtcNow,
            };

            var newId = _service.Create(entity);
            if (newId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Failed to create item type.");
                return View(model);
            }
            return RedirectToAction("Details", new { id = newId });
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            var item = _service.GetById(id);
            if (item == null) return NotFound();

            var vm = new ItemTypeViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                IsActive = item.IsActive ?? false,
            };
            return View(vm);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, ItemTypeViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = CurrentUserId;
            var companyId = CurrentCompanyId;
            var schoolId = CurrentSchoolId;
            if (!userId.HasValue || !companyId.HasValue || !schoolId.HasValue)
            {
                ModelState.AddModelError(string.Empty, "Company/School information not found. Please login again.");
                return View(model);
            }

            var entity = new ItemTypeMaster
            {
                Id = id,
                Name = model.Name,
                Description = model.Description ?? string.Empty,
                IsActive = model.IsActive,
                CompanyId = companyId.Value,
                SchoolId = schoolId.Value,
                ModifiedBy = userId.Value,
                ModifiedDate = DateTime.UtcNow,
            };

            if (!_service.Update(entity))
            {
                ModelState.AddModelError(string.Empty, "Failed to update item type.");
                return View(model);
            }
            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            var item = _service.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(Guid id)
        {
            if (!_service.Delete(id))
            {
                TempData["ErrorMessage"] = "Failed to delete item type.";
                return RedirectToAction("Delete", new { id });
            }
            return RedirectToAction("Index");
        }
    }
}