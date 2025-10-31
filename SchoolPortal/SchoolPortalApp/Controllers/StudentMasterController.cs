using System;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortalApp.Controllers
{
    [Route("StudentMaster")]
    public class StudentMasterController : Controller
    {
        private readonly IStudentService _service;
        private readonly ISchoolService _schoolService;
        private readonly IClassService _classService;
        private readonly ISectionService _sectionService;
        private readonly ITeacherService _teacherService;
        private readonly ILookupService _lookupService;
        private readonly ILogger<StudentMasterController> _logger;
        private readonly IWebHostEnvironment _env;

        public StudentMasterController(
            IStudentService service,
            ISchoolService schoolService,
            IClassService classService,
            ISectionService sectionService,
            ITeacherService teacherService,
            ILookupService lookupService,
            ILogger<StudentMasterController> logger,
            IWebHostEnvironment env)
        {
            _service = service;
            _schoolService = schoolService;
            _classService = classService;
            _sectionService = sectionService;
            _teacherService = teacherService;
            _lookupService = lookupService;
            _logger = logger;
            _env = env;
        }

        [HttpGet]
        [Route("GetSectionsByClass/{classId}")]
        public IActionResult GetSectionsByClass(Guid classId)
        {
            try
            {
                var sections = _sectionService.GetSectionsByClassId(classId);
                var result = sections.Select(s => new 
                { 
                    value = s.Id.ToString(), 
                    text = s.Name 
                });
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sections for class {ClassId}", classId);
                return StatusCode(500, "Error loading sections");
            }
        }

        [HttpGet]
        [Route("GetStatesByCountry/{countryId}")]
        public IActionResult GetStatesByCountry(Guid countryId)
        {
            try
            {
                var states = _lookupService.GetStates(countryId);
                var result = states.Select(s => new 
                { 
                    value = s.Id.ToString(), 
                    text = s.Name 
                });
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting states for country {CountryId}", countryId);
                return StatusCode(500, "Error loading states");
            }
        }

        [HttpGet]
        [Route("GetCitiesByState/{stateId}")]
        public IActionResult GetCitiesByState(Guid stateId)
        {
            try
            {
                var cities = _lookupService.GetCities(stateId);
                var result = cities.Select(c => new 
                { 
                    value = c.Id.ToString(), 
                    text = c.Name 
                });
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cities for state {StateId}", stateId);
                return StatusCode(500, "Error loading cities");
            }
        }

        private void PopulateDropdowns(StudentViewModel vm)
        {
            var schools = _schoolService.GetAll();
            vm.Schools = schools.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.SchoolId }).ToList();

            // Classes
            var classes = _classService.GetAll();
            vm.Classes = classes.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == vm.ClassId }).ToList();

            // Sections
            var sections = _sectionService.GetAll();
            vm.Sections = sections.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.SectionId }).ToList();

            // Teachers (Class Teachers)
            var teachers = _teacherService.GetAll();
            vm.ClassTeachers = teachers.Select(t => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = t.Id.ToString(), Text = string.Join(" ", new[] { t.FirstName, t.LastName }.Where(x => !string.IsNullOrWhiteSpace(x))), Selected = vm.ClassTeacherId.HasValue && t.Id == vm.ClassTeacherId.Value }).ToList();

            // Countries / States / Cities
            var countries = _lookupService.GetCountries();
            vm.Countries = countries.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == vm.CountryId }).ToList();
            vm.BirthCountries = countries.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == vm.BirthCountryId }).ToList();
            vm.Nationalities = countries.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == vm.Nationality }).ToList();

            if (vm.CountryId != Guid.Empty)
            {
                var states = _lookupService.GetStates(vm.CountryId);
                vm.States = states.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == vm.StateId }).ToList();
                if (vm.StateId != Guid.Empty)
                {
                    var cities = _lookupService.GetCities(vm.StateId);
                    vm.Cities = cities.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == vm.CityId }).ToList();
                }
            }

            if (vm.BirthCountryId != Guid.Empty)
            {
                var birthStates = _lookupService.GetStates(vm.BirthCountryId);
                vm.BirthStates = birthStates.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == vm.BirthStateId }).ToList();
                if (vm.BirthStateId != Guid.Empty)
                {
                    var birthCities = _lookupService.GetCities(vm.BirthStateId);
                    vm.BirthCities = birthCities.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == vm.BirthCityId }).ToList();
                }
            }
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
                return new StudentListItemViewModel
                {
                    Id = item.Id,
                    Name = string.Join(" ", new[] { item.FirstName, item.LastName }.Where(x => !string.IsNullOrWhiteSpace(x))),
                    Email = item.Email ?? string.Empty,
                    Phone = item.Phone ?? string.Empty,
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
            var vm = new StudentViewModel();
            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentViewModel model)
        {
            var schoolIdStr = HttpContext.Session.GetString("SchoolId");
            if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolId))
            {
                ModelState.Remove(nameof(StudentViewModel.SchoolId));
                model.SchoolId = schoolId;
            }

            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model);
                return View(model);
            }

            var userIdStr = HttpContext.Session.GetString("UserId");
            var companyIdStr = HttpContext.Session.GetString("CompanyId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId) || model.SchoolId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Please login and select company to create student.");
                PopulateDropdowns(model);
                return View(model);
            }

            // Handle image upload if provided
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadsRoot = Path.Combine(_env.WebRootPath ?? string.Empty, "uploads", "students");
                Directory.CreateDirectory(uploadsRoot);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImageFile.FileName)}";
                var fullPath = Path.Combine(uploadsRoot, fileName);
                using (var stream = System.IO.File.Create(fullPath))
                {
                    model.ImageFile.CopyTo(stream);
                }
                // store web-relative path
                model.Image = $"/uploads/students/{fileName}";
            }

            var entity = new StudentMaster
            {
                Id = Guid.Empty,
                RollNumber = model.RollNumber,
                FirstName = model.FirstName,
                LastName = model.LastName ?? string.Empty,
                Address = model.Address ?? string.Empty,
                CityId = model.CityId,
                StateId = model.StateId,
                CountryId = model.CountryId,
                ZipCode = model.ZipCode ?? string.Empty,
                ContactNumber = model.ContactNumber ?? string.Empty,
                EmergencyContactNumber = model.EmergencyContactNumber ?? string.Empty,
                DOB = model.DOB,
                DOJ = model.DOJ,
                RegistrationNumber = model.RegistrationNumber ?? string.Empty,
                ClassId = model.ClassId,
                SectionId = model.SectionId,
                AvailTransport = model.AvailTransport,
                Image = model.Image ?? string.Empty,
                Email = model.Email ?? string.Empty,
                CategoryId = model.CategoryId,
                SiblingsIfAny = model.SiblingsIfAny,
                SiblingClassId = model.SiblingClassId,
                Gender = model.Gender,
                DisabilityAny = model.DisabilityAny ?? string.Empty,
                MedicalAlleryAny = model.MedicalAlleryAny ?? string.Empty,
                BirthCityId = model.BirthCityId,
                BirthStateId = model.BirthStateId,
                BirthCountryId = model.BirthCountryId,
                PreviousSchoolAttended = model.PreviousSchoolAttended ?? string.Empty,
                PreviousSchoolClassId = model.PreviousSchoolClassId,
                PreviousSchoolPercentage = model.PreviousSchoolPercentage,
                PreviousSchoolRank = model.PreviousSchoolRank ?? string.Empty,
                PreviousSchoolBoardId = model.PreviousSchoolBoardId,
                PreviousSchoolFromDate = model.PreviousSchoolFromDate,
                PreviousSchoolToDate = model.PreviousSchoolToDate,
                WithdrawnDate = model.WithdrawnDate,
                WithdrawnReason = model.WithdrawnReason ?? string.Empty,
                BloodGroupId = model.BloodGroupId,
                Nationality = model.Nationality,
                Hobbies = model.Hobbies ?? string.Empty,
                ReligionId = model.ReligionId,
                Phone = model.Phone ?? string.Empty,
                RouteId = model.RouteId,
                RouteStopDetailsId = model.RouteStopDetailsId,
                ClassTeacherId = model.ClassTeacherId,
                RoutePickAndDrop = model.RoutePickAndDrop,
                FeesDiscountCategoryMasterId = model.FeesDiscountCategoryMasterId,
                TutionFees = model.TutionFees,
                AnnualFees = model.AnnualFees,
                TransportFees = model.TransportFees,
                UseTransportFees = model.UseTransportFees,
                SessionId = model.SessionId,
                CompanyId = companyId,
                SchoolId = model.SchoolId,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow,
                Status = model.Status ?? string.Empty,
                StatusMessage = model.StatusMessage ?? string.Empty,
                HouseAllotted = model.HouseAllotted
            };

            var newId = _service.Create(entity);
            if (newId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Failed to create student.");
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
            var vm = new StudentViewModel
            {
                Id = item.Id,
                RollNumber = item.RollNumber,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Address = item.Address,
                CityId = item.CityId,
                StateId = item.StateId,
                CountryId = item.CountryId,
                ZipCode = item.ZipCode,
                ContactNumber = item.ContactNumber,
                EmergencyContactNumber = item.EmergencyContactNumber,
                DOB = item.DOB,
                DOJ = item.DOJ,
                RegistrationNumber = item.RegistrationNumber,
                ClassId = item.ClassId,
                SectionId = item.SectionId,
                AvailTransport = item.AvailTransport,
                Image = item.Image,
                Email = item.Email,
                CategoryId = item.CategoryId,
                SiblingsIfAny = item.SiblingsIfAny,
                SiblingClassId = item.SiblingClassId,
                Gender = item.Gender,
                DisabilityAny = item.DisabilityAny,
                MedicalAlleryAny = item.MedicalAlleryAny,
                BirthCityId = item.BirthCityId,
                BirthStateId = item.BirthStateId,
                BirthCountryId = item.BirthCountryId,
                PreviousSchoolAttended = item.PreviousSchoolAttended,
                PreviousSchoolClassId = item.PreviousSchoolClassId,
                PreviousSchoolPercentage = item.PreviousSchoolPercentage,
                PreviousSchoolRank = item.PreviousSchoolRank,
                PreviousSchoolBoardId = item.PreviousSchoolBoardId,
                PreviousSchoolFromDate = item.PreviousSchoolFromDate,
                PreviousSchoolToDate = item.PreviousSchoolToDate,
                WithdrawnDate = item.WithdrawnDate,
                WithdrawnReason = item.WithdrawnReason,
                BloodGroupId = item.BloodGroupId,
                Nationality = item.Nationality,
                Hobbies = item.Hobbies,
                ReligionId = item.ReligionId,
                RouteId = item.RouteId,
                RouteStopDetailsId = item.RouteStopDetailsId,
                ClassTeacherId = item.ClassTeacherId,
                RoutePickAndDrop = item.RoutePickAndDrop,
                FeesDiscountCategoryMasterId = item.FeesDiscountCategoryMasterId,
                TutionFees = item.TutionFees,
                AnnualFees = item.AnnualFees,
                TransportFees = item.TransportFees,
                UseTransportFees = item.UseTransportFees,
                SessionId = item.SessionId,
                IsDeleted = item.IsDeleted,
                Status = item.Status,
                StatusMessage = item.StatusMessage,
                HouseAllotted = item.HouseAllotted
            };
            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, StudentViewModel model)
        {
            if (id != model.Id) return BadRequest();

            var schoolIdStr = HttpContext.Session.GetString("SchoolId");
            if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolIdFromSession))
            {
                ModelState.Remove(nameof(StudentViewModel.SchoolId));
                model.SchoolId = schoolIdFromSession;
            }

            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model);
                return View(model);
            }

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) || model.SchoolId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Please login to update student.");
                PopulateDropdowns(model);
                return View(model);
            }

            // Handle image upload if provided (replace existing path)
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadsRoot = Path.Combine(_env.WebRootPath ?? string.Empty, "uploads", "students");
                Directory.CreateDirectory(uploadsRoot);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImageFile.FileName)}";
                var fullPath = Path.Combine(uploadsRoot, fileName);
                using (var stream = System.IO.File.Create(fullPath))
                {
                    model.ImageFile.CopyTo(stream);
                }
                model.Image = $"/uploads/students/{fileName}";
            }

            var entity = new StudentMaster
            {
                Id = id,
                RollNumber = model.RollNumber,
                FirstName = model.FirstName,
                LastName = model.LastName ?? string.Empty,
                Address = model.Address ?? string.Empty,
                CityId = model.CityId,
                StateId = model.StateId,
                CountryId = model.CountryId,
                ZipCode = model.ZipCode ?? string.Empty,
                ContactNumber = model.ContactNumber ?? string.Empty,
                EmergencyContactNumber = model.EmergencyContactNumber ?? string.Empty,
                DOB = model.DOB,
                DOJ = model.DOJ,
                RegistrationNumber = model.RegistrationNumber ?? string.Empty,
                ClassId = model.ClassId,
                SectionId = model.SectionId,
                AvailTransport = model.AvailTransport,
                Image = model.Image ?? string.Empty,
                Email = model.Email ?? string.Empty,
                CategoryId = model.CategoryId,
                SiblingsIfAny = model.SiblingsIfAny,
                SiblingClassId = model.SiblingClassId,
                Gender = model.Gender,
                DisabilityAny = model.DisabilityAny ?? string.Empty,
                MedicalAlleryAny = model.MedicalAlleryAny ?? string.Empty,
                BirthCityId = model.BirthCityId,
                BirthStateId = model.BirthStateId,
                BirthCountryId = model.BirthCountryId,
                PreviousSchoolAttended = model.PreviousSchoolAttended ?? string.Empty,
                PreviousSchoolClassId = model.PreviousSchoolClassId,
                PreviousSchoolPercentage = model.PreviousSchoolPercentage,
                PreviousSchoolRank = model.PreviousSchoolRank ?? string.Empty,
                PreviousSchoolBoardId = model.PreviousSchoolBoardId,
                PreviousSchoolFromDate = model.PreviousSchoolFromDate,
                PreviousSchoolToDate = model.PreviousSchoolToDate,
                WithdrawnDate = model.WithdrawnDate,
                WithdrawnReason = model.WithdrawnReason ?? string.Empty,
                BloodGroupId = model.BloodGroupId,
                Nationality = model.Nationality,
                Hobbies = model.Hobbies ?? string.Empty,
                ReligionId = model.ReligionId,
                Phone = model.Phone ?? string.Empty,
                RouteId = model.RouteId,
                RouteStopDetailsId = model.RouteStopDetailsId,
                ClassTeacherId = model.ClassTeacherId,
                RoutePickAndDrop = model.RoutePickAndDrop,
                FeesDiscountCategoryMasterId = model.FeesDiscountCategoryMasterId,
                TutionFees = model.TutionFees,
                AnnualFees = model.AnnualFees,
                TransportFees = model.TransportFees,
                UseTransportFees = model.UseTransportFees,
                SessionId = model.SessionId,
                SchoolId = model.SchoolId,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                ModifiedBy = userId,
                ModifiedDate = DateTime.UtcNow,
                Status = model.Status ?? string.Empty,
                StatusMessage = model.StatusMessage ?? string.Empty,
                HouseAllotted = model.HouseAllotted
            };

            if (!_service.Update(entity))
            {
                ModelState.AddModelError(string.Empty, "Failed to update student.");
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
                TempData["ErrorMessage"] = "Failed to delete student.";
                return RedirectToAction("Delete", new { id });
            }
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public IActionResult GetStatesByCountry(Guid countryId)
        //{
        //    var states = _lookupService.GetStates(countryId)
        //        .Select(x => new 
        //        { 
        //            value = x.Id.ToString(), 
        //            text = x.Name,
        //            selected = false
        //        });
        //    return Json(states);
        //}

        //[HttpGet]
        //public IActionResult GetCitiesByState(Guid stateId)
        //{
        //    var cities = _lookupService.GetCities(stateId)
        //        .Select(x => new 
        //        { 
        //            value = x.Id.ToString(), 
        //            text = x.Name,
        //            selected = false
        //        });
        //    return Json(cities);
        //}
    }
}
