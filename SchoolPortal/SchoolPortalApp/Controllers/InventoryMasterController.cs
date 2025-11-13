using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortalApp.Controllers
{
    [Route("InventoryMaster")]
    public class InventoryMasterController : Controller
    {
        private readonly IInventoryService _service;
        private readonly IItemService _itemService;
        private readonly ILookupService _lookup;
        private readonly ILogger<InventoryMasterController> _logger;

        public InventoryMasterController(
            IInventoryService service,
            IItemService itemService,
            ILookupService lookup,
            ILogger<InventoryMasterController> logger)
        {
            _service = service;
            _itemService = itemService;
            _lookup = lookup;
            _logger = logger;
        }

        private void PopulateDropdowns(InventoryMasterViewModel vm)
        {
            var items = _itemService.GetAll();
            vm.Items = items
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.ItemName ?? string.Empty,
                    Selected = i.Id == vm.ItemId
                })
                .ToList();

            var locations = _lookup.GetLocations();
            vm.Locations = locations
                .Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.Name ?? string.Empty,
                    Selected = l.Id == vm.LocationId
                })
                .ToList();
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var list = _service.GetAll();
            var items = _itemService.GetAll();
            var locs = _lookup.GetLocations().ToList();

            var vm = list.Select(e =>
            {
                var item = items.FirstOrDefault(x => x.Id == e.ItemId);
                var loc = locs.FirstOrDefault(x => x.Id == e.LocationId);
                return new InventoryMasterListItemViewModel
                {
                    Id = e.Id,
                    Name = e.Name ?? string.Empty,
                    ItemName = item?.ItemName ?? string.Empty,
                    LocationName = loc?.Name ?? string.Empty,
                    Quantity = e.Quantity ?? 0,
                    IsActive = e.IsActive ?? false
                };
            }).ToList();
            return View(vm);
        }

        [HttpGet]
        [Route("Details/{id}")]
        public IActionResult Details(Guid id)
        {
            var e = _service.GetById(id);
            if (e == null) return NotFound();

            var item = _itemService.GetById(e.ItemId);
            var loc = _lookup.GetLocations().FirstOrDefault(x => x.Id == e.LocationId);

            var vm = new InventoryMasterDetailsViewModel
            {
                Id = e.Id,
                Name = e.Name ?? string.Empty,
                ItemName = item?.ItemName ?? string.Empty,
                LocationName = loc?.Name ?? string.Empty,
                Quantity = e.Quantity ?? 0,
                CostPerItem = e.CostPerItem ?? 0m,
                IsActive = e.IsActive ?? false
            };
            return View(vm);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var vm = new InventoryMasterViewModel();
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
        public IActionResult Create(InventoryMasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model);
                return View(model);
            }

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
            {
                ModelState.AddModelError(string.Empty, "Please login to create inventory.");
                PopulateDropdowns(model);
                return View(model);
            }

            if (model.CompanyId == Guid.Empty || model.SchoolId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Company/School information not found in session.");
                PopulateDropdowns(model);
                return View(model);
            }

            var entity = new InventoryMaster
            {
                Id = Guid.Empty,
                Name = model.Name,
                ItemId = model.ItemId,
                LocationId = model.LocationId,
                Quantity = model.Quantity,
                CostPerItem = model.CostPerItem,
                IsActive = model.IsActive,
                CompanyId = model.CompanyId,
                SchoolId = model.SchoolId,
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow
            };

            var newId = _service.Create(entity);
            if (newId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Failed to create inventory.");
                PopulateDropdowns(model);
                return View(model);
            }
            return RedirectToAction("Details", new { id = newId });
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            var e = _service.GetById(id);
            if (e == null) return NotFound();

            var vm = new InventoryMasterViewModel
            {
                Id = e.Id,
                Name = e.Name ?? string.Empty,
                ItemId = e.ItemId,
                LocationId = e.LocationId,
                Quantity = e.Quantity ?? 0,
                CostPerItem = e.CostPerItem ?? 0m,
                IsActive = e.IsActive ?? false,
                CompanyId = e.CompanyId,
                SchoolId = e.SchoolId
            };
            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, InventoryMasterViewModel model)
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
                ModelState.AddModelError(string.Empty, "Please login to update inventory.");
                PopulateDropdowns(model);
                return View(model);
            }

            var entity = new InventoryMaster
            {
                Id = id,
                Name = model.Name,
                ItemId = model.ItemId,
                LocationId = model.LocationId,
                Quantity = model.Quantity,
                CostPerItem = model.CostPerItem,
                IsActive = model.IsActive,
                CompanyId = model.CompanyId,
                SchoolId = model.SchoolId,
                ModifiedBy = userId,
                ModifiedDate = DateTime.UtcNow
            };

            if (!_service.Update(entity))
            {
                ModelState.AddModelError(string.Empty, "Failed to update inventory.");
                PopulateDropdowns(model);
                return View(model);
            }
            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            var e = _service.GetById(id);
            if (e == null) return NotFound();
            return View(e);
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(Guid id)
        {
            if (!_service.Delete(id))
            {
                TempData["ErrorMessage"] = "Failed to delete inventory.";
                return RedirectToAction("Delete", new { id });
            }
            return RedirectToAction("Index");
        }
    }
}