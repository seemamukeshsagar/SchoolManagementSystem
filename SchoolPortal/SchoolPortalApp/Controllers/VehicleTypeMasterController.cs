using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SchoolPortalApp.Controllers
{
	[Route("VehicleTypeMaster")]
	public class VehicleTypeMasterController : Controller
	{
		private readonly IVehicleTypeMasterService _service;
		private readonly ICompanyService _companyService;
		private readonly ISchoolService _schoolService;

		public VehicleTypeMasterController(IVehicleTypeMasterService service, ICompanyService companyService, ISchoolService schoolService)
		{
			_service = service;
			_companyService = companyService;
			_schoolService = schoolService;
		}

		private void PopulateDropdowns(VehicleTypeMasterViewModel vm)
		{
			// Populate Companies dropdown
			var companies = _companyService.GetAll();
			ViewBag.Companies = companies.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem 
			{ 
				Value = c.Id.ToString(), 
				Text = c.CompanyName 
			}).ToList();

			// Populate Schools dropdown
			var schools = _schoolService.GetAll();
			ViewBag.Schools = schools.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem 
			{ 
				Value = s.Id.ToString(), 
				Text = s.SchoolName 
			}).ToList();
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var list = _service.GetAll();
			
			// Enrich with company and school names
			var companies = _companyService.GetAll().ToDictionary(c => c.Id, c => c.CompanyName);
			var schools = _schoolService.GetAll().ToDictionary(s => s.Id, s => s.SchoolName);
			
			var result = list.Select(item =>
			{
				return new
				{
					Id = item.Id,
					VehicleType = item.VehicleType,
					CompanyName = item.CompanyId.HasValue && companies.ContainsKey(item.CompanyId.Value) ? companies[item.CompanyId.Value] : string.Empty,
					SchoolName = item.SchoolId.HasValue && schools.ContainsKey(item.SchoolId.Value) ? schools[item.SchoolId.Value] : string.Empty,
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

			var companies = _companyService.GetAll().ToDictionary(c => c.Id, c => c.CompanyName);
			var schools = _schoolService.GetAll().ToDictionary(s => s.Id, s => s.SchoolName);

			var vm = new
			{
				Id = item.Id,
				VehicleType = item.VehicleType,
				CompanyName = item.CompanyId.HasValue && companies.ContainsKey(item.CompanyId.Value) ? companies[item.CompanyId.Value] : string.Empty,
				SchoolName = item.SchoolId.HasValue && schools.ContainsKey(item.SchoolId.Value) ? schools[item.SchoolId.Value] : string.Empty,
				IsActive = item.IsActive
			};
			
			return View(vm);
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var vm = new VehicleTypeMasterViewModel();
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(VehicleTypeMasterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}
			
			var userIdStr = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
			{
				ModelState.AddModelError(string.Empty, "Please login to create vehicle type.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new VehicleTypeMaster
			{
				Id = Guid.Empty,
				VehicleType = model.VehicleType,
				CompanyId = model.CompanyId,
				SchoolId = model.SchoolId,
				IsActive = model.IsActive,
				CreatedBy = userId,
				CreatedDate = DateTime.UtcNow,
				Status = "ACT",
				StatusMessage = "Active"
			};

			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create vehicle type.");
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
			
			var vm = new VehicleTypeMasterViewModel
			{
				Id = item.Id,
				VehicleType = item.VehicleType,
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
		public IActionResult Edit(Guid id, VehicleTypeMasterViewModel model)
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
				ModelState.AddModelError(string.Empty, "Please login to update vehicle type.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new VehicleTypeMaster
			{
				Id = id,
				VehicleType = model.VehicleType,
				CompanyId = model.CompanyId,
				SchoolId = model.SchoolId,
				IsActive = model.IsActive,
				ModifiedBy = userId,
				ModifiedDate = DateTime.UtcNow
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update vehicle type.");
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
			
			var companies = _companyService.GetAll().ToDictionary(c => c.Id, c => c.CompanyName);
			var schools = _schoolService.GetAll().ToDictionary(s => s.Id, s => s.SchoolName);

			var vm = new
			{
				Id = item.Id,
				VehicleType = item.VehicleType,
				CompanyName = item.CompanyId.HasValue && companies.ContainsKey(item.CompanyId.Value) ? companies[item.CompanyId.Value] : string.Empty,
				SchoolName = item.SchoolId.HasValue && schools.ContainsKey(item.SchoolId.Value) ? schools[item.SchoolId.Value] : string.Empty,
				IsActive = item.IsActive
			};
			
			return View(vm);
		}

		[HttpPost]
		[Route("Delete/{id}")]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult ConfirmDelete(Guid id)
		{
			if (!_service.Delete(id))
			{
				TempData["ErrorMessage"] = "Failed to delete vehicle type.";
				return RedirectToAction("Delete", new { id });
			}
			
			return RedirectToAction("Index");
		}
	}
}