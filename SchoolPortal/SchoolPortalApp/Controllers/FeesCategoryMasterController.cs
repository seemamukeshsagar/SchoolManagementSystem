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
	[Route("FeesCategoryMaster")]
	public class FeesCategoryMasterController : Controller
	{
		private readonly IFeesCategoryMasterService _service;
		private readonly ILogger<FeesCategoryMasterController> _logger;

		public FeesCategoryMasterController(IFeesCategoryMasterService service, ILogger<FeesCategoryMasterController> logger)
		{
			_service = service;
			_logger = logger;
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			var list = _service.GetAll();
			var result = list.Select(item => new FeesCategoryMasterListItemViewModel
			{
				Id = item.Id,
				FeesCatgoryName = item.FeesCatgoryName,
				Description = item.Description,
				IsActive = item.IsActive
			}).ToList();
			return View(result);
		}

		[HttpGet]
		[Route("Details/{id}")]
		public IActionResult Details(Guid id)
		{
			var item = _service.GetById(id);
			if (item == null) return NotFound();
			var vm = new FeesCategoryMasterViewModel
			{
				Id = item.Id,
				FeesCatgoryName = item.FeesCatgoryName,
				Description = item.Description,
				IsActive = item.IsActive
			};
			return View(vm);
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			var vm = new FeesCategoryMasterViewModel();
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(FeesCategoryMasterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
			{
				ModelState.AddModelError(string.Empty, "Missing required session data.");
				return View(model);
			}

			var entity = new FeesCategoryMaster
			{
				Id = Guid.Empty,
				FeesCatgoryName = model.FeesCatgoryName,
				Description = model.Description ?? string.Empty,
				IsActive = model.IsActive,
				CreatedBy = userId,
				CreatedDate = DateTime.UtcNow
			};

			var newId = _service.Create(entity);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create fees category.");
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
			var vm = new FeesCategoryMasterViewModel
			{
				Id = item.Id,
				FeesCatgoryName = item.FeesCatgoryName,
				Description = item.Description,
				IsActive = item.IsActive
			};
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, FeesCategoryMasterViewModel model)
		{
			if (id != model.Id) return BadRequest();
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
			{
				ModelState.AddModelError(string.Empty, "Missing required session data.");
				return View(model);
			}

			var entity = new FeesCategoryMaster
			{
				Id = id,
				FeesCatgoryName = model.FeesCatgoryName,
				Description = model.Description ?? string.Empty,
				IsActive = model.IsActive,
				ModifiedBy = userId,
				ModifiedDate = DateTime.UtcNow
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update fees category.");
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
			var vm = new FeesCategoryMasterViewModel
			{
				Id = item.Id,
				FeesCatgoryName = item.FeesCatgoryName,
				Description = item.Description,
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
				ModelState.AddModelError(string.Empty, "Failed to delete fees category.");
				var item = _service.GetById(id);
				if (item == null) return NotFound();
				var vm = new FeesCategoryMasterViewModel
				{
					Id = item.Id,
					FeesCatgoryName = item.FeesCatgoryName,
					Description = item.Description,
					IsActive = item.IsActive
				};
				return View(vm);
			}
			return RedirectToAction("Index");
		}
	}
}
