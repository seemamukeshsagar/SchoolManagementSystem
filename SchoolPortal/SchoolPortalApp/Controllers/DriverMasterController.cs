using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using SchoolPortalApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Controllers
{
    [Route("DriverMaster")]
    public class DriverMasterController : Controller
    {
        private readonly IDriverMasterService _service;
        private readonly ISchoolService _schoolService;
        private readonly ILookupService _lookup;
        private readonly ILogger<DriverMasterController> _logger;

        public DriverMasterController(IDriverMasterService service, ISchoolService schoolService, ILookupService lookup, ILogger<DriverMasterController> logger)
        {
            _service = service;
            _schoolService = schoolService;
            _lookup = lookup;
            _logger = logger;
        }

        private void PopulateQualifications(Guid selectedId)
        {
            var list = _lookup.GetQualifications() ?? new System.Collections.Generic.List<LookupItem>();
            ViewBag.Qualifications = list.Select(q => new SelectListItem
            {
                Value = q.Id.ToString(),
                Text = q.Name,
                Selected = q.Id == selectedId
            }).ToList();
        }

        private void PopulateLocationLists(DriverMaster model)
        {
            var countries = _lookup.GetCountries() ?? new System.Collections.Generic.List<LookupItem>();
            ViewBag.Countries = countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = c.Id == model.CountryId
            }).ToList();

            var states = model.CountryId != Guid.Empty ? (_lookup.GetStates(model.CountryId) ?? new System.Collections.Generic.List<LookupItem>()) : new System.Collections.Generic.List<LookupItem>();
            ViewBag.States = states.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name,
                Selected = s.Id == model.StateId
            }).ToList();

            var cities = model.StateId != Guid.Empty ? (_lookup.GetCities(model.StateId) ?? new System.Collections.Generic.List<LookupItem>()) : new System.Collections.Generic.List<LookupItem>();
            ViewBag.Cities = cities.Select(ci => new SelectListItem
            {
                Value = ci.Id.ToString(),
                Text = ci.Name,
                Selected = ci.Id == model.CityId
            }).ToList();
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var list = _service.GetAll();
            var schools = _schoolService.GetAll();
            var result = list.Select(item =>
            {
                var school = schools.FirstOrDefault(s => s.Id == item.SchoolId);
                return new DriverListItemViewModel
                {
                    Id = item.Id,
                    Name = string.Join(" ", new[] { item.FirstName, item.LastName }.Where(x => !string.IsNullOrWhiteSpace(x))),
                    MobileNumber = item.MobileNumber ?? string.Empty,
                    PhoneNumber = item.PhoneNumber ?? string.Empty,
                    IsActive = item.IsActive,
                    SchoolName = school?.Name ?? string.Empty
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
            return View(item);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var entity = new DriverMaster
            {
                IsActive = true,
                IsDeleted = false,
                Status = "INC",
                StatusMessage = "In Process....",
                CreatedDate = DateTime.UtcNow
            };
            PopulateQualifications(entity.QualificationId);
            PopulateLocationLists(entity);
            return View(entity);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DriverMaster model)
        {
            var schoolIdStr = HttpContext.Session.GetString("SchoolId");
            if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolId))
            {
                model.SchoolId = schoolId;
            }

            if (!ModelState.IsValid)
            {
                PopulateQualifications(model.QualificationId);
                PopulateLocationLists(model);
                return View(model);
            }

            var userIdStr = HttpContext.Session.GetString("UserId");
            var companyIdStr = HttpContext.Session.GetString("CompanyId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId) || model.SchoolId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Please login and select company to create driver.");
                return View(model);
            }

            // Normalize optional strings
            model.Id = Guid.Empty;
            model.FirstName = model.FirstName ?? string.Empty;
            model.LastName = model.LastName ?? string.Empty;
            model.FathersName = model.FathersName ?? string.Empty;
            model.MothersName = model.MothersName ?? string.Empty;
            model.Address1 = model.Address1 ?? string.Empty;
            model.Address2 = model.Address2 ?? string.Empty;
            model.ZipCode = model.ZipCode ?? string.Empty;
            model.MobileNumber = model.MobileNumber ?? string.Empty;
            model.PhoneNumber = model.PhoneNumber ?? string.Empty;
            model.DriverImage = model.DriverImage ?? string.Empty;
            model.LicenceNumber = model.LicenceNumber ?? string.Empty;
            model.LicenceDescription = model.LicenceDescription ?? string.Empty;
            model.LicenceImage = model.LicenceImage ?? string.Empty;
            model.LicenceType = model.LicenceType ?? string.Empty;
            model.Status = string.IsNullOrWhiteSpace(model.Status) ? "INC" : model.Status;
            model.StatusMessage = string.IsNullOrWhiteSpace(model.StatusMessage) ? "In Process...." : model.StatusMessage;
            model.CompanyId = companyId;
            model.CreatedBy = userId;
            model.CreatedDate = DateTime.UtcNow;

            var newId = _service.Create(model);
            if (newId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Failed to create driver.");
                PopulateQualifications(model.QualificationId);
                PopulateLocationLists(model);
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
            PopulateQualifications(item.QualificationId);
            PopulateLocationLists(item);
            return View(item);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, DriverMaster model)
        {
            if (id != model.Id) return BadRequest();

            var schoolIdStr = HttpContext.Session.GetString("SchoolId");
            if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolIdFromSession))
            {
                model.SchoolId = schoolIdFromSession;
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || model.SchoolId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Please login to update driver.");
                return View(model);
            }

            // Normalize optional strings
            model.FirstName = model.FirstName ?? string.Empty;
            model.LastName = model.LastName ?? string.Empty;
            model.FathersName = model.FathersName ?? string.Empty;
            model.MothersName = model.MothersName ?? string.Empty;
            model.Address1 = model.Address1 ?? string.Empty;
            model.Address2 = model.Address2 ?? string.Empty;
            model.ZipCode = model.ZipCode ?? string.Empty;
            model.MobileNumber = model.MobileNumber ?? string.Empty;
            model.PhoneNumber = model.PhoneNumber ?? string.Empty;
            model.DriverImage = model.DriverImage ?? string.Empty;
            model.LicenceNumber = model.LicenceNumber ?? string.Empty;
            model.LicenceDescription = model.LicenceDescription ?? string.Empty;
            model.LicenceImage = model.LicenceImage ?? string.Empty;
            model.LicenceType = model.LicenceType ?? string.Empty;
            model.Status = string.IsNullOrWhiteSpace(model.Status) ? "INC" : model.Status;
            model.StatusMessage = string.IsNullOrWhiteSpace(model.StatusMessage) ? "In Process...." : model.StatusMessage;
            model.ModifiedBy = userId;
            model.ModifiedDate = DateTime.UtcNow;

            if (!_service.Update(model))
            {
                ModelState.AddModelError(string.Empty, "Failed to update driver.");
                PopulateQualifications(model.QualificationId);
                PopulateLocationLists(model);
                return View(model);
            }
            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        [Route("GetStates")]
        public IActionResult GetStates(Guid countryId)
        {
            var states = _lookup.GetStates(countryId) ?? new System.Collections.Generic.List<LookupItem>();
            return Json(states.Select(s => new { id = s.Id, name = s.Name }));
        }

        [HttpGet]
        [Route("GetCities")]
        public IActionResult GetCities(Guid stateId)
        {
            var cities = _lookup.GetCities(stateId) ?? new System.Collections.Generic.List<LookupItem>();
            return Json(cities.Select(c => new { id = c.Id, name = c.Name }));
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
                TempData["ErrorMessage"] = "Failed to delete driver.";
                return RedirectToAction("Delete", new { id });
            }
            return RedirectToAction("Index");
        }
    }
}
