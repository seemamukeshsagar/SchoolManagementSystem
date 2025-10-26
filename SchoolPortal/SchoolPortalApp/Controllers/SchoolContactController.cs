using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Schoolortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortalApp.Controllers
{
    [Route("SchoolContact")]
    public class SchoolContactController : Controller
    {
        private readonly ISchoolContactService _service;
        private readonly ILookupService _lookup;
        private readonly ISchoolService _schoolService;
        private readonly ILogger<SchoolContactController> _logger;

        public SchoolContactController(ISchoolContactService service, ILookupService lookup, ISchoolService schoolService, ILogger<SchoolContactController> logger)
        {
            _service = service;
            _lookup = lookup;
            _schoolService = schoolService;
            _logger = logger;
        }

        private void PopulateDropdowns(SchoolContactViewModel vm)
        {
            var schools = _schoolService.GetAll();
            vm.Schools = schools.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.SchoolId }).ToList();

            var countries = _lookup.GetCountries();
            vm.Countries = countries.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == vm.CountryId }).ToList();

            if (vm.CountryId != Guid.Empty)
            {
                var states = _lookup.GetStates(vm.CountryId);
                vm.States = states.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.StateId }).ToList();
            }
            else
            {
                vm.States = Array.Empty<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }

            if (vm.StateId != Guid.Empty)
            {
                var cities = _lookup.GetCities(vm.StateId);
                vm.Cities = cities.Select(ci => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = ci.Id.ToString(), Text = ci.Name, Selected = ci.Id == vm.CityId }).ToList();
            }
            else
            {
                vm.Cities = Array.Empty<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var list = _service.GetAll();
            return View(list);
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
            var vm = new SchoolContactViewModel();
            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SchoolContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model);
                return View(model);
            }
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
            {
                ModelState.AddModelError(string.Empty, "Please login to create school contact.");
                PopulateDropdowns(model);
                return View(model);
            }

            var entity = new SchoolContactMaster
            {
                Id = Guid.Empty,
                SchoolId = model.SchoolId,
                FirstName = model.FirstName,
                LastName = model.LastName ?? string.Empty,
                Email = model.Email ?? string.Empty,
                Phone = model.Phone ?? string.Empty,
                MobilePhone = model.MobilePhone ?? string.Empty,
                AddressLine1 = model.AddressLine1 ?? string.Empty,
                AddressLine2 = model.AddressLine2 ?? string.Empty,
                CityId = model.CityId,
                StateId = model.StateId,
                CountryId = model.CountryId,
                IsActive = model.IsActive,
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow
            };

            var newId = _service.Create(entity);
            if (newId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Failed to create school contact.");
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
            var vm = new SchoolContactViewModel
            {
                Id = item.Id,
                SchoolId = item.SchoolId,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                Phone = item.Phone,
                MobilePhone = item.MobilePhone,
                AddressLine1 = item.AddressLine1,
                AddressLine2 = item.AddressLine2,
                CityId = item.CityId,
                StateId = item.StateId,
                CountryId = item.CountryId,
                IsActive = item.IsActive
            };
            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, SchoolContactViewModel model)
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
                ModelState.AddModelError(string.Empty, "Please login to update school contact.");
                PopulateDropdowns(model);
                return View(model);
            }

            var entity = new SchoolContactMaster
            {
                Id = id,
                SchoolId = model.SchoolId,
                FirstName = model.FirstName,
                LastName = model.LastName ?? string.Empty,
                Email = model.Email ?? string.Empty,
                Phone = model.Phone ?? string.Empty,
                MobilePhone = model.MobilePhone ?? string.Empty,
                AddressLine1 = model.AddressLine1 ?? string.Empty,
                AddressLine2 = model.AddressLine2 ?? string.Empty,
                CityId = model.CityId,
                StateId = model.StateId,
                CountryId = model.CountryId,
                IsActive = model.IsActive,
                ModifiedBy = userId,
                ModifiedDate = DateTime.UtcNow
            };

            if (!_service.Update(entity))
            {
                ModelState.AddModelError(string.Empty, "Failed to update school contact.");
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
                TempData["ErrorMessage"] = "Failed to delete school contact.";
                return RedirectToAction("Delete", new { id });
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("GetStates")]
        public IActionResult GetStates(Guid countryId)
        {
            var list = _lookup.GetStates(countryId).Select(s => new { id = s.Id, name = s.Name });
            return Ok(list);
        }

        [HttpGet]
        [Route("GetCities")]
        public IActionResult GetCities(Guid stateId)
        {
            var list = _lookup.GetCities(stateId).Select(c => new { id = c.Id, name = c.Name });
            return Ok(list);
        }
    }
}
