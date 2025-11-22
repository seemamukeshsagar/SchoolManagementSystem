using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using SchoolPortalApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;

namespace SchoolPortalApp.Controllers
{
	[Route("CleanerMaster")]
	public class CleanerMasterController : BaseController
	{
		private readonly ICleanerMasterService _service;
		private readonly ISchoolService _schoolService;
		private readonly ILookupService _lookup;
		private readonly ICleanerDocumentDetailsService _docService;
		private readonly ICleanerQualificationDetailsService _qualService;
		private readonly IWebHostEnvironment _env;
		private readonly ILogger<CleanerMasterController> _logger;

		public CleanerMasterController(ICleanerMasterService service, ISchoolService schoolService, ILookupService lookup, ICleanerDocumentDetailsService docService, ICleanerQualificationDetailsService qualService, IWebHostEnvironment env, ILogger<CleanerMasterController> logger)
		{
			_service = service;
			_schoolService = schoolService;
			_lookup = lookup;
			_docService = docService;
			_qualService = qualService;
			_env = env;
			_logger = logger;
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
				return new CleanerListItemViewModel
				{
					Id = item.Id,
					Name = item.Name ?? string.Empty,
					FatherName = item.FatherName ?? string.Empty,
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
			var entity = new CleanerMaster
			{
				IsActive = true,
				IsDeleted = false,
				Status = "INC",
				StatusMessage = "In Process....",
				CreatedDate = DateTime.UtcNow
			};
			var vm = new CleanerAggregateViewModel { Master = entity };
			try
			{
				var quals = _lookup.GetQualifications() ?? new List<LookupItem>();
				vm.QualificationItems = quals.Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Name }).ToList();
			}
			catch { }
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(CleanerAggregateViewModel vm)
		{
			var model = vm.Master;
			var schoolId = CurrentSchoolId;
			if (schoolId.HasValue)
			{
				model.SchoolId = schoolId.Value;
			}

			// Validate child rows
			if (vm.Documents != null)
			{
				for (int i = 0; i < vm.Documents.Count; i++)
				{
					var d = vm.Documents[i];
					if (d == null || d.IsDeleted) continue;
					if (string.IsNullOrWhiteSpace(d.Name))
					{
						ModelState.AddModelError($"Documents[{i}].Name", "Document name is required.");
					}
				}
			}
			if (vm.Qualifications != null)
			{
				for (int i = 0; i < vm.Qualifications.Count; i++)
				{
					var q = vm.Qualifications[i];
					if (q == null || q.IsDeleted) continue;
					if (q.QualificationId == Guid.Empty)
					{
						ModelState.AddModelError($"Qualifications[{i}].QualificationId", "Qualification is required.");
					}
				}
			}

			if (!ModelState.IsValid)
			{
				try
				{
					var quals = _lookup.GetQualifications() ?? new List<LookupItem>();
					vm.QualificationItems = quals.Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Name }).ToList();
				}
				catch { }
				return View(vm);
			}

			var userId = CurrentUserId;
			var companyId = CurrentCompanyId;
			if (!userId.HasValue || !companyId.HasValue || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login and select company to create cleaner.");
				return View(vm);
			}

			// Normalize optional strings
			model.Id = Guid.Empty;
			model.Name = model.Name ?? string.Empty;
			if (vm.ImageFile != null && vm.ImageFile.Length > 0)
			{
				model.Image = SaveUpload(vm.ImageFile, "cleaners");
			}
			else
			{
				model.Image = model.Image ?? string.Empty;
			}
			model.FatherName = model.FatherName ?? string.Empty;
			model.Description = model.Description ?? string.Empty;
			model.Status = string.IsNullOrWhiteSpace(model.Status) ? "INC" : model.Status;
			model.StatusMessage = string.IsNullOrWhiteSpace(model.StatusMessage) ? "In Process...." : model.StatusMessage;
			model.CompanyId = companyId.Value;
			model.CreatedBy = userId.Value;
			model.CreatedDate = DateTime.UtcNow;

			var newId = _service.Create(model);
			if (newId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Failed to create cleaner.");
				return View(vm);
			}

			// Persist documents
			if (vm.Documents != null && vm.Documents.Count > 0)
			{
				for (int i = 0; i < vm.Documents.Count; i++)
				{
					var d = vm.Documents[i];
					if (d == null || d.IsDeleted) continue;
					d.CleanerId = newId;
					d.CompanyId = companyId.Value;
					d.SchoolId = model.SchoolId;
					d.CreatedBy = userId.Value;
					d.CreatedDate = DateTime.UtcNow;
					d.Status = d.Status ?? "INC";
					d.StatusMessage = d.StatusMessage ?? "In Process....";
					if (vm.DocumentFiles != null && i < vm.DocumentFiles.Count && vm.DocumentFiles[i] != null)
					{
						var saved = SaveUpload(vm.DocumentFiles[i], "cleaners");
						d.FileName = saved;
					}
					_docService.Create(d);
				}
			}

			// Persist qualifications
			if (vm.Qualifications != null && vm.Qualifications.Count > 0)
			{
				foreach (var q in vm.Qualifications)
				{
					if (q == null || q.IsDeleted) continue;
					q.CleanerId = newId;
					q.CompanyId = companyId.Value;
					q.SchoolId = model.SchoolId;
					q.CreatedBy = userId.Value;
					q.CreatedDate = DateTime.UtcNow;
					q.Status = q.Status ?? "INC";
					q.StatusMessage = q.StatusMessage ?? "In Process....";
					_qualService.Create(q);
				}
			}

			return RedirectToAction("Details", new { id = newId });
		}

		[HttpGet]
		[Route("Edit/{id}")]
		public IActionResult Edit(Guid id)
		{
			var item = _service.GetById(id);
			if (item == null) return NotFound();
			var vm = new CleanerAggregateViewModel { Master = item };
			try
			{
				var quals = _lookup.GetQualifications() ?? new List<LookupItem>();
				vm.QualificationItems = quals.Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Name, Selected = q.Id == item.CompanyId }).ToList();
				vm.Documents = (_docService.GetAll() ?? new List<CleanerDocumentDetails>()).Where(d => d.CleanerId == id && !d.IsDeleted).ToList();
				vm.Qualifications = (_qualService.GetAll() ?? new List<CleanerQualificationDetails>()).Where(q => q.CleanerId == id && !q.IsDeleted).ToList();
			}
			catch { }
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, CleanerAggregateViewModel vm)
		{
			if (vm?.Master == null || vm.Master.Id == Guid.Empty || vm.Master.Id != id) return BadRequest();
			var model = vm.Master;

			// Validate child rows
			if (vm.Documents != null)
			{
				for (int i = 0; i < vm.Documents.Count; i++)
				{
					var d = vm.Documents[i];
					if (d == null || d.IsDeleted) continue;
					if (string.IsNullOrWhiteSpace(d.Name))
					{
						ModelState.AddModelError($"Documents[{i}].Name", "Document name is required.");
					}
				}
			}
			if (vm.Qualifications != null)
			{
				for (int i = 0; i < vm.Qualifications.Count; i++)
				{
					var q = vm.Qualifications[i];
					if (q == null || q.IsDeleted) continue;
					if (q.QualificationId == Guid.Empty)
					{
						ModelState.AddModelError($"Qualifications[{i}].QualificationId", "Qualification is required.");
					}
				}
			}

			if (!ModelState.IsValid)
			{
				try
				{
					var quals = _lookup.GetQualifications() ?? new List<LookupItem>();
					vm.QualificationItems = quals.Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Name, Selected = q.Id == model.CompanyId }).ToList();
				}
				catch { }
				return View(vm);
			}

			var userId = CurrentUserId;
			var companyId = CurrentCompanyId;
			if (!userId.HasValue || !companyId.HasValue || model.SchoolId == Guid.Empty)
			{
				ModelState.AddModelError(string.Empty, "Please login to update cleaner.");
				return View(vm);
			}

			// Normalize optional strings
			model.Name = model.Name ?? string.Empty;
			if (vm.ImageFile != null && vm.ImageFile.Length > 0)
			{
				model.Image = SaveUpload(vm.ImageFile, "cleaners");
			}
			else
			{
				model.Image = model.Image ?? string.Empty;
			}
			model.FatherName = model.FatherName ?? string.Empty;
			model.Description = model.Description ?? string.Empty;
			model.Status = string.IsNullOrWhiteSpace(model.Status) ? "INC" : model.Status;
			model.StatusMessage = string.IsNullOrWhiteSpace(model.StatusMessage) ? "In Process...." : model.StatusMessage;
			model.ModifiedBy = userId.Value;
			model.ModifiedDate = DateTime.UtcNow;

			if (!_service.Update(model))
			{
				ModelState.AddModelError(string.Empty, "Failed to update cleaner.");
				return View(vm);
			}

			// Upsert/Delete docs
			if (vm.Documents != null && vm.Documents.Count > 0)
			{
				for (int i = 0; i < vm.Documents.Count; i++)
				{
					var d = vm.Documents[i];
					if (d == null) continue;
					d.CleanerId = model.Id;
					d.CompanyId = companyId.Value;
					d.SchoolId = model.SchoolId;
					var hasNewFile = vm.DocumentFiles != null && i < vm.DocumentFiles.Count && vm.DocumentFiles[i] != null;
					if (d.Id == Guid.Empty)
					{
						if (d.IsDeleted) continue;
						d.CreatedBy = userId.Value;
						d.CreatedDate = DateTime.UtcNow;
						d.Status = d.Status ?? "INC";
						d.StatusMessage = d.StatusMessage ?? "In Process....";
						if (hasNewFile)
						{
							var saved = SaveUpload(vm.DocumentFiles![i], "cleaners");
							d.FileName = saved;
						}
						_docService.Create(d);
					}
					else
					{
						if (d.IsDeleted) { _docService.Delete(d.Id); continue; }
						d.ModifiedBy = userId.Value;
						d.ModifiedDate = DateTime.UtcNow;
						if (hasNewFile)
						{
							var saved = SaveUpload(vm.DocumentFiles![i], "cleaners");
							d.FileName = saved;
						}
						_docService.Update(d);
					}
				}
			}

			// Upsert/Delete quals
			if (vm.Qualifications != null && vm.Qualifications.Count > 0)
			{
				foreach (var q in vm.Qualifications)
				{
					if (q == null) continue;
					q.CleanerId = model.Id;
					q.CompanyId = companyId.Value;
					q.SchoolId = model.SchoolId;
					if (q.Id == Guid.Empty)
					{
						if (q.IsDeleted) continue;
						q.CreatedBy = userId.Value;
						q.CreatedDate = DateTime.UtcNow;
						q.Status = q.Status ?? "INC";
						q.StatusMessage = q.StatusMessage ?? "In Process....";
						_qualService.Create(q);
					}
					else
					{
						if (q.IsDeleted) { _qualService.Delete(q.Id); continue; }
						q.ModifiedBy = userId.Value;
						q.ModifiedDate = DateTime.UtcNow;
						_qualService.Update(q);
					}
				}
			}

			return RedirectToAction("Details", new { id = model.Id });
		}

		private string SaveUpload(IFormFile file, string folder)
		{
			if (file == null || file.Length == 0) return string.Empty;
			var root = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
			var uploads = Path.Combine(root, folder);
			if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
			var name = $"{Guid.NewGuid():N}_{Path.GetFileName(file.FileName)}";
			var full = Path.Combine(uploads, name);
			using (var stream = System.IO.File.Create(full))
			{
				file.CopyTo(stream);
			}
			return $"/{folder}/{name}";
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
				TempData["ErrorMessage"] = "Failed to delete cleaner.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}
	}
}
