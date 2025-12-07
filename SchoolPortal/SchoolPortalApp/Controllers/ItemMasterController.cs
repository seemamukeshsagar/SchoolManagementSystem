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
    [Route("ItemMaster")]
    public class ItemMasterController : BaseController
    {
        private readonly IItemService _service;
        private readonly IItemTypeService _itemTypeService;
        private new readonly ILogger<ItemMasterController> _logger;

        public ItemMasterController(IItemService service, IItemTypeService itemTypeService, ILogger<ItemMasterController> logger)
        {
            _service = service;
            _itemTypeService = itemTypeService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private void PopulateDropdowns(ItemMasterViewModel vm)
        {
            var types = _itemTypeService.GetAll();
            vm.ItemTypes = types
                .Select(t => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name ?? string.Empty,
                    Selected = t.Id == vm.ItemTypeMasterId
                })
                .ToList();
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var list = _service.GetAll();
            var types = _itemTypeService.GetAll();
            var result = list.Select(item =>
            {
                var type = types.FirstOrDefault(t => t.Id == item.ItemTypeMasterId);
                return new ItemMasterListItemViewModel
                {
                    Id = item.Id,
                    ItemName = item.ItemName ?? string.Empty,
                    ItemTypeName = type?.Name ?? string.Empty,
                    IsActive = item.IsActive ?? false
                };
            }).ToList();
            return View(result);
        }

        [HttpGet]
        [Route("Details/{id}")]
        public IActionResult Details(Guid id)
        {
            var item = _service.GetById(id);
            if (item == null) return NotFound();

            var type = _itemTypeService.GetById(item.ItemTypeMasterId);
            var vm = new ItemMasterDetailsViewModel
            {
                Id = item.Id,
                ItemName = item.ItemName ?? string.Empty,
                Description = item.Description ?? string.Empty,
                ItemTypeName = type?.Name ?? string.Empty,
                IsActive = item.IsActive ?? false
            };
            return View(vm);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var vm = new ItemMasterViewModel();
            var companyId = CurrentCompanyId;
            var schoolId = CurrentSchoolId;
            if (companyId.HasValue) vm.CompanyId = companyId.Value;
            if (schoolId.HasValue) vm.SchoolId = schoolId.Value;
            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ItemMasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model);
                return View(model);
            }

            var userId = CurrentUserId;
            var companyId = CurrentCompanyId;
            var schoolId = CurrentSchoolId;
            if (!userId.HasValue || !companyId.HasValue || !schoolId.HasValue)
            {
                ModelState.AddModelError(string.Empty, "Company/School information not found. Please login again.");
                PopulateDropdowns(model);
                return View(model);
            }

            var entity = new ItemMaster
            {
                Id = Guid.Empty,
                ItemName = model.ItemName,
                Description = model.Description ?? string.Empty,
                ItemTypeMasterId = model.ItemTypeMasterId,
                IsActive = model.IsActive,
                CompanyId = companyId.Value,
                SchoolId = schoolId.Value,
                CreatedBy = userId.Value,
                CreatedDate = DateTime.UtcNow
            };

            var newId = _service.Create(entity);
            if (newId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Failed to create item.");
                PopulateDropdowns(model);
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

            var vm = new ItemMasterViewModel
            {
                Id = item.Id,
                ItemName = item.ItemName ?? string.Empty,
                Description = item.Description ?? string.Empty,
                ItemTypeMasterId = item.ItemTypeMasterId,
                CompanyId = item.CompanyId,
                SchoolId = item.SchoolId,
                IsActive = item.IsActive ?? false
            };
            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, ItemMasterViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model);
                return View(model);
            }

            var userId = CurrentUserId;
            var companyId = CurrentCompanyId;
            var schoolId = CurrentSchoolId;
            if (!userId.HasValue || !companyId.HasValue || !schoolId.HasValue)
            {
                ModelState.AddModelError(string.Empty, "Company/School information not found. Please login again.");
                PopulateDropdowns(model);
                return View(model);
            }

            var entity = new ItemMaster
            {
                Id = id,
                ItemName = model.ItemName,
                Description = model.Description ?? string.Empty,
                ItemTypeMasterId = model.ItemTypeMasterId,
                IsActive = model.IsActive,
                CompanyId = companyId.Value,
                SchoolId = schoolId.Value,
                ModifiedBy = userId.Value,
                ModifiedDate = DateTime.UtcNow
            };

            if (!_service.Update(entity))
            {
                ModelState.AddModelError(string.Empty, "Failed to update item.");
                PopulateDropdowns(model);
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
                TempData["ErrorMessage"] = "Failed to delete item.";
                return RedirectToAction("Delete", new { id });
            }
            return RedirectToAction("Index");
        }
    }
}