using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;

namespace SchoolPortalApp.Controllers
{
    [Route("Visitor")]
    public class VisitorController : Controller
    {
        private readonly IVisitorService _service;
        private readonly ILookupService _lookup;
        private readonly ILogger<VisitorController> _logger;

        public VisitorController(IVisitorService service, ILookupService lookup, ILogger<VisitorController> logger)
        {
            _service = service;
            _lookup = lookup;
            _logger = logger;
        }

        private void PopulateDropdowns(VisitorViewModel vm)
        {
            var countries = _lookup.GetCountries();
            vm.Countries = countries.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == vm.CountryId }).ToList();

            if (vm.CountryId != Guid.Empty)
            {
                var states = _lookup.GetStates(vm.CountryId);
                vm.States = states.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.StateId }).ToList();
            }
            else
            {
                vm.States = Array.Empty<SelectListItem>();
            }

            if (vm.StateId != Guid.Empty)
            {
                var cities = _lookup.GetCities(vm.StateId);
                vm.Cities = cities.Select(ci => new SelectListItem { Value = ci.Id.ToString(), Text = ci.Name, Selected = ci.Id == vm.CityId }).ToList();
            }
            else
            {
                vm.Cities = Array.Empty<SelectListItem>();
            }

            vm.Companies = _lookup.GetCompanies().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == (vm.CompanyId ?? Guid.Empty) }).ToList();
            vm.Schools = _lookup.GetSchools().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == (vm.SchoolId ?? Guid.Empty) }).ToList();
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var list = _service.GetAll();
            var countries = _lookup.GetCountries();
            var result = list.Select(item =>
            {
                var country = countries.FirstOrDefault(c => c.Id == item.CountryId);
                var states = _lookup.GetStates(item.CountryId);
                var state = states.FirstOrDefault(s => s.Id == item.StateId);
                var cities = _lookup.GetCities(item.StateId);
                var city = cities.FirstOrDefault(ci => ci.Id == item.CityId);

                return new VisitorListItemViewModel
                {
                    Id = item.Id,
                    VehicleNumber = item.VehicleNumber ?? string.Empty,
                    VehicleName = item.VehicleName ?? string.Empty,
                    DateOfEntry = item.DateOfEntry,
                    Purpose = item.Purpose ?? string.Empty,
                    ContactPerson = item.ContactPerson ?? string.Empty,
                    CountryName = country?.Name ?? string.Empty,
                    StateName = state?.Name ?? string.Empty,
                    CityName = city?.Name ?? string.Empty,
                    IsActive = item.IsActive
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

            var countries = _lookup.GetCountries();
            var country = countries.FirstOrDefault(c => c.Id == item.CountryId);
            var states = _lookup.GetStates(item.CountryId);
            var state = states.FirstOrDefault(s => s.Id == item.StateId);
            var cities = _lookup.GetCities(item.StateId);
            var city = cities.FirstOrDefault(ci => ci.Id == item.CityId);

            var vm = new VisitorDetailsViewModel
            {
                Id = item.Id,
                VehicleNumber = item.VehicleNumber ?? string.Empty,
                VehicleName = item.VehicleName ?? string.Empty,
                DateOfEntry = item.DateOfEntry,
                ArrivalTime = item.ArrivalTime,
                ExitTime = item.ExitTime,
                Purpose = item.Purpose ?? string.Empty,
                ContactPerson = item.ContactPerson ?? string.Empty,
                Address1 = item.Address1 ?? string.Empty,
                Address2 = item.Address2 ?? string.Empty,
                CountryName = country?.Name ?? string.Empty,
                StateName = state?.Name ?? string.Empty,
                CityName = city?.Name ?? string.Empty,
                ZipCode = item.ZipCode ?? string.Empty,
                CompanyName = item.CompanyId.HasValue ? _lookup.GetCompanies().FirstOrDefault(x => x.Id == item.CompanyId.Value)?.Name ?? string.Empty : string.Empty,
                SchoolName = item.SchoolId.HasValue ? _lookup.GetSchools().FirstOrDefault(x => x.Id == item.SchoolId.Value)?.Name ?? string.Empty : string.Empty,
                IsActive = item.IsActive
            };
            return View(vm);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var vm = new VisitorViewModel();
            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VisitorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model);
                return View(model);
            }

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
            {
                ModelState.AddModelError(string.Empty, "Please login to create visitor entry.");
                PopulateDropdowns(model);
                return View(model);
            }

            var entity = new VisitorMaster
            {
                Id = Guid.Empty,
                VehicleNumber = model.VehicleNumber,
                VehicleName = model.VehicleName ?? string.Empty,
                DateOfEntry = model.DateOfEntry,
                ArrivalTime = model.ArrivalTime,
                ExitTime = model.ExitTime,
                Purpose = model.Purpose ?? string.Empty,
                ContactPerson = model.ContactPerson ?? string.Empty,
                Address1 = model.Address1 ?? string.Empty,
                Address2 = model.Address2 ?? string.Empty,
                CityId = model.CityId,
                StateId = model.StateId,
                CountryId = model.CountryId,
                ZipCode = model.ZipCode ?? string.Empty,
                CompanyId = model.CompanyId,
                SchoolId = model.SchoolId,
                IsActive = model.IsActive,
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow
            };

            var newId = _service.Create(entity);
            if (newId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Failed to create visitor entry.");
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

            var vm = new VisitorViewModel
            {
                Id = item.Id,
                VehicleNumber = item.VehicleNumber,
                VehicleName = item.VehicleName,
                DateOfEntry = item.DateOfEntry,
                ArrivalTime = item.ArrivalTime,
                ExitTime = item.ExitTime,
                Purpose = item.Purpose,
                ContactPerson = item.ContactPerson,
                Address1 = item.Address1,
                Address2 = item.Address2,
                CityId = item.CityId,
                StateId = item.StateId,
                CountryId = item.CountryId,
                ZipCode = item.ZipCode,
                CompanyId = item.CompanyId,
                SchoolId = item.SchoolId,
                IsActive = item.IsActive
            };
            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, VisitorViewModel model)
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
                ModelState.AddModelError(string.Empty, "Please login to update visitor entry.");
                PopulateDropdowns(model);
                return View(model);
            }

            var entity = new VisitorMaster
            {
                Id = id,
                VehicleNumber = model.VehicleNumber ?? string.Empty,
                VehicleName = model.VehicleName ?? string.Empty,
                DateOfEntry = model.DateOfEntry,
                ArrivalTime = model.ArrivalTime,
                ExitTime = model.ExitTime,
                Purpose = model.Purpose ?? string.Empty,
                ContactPerson = model.ContactPerson ?? string.Empty,
                Address1 = model.Address1 ?? string.Empty,
                Address2 = model.Address2 ?? string.Empty,
                CityId = model.CityId,
                StateId = model.StateId,
                CountryId = model.CountryId,
                ZipCode = model.ZipCode ?? string.Empty,
                CompanyId = model.CompanyId,
                SchoolId = model.SchoolId,
                IsActive = model.IsActive,
                ModifiedBy = userId,
                ModifiedDate = DateTime.UtcNow
            };

            if (!_service.Update(entity))
            {
                ModelState.AddModelError(string.Empty, "Failed to update visitor entry.");
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
                TempData["ErrorMessage"] = "Failed to delete visitor entry.";
                return RedirectToAction("Delete", new { id });
            }
            return RedirectToAction("Index");
        }

        // JSON endpoints for cascading dropdowns
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