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
	[Route("Privileges")]
	public class PrivilegesController : Controller
	{
		private readonly IPrivilegeService _service;
		private readonly ILogger<PrivilegesController> _logger;

		public PrivilegesController(IPrivilegeService service, ILogger<PrivilegesController> logger)
		{
			_service = service;
			_logger = logger;
		}

		private void PopulateDropdowns(PrivilegeViewModel vm)
		{
			var allPrivileges = _service.GetAll();
			// Exclude the current privilege from parent options to prevent circular references
			var parentPrivileges = allPrivileges.Where(p => p.Id != vm.Id).ToList();
			vm.ParentPrivileges = parentPrivileges.Select(p => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem 
			{ 
				Value = p.Id.ToString(), 
				Text = p.PrivilegeName,
				Selected = p.Id == vm.PrivilegeParentId 
			}).ToList();
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var list = _service.GetAll();
			var allPrivileges = list.ToList();
			var result = list.Select(item =>
			{
				var parent = allPrivileges.FirstOrDefault(p => p.Id == item.PrivilegeParentId);
				return new PrivilegeListItemViewModel
				{
					Id = item.Id,
					PrivilegeName = item.PrivilegeName,
					IsActive = item.IsActive,
					ParentPrivilegeName = parent?.PrivilegeName ?? string.Empty,
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

			var allPrivileges = _service.GetAll().ToList();
			var parent = allPrivileges.FirstOrDefault(p => p.Id == item.PrivilegeParentId);

			var vm = new PrivilegeDetailsViewModel
			{
				Id = item.Id,
				PrivilegeName = item.PrivilegeName ?? string.Empty,
				IsActive = item.IsActive,
				ParentPrivilegeName = parent?.PrivilegeName ?? string.Empty,
			};
			return View(vm);
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var vm = new PrivilegeViewModel();
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(PrivilegeViewModel model)
		{
			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}
			var userIdStr = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
			{
				ModelState.AddModelError(string.Empty, "Please login to create privilege.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new Privileges
			{
				Id = Guid.NewGuid(),
				PrivilegeName = model.PrivilegeName,
				IsActive = model.IsActive,
				CreatedBy = userId,
				CreatedDate = DateTime.UtcNow,
				PrivilegeParentId = model.PrivilegeParentId,
				Status = string.Empty,
				StatusMessage = string.Empty,
			};

			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create privilege.");
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
			var vm = new PrivilegeViewModel
			{
				Id = item.Id,
				PrivilegeName = item.PrivilegeName,
				IsActive = item.IsActive,
				PrivilegeParentId = item.PrivilegeParentId,
			};
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, PrivilegeViewModel model)
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
				ModelState.AddModelError(string.Empty, "Please login to update privilege.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new Privileges
			{
				Id = id,
				PrivilegeName = model.PrivilegeName,
				IsActive = model.IsActive,
				ModifiedBy = userId,
				ModifiedDate = DateTime.UtcNow,
				PrivilegeParentId = model.PrivilegeParentId,
				Status = string.Empty,
				StatusMessage = string.Empty,
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update privilege.");
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
				TempData["ErrorMessage"] = "Failed to delete privilege.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}
	}
}