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
    public class ItemMasterController : Controller
    {
        private readonly IItemService _service;
        private readonly IItemTypeService _itemTypeService;
        private readonly ILogger<ItemMasterController> _logger;

        public ItemMasterController(IItemService service, IItemTypeService itemTypeService, ILogger<ItemMasterController> logger)
        {
            _service = service;
            _itemTypeService = itemTypeService;
            _logger = logger;
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
            var companyIdStr = HttpContext.Session.GetString("CompanyId");
            var schoolIdStr = HttpContext.Session.GetString("SchoolId");
            if (!string.IsNullOrWhiteSpace(companyIdStr) && Guid.TryParse(companyIdStr, out var companyId)) vm.CompanyId = companyId;
            if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolId)) vm.SchoolId = schoolId;
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

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
            {
                ModelState.AddModelError(string.Empty, "Please login to create item.");
                PopulateDropdowns(model);
                return View(model);
            }

            if (model.CompanyId == Guid.Empty || model.SchoolId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Company/School information not found in session.");
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
                CompanyId = model.CompanyId,
                SchoolId = model.SchoolId,
                CreatedBy = userId,
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

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
            {
                ModelState.AddModelError(string.Empty, "Please login to update item.");
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
                CompanyId = model.CompanyId,
                SchoolId = model.SchoolId,
                ModifiedBy = userId,
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