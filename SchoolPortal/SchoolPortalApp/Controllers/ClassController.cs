using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;
using System.Collections.Generic;

namespace SchoolPortalApp.Controllers
{
	[Route("Class")]
	public class ClassController : BaseController
	{
		private readonly IClassService _service;
		private readonly ISchoolService _schoolService;
		private readonly ILogger<ClassController> _logger;

		public ClassController(IClassService service, ISchoolService schoolService, ILogger<ClassController> logger)
		{
			_service = service;
			_schoolService = schoolService;
			_logger = logger;
		}

		private void PopulateDropdowns(ClassViewModel vm)
		{
			var schools = _schoolService.GetAll();
			vm.Schools = schools.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.SchoolId }).ToList();
		}

		[HttpPost]
        [Route("GetClassesData")]
        public IActionResult GetClassesData()
        {
            try
            {
                var requestForm = Request.Form;
                var draw = Convert.ToInt32(requestForm["draw"].FirstOrDefault() ?? "0");
                var start = Convert.ToInt32(requestForm["start"].FirstOrDefault() ?? "0");
                var length = Convert.ToInt32(requestForm["length"].FirstOrDefault() ?? "10");
                var sortColumn = requestForm["columns[" + requestForm["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = requestForm["order[0][dir]"].FirstOrDefault();
                var searchValue = requestForm["search[value]"].FirstOrDefault();
                int pageSize = length != -1 ? length : 0;
                int skip = start != 0 ? start : 0;
                int recordsTotal = 0;

                // Filter classes by SchoolId from session
                var schoolId = CurrentSchoolId;
                if (!schoolId.HasValue)
                {
                    return Json(new 
                    { 
                        draw = draw,
                        recordsFiltered = 0,
                        recordsTotal = 0,
                        data = new List<object>()
                    });
                }

                // Get all classes for the current school
                var list = _service.GetAll()
                    .Where(c => c.SchoolId == schoolId.Value)
                    .ToList();

                var schools = _schoolService.GetAll();
                var classes = list.Select(item =>
                {
                    var school = schools.FirstOrDefault(s => s.Id == item.SchoolId);
                    return new 
                    {
                        id = item.Id,
                        name = item.Name,
                        examAssessment = item.ExamAssessment,
                        isActive = item.IsActive,
                        schoolName = school?.Name ?? string.Empty
                    };
                }).ToList();

                // Apply search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    classes = classes.Where(c => 
                        (c.name != null && c.name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
                        (c.examAssessment != null && c.examAssessment.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
                        (c.schoolName != null && c.schoolName.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }

                // Get total count
                recordsTotal = classes.Count;

                // Apply sorting
                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                {
                    var propertyInfo = typeof(ClassListItemViewModel).GetProperty(sortColumn, 
                        System.Reflection.BindingFlags.IgnoreCase | 
                        System.Reflection.BindingFlags.Public | 
                        System.Reflection.BindingFlags.Instance);

                    if (propertyInfo != null)
                    {
                        classes = sortColumnDirection.ToLower() == "asc"
                            ? classes.OrderBy(x => x.GetType().GetProperty(sortColumn.ToLower())?.GetValue(x, null)).ToList()
                            : classes.OrderByDescending(x => x.GetType().GetProperty(sortColumn.ToLower())?.GetValue(x, null)).ToList();
                    }
                }

                // Apply pagination
                var data = classes
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();

                return Json(new 
                {
                    draw = draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = data
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading classes data");
                return Json(new { error = "An error occurred while loading classes data." });
            }
        }

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			return View();
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
			var vm = new ClassViewModel();
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ClassViewModel model)
		{
			// Take SchoolId from session instead of user input
			var schoolId = CurrentSchoolId;
			if (schoolId.HasValue)
			{
				ModelState.Remove(nameof(ClassViewModel.SchoolId));
				model.SchoolId = schoolId.Value;
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userId = CurrentUserId;
			var companyId = CurrentCompanyId;
			if (!userId.HasValue || !companyId.HasValue || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login and select company to create class.");
				return View(model);
			}

			var entity = new ClassMaster
			{
				Id = Guid.Empty,
				Name = model.Name,
				ExamAssessment = (model.ExamAssessment ?? false) ? "Yes" : "No",
				IsGradePointApplicable = model.IsGradePointApplicable ?? false,
				IsActive = model.IsActive,
				CompanyId = companyId.Value,
				SchoolId = model.SchoolId,
				CreatedBy = userId.Value,
				CreatedDate = DateTime.UtcNow
			};

			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create class.");
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
			var vm = new ClassViewModel
			{
				Id = item.Id,
				Name = item.Name,
				ExamAssessment = string.Equals(item.ExamAssessment, "Yes", StringComparison.OrdinalIgnoreCase)
								   || string.Equals(item.ExamAssessment, "True", StringComparison.OrdinalIgnoreCase)
								   || item.ExamAssessment == "1",
				IsGradePointApplicable = item.IsGradePointApplicable,
				IsActive = item.IsActive,
				SchoolId = item.SchoolId
			};
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, ClassViewModel model)
		{
			if (id != model.Id) return BadRequest();

			// Take SchoolId from session instead of user input
			var schoolId = CurrentSchoolId;
			if (schoolId.HasValue)
			{
				ModelState.Remove(nameof(ClassViewModel.SchoolId));
				model.SchoolId = schoolId.Value;
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userId = CurrentUserId;
			if (!userId.HasValue || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login to update class.");
				return View(model);
			}

			var entity = new ClassMaster
			{
				Id = id,
				Name = model.Name,
				ExamAssessment = (model.ExamAssessment ?? false) ? "Yes" : "No",
				IsGradePointApplicable = model.IsGradePointApplicable ?? false,
				IsActive = model.IsActive,
				SchoolId = model.SchoolId,
				ModifiedBy = userId,
				ModifiedDate = DateTime.UtcNow
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update class.");
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
		[ActionName("Delete")]
		[Route("Delete/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			try
			{
				var result = _service.Delete(id);
				if (result)
				{
					if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
					{
						return Json(new { success = true, message = "Class deleted successfully" });
					}
					return RedirectToAction("Index");
				}
				
				if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
				{
					return Json(new { success = false, message = "Failed to delete class" });
				}
				return View("Delete", _service.GetById(id));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error deleting class");
				if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
				{
					return Json(new { success = false, message = "An error occurred while deleting the class" });
				}
				return View("Error");
			}
		}
	}
}
