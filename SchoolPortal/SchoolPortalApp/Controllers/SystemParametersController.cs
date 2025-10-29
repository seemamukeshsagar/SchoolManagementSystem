using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using SchoolPortalApp.Models;

namespace SchoolPortalApp.Controllers
{
    [Route("SystemParameters")]
    public class SystemParametersController : Controller
    {
        private readonly ISystemParametersService _service;
        private readonly ILookupService _lookup;
        private readonly ILogger<SystemParametersController> _logger;

        public SystemParametersController(ISystemParametersService service, ILookupService lookup, ILogger<SystemParametersController> logger)
        {
            _service = service;
            _lookup = lookup;
            _logger = logger;
        }

        //private void PopulateDropdowns(SystemParameterViewModel vm)
        //{
        //    var companies = _lookup.GetCompanies();
        //    vm.Companies = companies.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == vm.CompanyId }).ToList();
        //    var schools = _lookup.GetSchools();
        //    vm.Schools = schools.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.SchoolId }).ToList();
        //}

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var list = _service.GetAll();
            var companies = _lookup.GetCompanies();
            var schools = _lookup.GetSchools();
            var result = list.Select(item => new SystemParameterListItemViewModel
            {
                Id = item.Id,
                ParameterName = item.ParameterName,
                ParameterValue = item.ParameterValue,
                IsActive = item.IsActive,
                CompanyName = companies.FirstOrDefault(c => c.Id == item.CompanyId)?.Name ?? string.Empty,
                SchoolName = schools.FirstOrDefault(s => s.Id == item.SchoolId)?.Name ?? string.Empty,
            }).ToList();
            return View(result);
        }

        [HttpGet]
        [Route("Details/{id}")]
        public IActionResult Details(Guid id)
        {
            var item = _service.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var vm = new SystemParameterViewModel();
            //PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SystemParameterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //PopulateDropdowns(model);
                return View(model);
            }
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
            {
                ModelState.AddModelError(string.Empty, "Please login to create system parameter.");
                //PopulateDropdowns(model);
                return View(model);
            }

            var entity = new SystemParameters
            {
                Id = Guid.Empty,
                ParameterName = model.ParameterName,
                ParameterValue = model.ParameterValue ?? string.Empty,
                Description = model.Description ?? string.Empty,
                CompanyId = model.CompanyId,
                SchoolId = model.SchoolId,
                IsActive = model.IsActive,
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow,
            };

            var newId = _service.Create(entity);
            if (newId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Failed to create system parameter.");
                //PopulateDropdowns(model);
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
            var vm = new SystemParameterViewModel
            {
                Id = item.Id,
                ParameterName = item.ParameterName,
                ParameterValue = item.ParameterValue,
                Description = item.Description,
                CompanyId = item.CompanyId,
                SchoolId = item.SchoolId,
                IsActive = item.IsActive,
            };
            //PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, SystemParameterViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                //PopulateDropdowns(model);
                return View(model);
            }

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
            {
                ModelState.AddModelError(string.Empty, "Please login to update system parameter.");
                //PopulateDropdowns(model);
                return View(model);
            }

            var entity = new SystemParameters
            {
                Id = id,
                ParameterName = model.ParameterName,
                ParameterValue = model.ParameterValue ?? string.Empty,
                Description = model.Description ?? string.Empty,
                CompanyId = model.CompanyId,
                SchoolId = model.SchoolId,
                IsActive = model.IsActive,
                ModifiedBy = userId,
                ModifiedDate = DateTime.UtcNow,
            };

            if (!_service.Update(entity))
            {
                ModelState.AddModelError(string.Empty, "Failed to update system parameter.");
                //PopulateDropdowns(model);
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
                TempData["ErrorMessage"] = "Failed to delete system parameter.";
                return RedirectToAction("Delete", new { id });
            }
            return RedirectToAction("Index");
        }
    }
}
