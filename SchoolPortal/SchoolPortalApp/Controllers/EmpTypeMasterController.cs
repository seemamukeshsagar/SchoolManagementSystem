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
	[Route("EmpTypeMaster")]
	public class EmpTypeMasterController : Controller
	{
		private readonly IEmpTypeService _service;
		private readonly ILookupService _lookup;
		private readonly ISchoolService _schoolService;
		private readonly ICompanyService _companyService;
		private readonly ILogger<EmpTypeMasterController> _logger;

		public EmpTypeMasterController(
			IEmpTypeService service, 
			ILookupService lookup, 
			ISchoolService schoolService,
			ICompanyService companyService,
			ILogger<EmpTypeMasterController> logger)
		{
			_service = service;
			_lookup = lookup;
			_schoolService = schoolService;
			_companyService = companyService;
			_logger = logger;
		}

		private void PopulateDropdowns(EmpTypeViewModel vm)
		{
			var companies = _lookup.GetCompanies();
			vm.Companies = companies.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == vm.CompanyId }).ToList();

			var schools = _schoolService.GetAll();
			vm.Schools = schools.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = s.Id == vm.SchoolId }).ToList();
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var list = _service.GetAll();
			var companies = _lookup.GetCompanies();
			var schools = _schoolService.GetAll();
			var result = list.Select(item =>
			{
				var company = companies.FirstOrDefault(c => c.Id == item.CompanyId);
				var school = schools.FirstOrDefault(s => s.Id == item.SchoolId);
				return new EmpTypeListItemViewModel
				{
					Id = item.Id,
					TypeName = item.TypeName,
					Description = item.Description,
					IsActive = item.IsActive,
					CompanyName = company?.Name ?? string.Empty,
					SchoolName = school?.Name ?? string.Empty,
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

			var companies = _lookup.GetCompanies();
			var company = companies.FirstOrDefault(c => c.Id == item.CompanyId);
			var schools = _schoolService.GetAll();
			var school = schools.FirstOrDefault(s => s.Id == item.SchoolId);

			var vm = new EmpTypeDetailsViewModel
			{
				Id = item.Id,
				TypeName = item.TypeName ?? string.Empty,
				Description = item.Description ?? string.Empty,
				IsActive = item.IsActive,
				CompanyName = company?.Name ?? string.Empty,
				SchoolName = school?.Name ?? string.Empty,
			};
			return View(vm);
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var vm = new EmpTypeViewModel();
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(EmpTypeViewModel model)
		{
			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
			{
				ModelState.AddModelError(string.Empty, "Please login to create employee type.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new EmpTypeMaster
			{
				Id = Guid.Empty,
				TypeName = model.TypeName,
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
				ModelState.AddModelError(string.Empty, "Failed to create employee type.");
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
			var vm = new EmpTypeViewModel
			{
				Id = item.Id,
				TypeName = item.TypeName,
				Description = item.Description,
				CompanyId = item.CompanyId,
				SchoolId = item.SchoolId,
				IsActive = item.IsActive,
			};
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, EmpTypeViewModel model)
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
				ModelState.AddModelError(string.Empty, "Please login to update employee type.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new EmpTypeMaster
			{
				Id = id,
				TypeName = model.TypeName,
				Description = model.Description ?? string.Empty,
				CompanyId = model.CompanyId,
				SchoolId = model.SchoolId,
				IsActive = model.IsActive,
				ModifiedBy = userId,
				ModifiedDate = DateTime.UtcNow,
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update employee type.");
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
				TempData["ErrorMessage"] = "Failed to delete employee type.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}
	}
}

