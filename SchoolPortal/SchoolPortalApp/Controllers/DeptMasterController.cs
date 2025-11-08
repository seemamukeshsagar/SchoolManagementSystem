using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using System.Threading.Tasks;

namespace SchoolPortalApp.Controllers
{
	[Route("DeptMaster")]
	public class DeptMasterController : Controller
	{
		private readonly IDeptMasterService _service;
		private readonly ISchoolService _schoolService;
		private readonly ILogger<DeptMasterController> _logger;

		public DeptMasterController(IDeptMasterService service, ISchoolService schoolService, ILogger<DeptMasterController> logger)
		{
			_service = service;
			_schoolService = schoolService;
			_logger = logger;
		}

		private void PopulateDropdowns(DeptMasterViewModel vm)
		{
			var schools = _schoolService.GetAll();
			vm.Schools = schools.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
			{
				Value = s.Id.ToString(),
				Text = s.Name,
				Selected = s.Id == vm.SchoolId
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
				return new DeptMasterListItemViewModel
				{
					Id = item.Id,
					DeptCode = item.DeptCode,
					DeptName = item.DeptName,
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
			var vm = new DeptMasterViewModel();
			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(DeptMasterViewModel model)
		{
			var schoolIdStr = HttpContext.Session.GetString("SchoolId");
			if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var parsedSchoolId))
			{
				ModelState.Remove(nameof(DeptMasterViewModel.SchoolId));
				model.SchoolId = parsedSchoolId;
			}

			if (!ModelState.IsValid)
			{
				PopulateDropdowns(model);
				return View(model);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			var companyIdStr = HttpContext.Session.GetString("CompanyId");

			if (string.IsNullOrEmpty(companyIdStr) || string.IsNullOrEmpty(schoolIdStr) || string.IsNullOrEmpty(userIdStr))
			{
				ModelState.AddModelError(string.Empty, "Missing required session data.");
				PopulateDropdowns(model);
				return View(model);
			}

			if (Guid.TryParse(companyIdStr, out var companyId) && 
				Guid.TryParse(schoolIdStr, out var schoolId) && 
				Guid.TryParse(userIdStr, out var userId))
			{
				var entity = new DeptMaster
				{
					Id = Guid.Empty,
					DeptCode = model.DeptCode,
					DeptName = model.DeptName,
					IsActive = model.IsActive,
					CompanyId = companyId,
					SchoolId = schoolId,
					CreatedBy = userId,
					CreatedDate = DateTime.UtcNow
				};

				var newId = _service.Create(entity);
				if (newId == Guid.Empty)
				{
					ModelState.AddModelError(string.Empty, "Failed to create department.");
					PopulateDropdowns(model);
					return View(model);
				}
				return RedirectToAction("Details", new { id = newId });
			}
			
			// If we get here, there was an error parsing the GUIDs
			ModelState.AddModelError(string.Empty, "Invalid session data format.");
			PopulateDropdowns(model);
			return View(model);
		}

		[HttpGet]
		[Route("Edit/{id}")]
		public IActionResult Edit(Guid id)
		{
			var item = _service.GetById(id);
			if (item == null) return NotFound();

			var vm = new DeptMasterViewModel
			{
				Id = item.Id,
				DeptCode = item.DeptCode,
				DeptName = item.DeptName,
				IsActive = item.IsActive,
				SchoolId = item.SchoolId
			};

			PopulateDropdowns(vm);
			return View(vm);
		}

		[HttpPost]
		[Route("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, DeptMasterViewModel model)
		{
			if (id != model.Id) return BadRequest();

			var schoolIdStr = HttpContext.Session.GetString("SchoolId");
			if (!string.IsNullOrWhiteSpace(schoolIdStr) && Guid.TryParse(schoolIdStr, out var schoolIdFromSession))
			{
				ModelState.Remove(nameof(DeptMasterViewModel.SchoolId));
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
				ModelState.AddModelError(string.Empty, "Please login to update department.");
				PopulateDropdowns(model);
				return View(model);
			}

			var entity = new DeptMaster
			{
				Id = id,
				DeptCode = model.DeptCode,
				DeptName = model.DeptName,
				IsActive = model.IsActive,
				SchoolId = model.SchoolId,
				ModifiedBy = userId,
				ModifiedDate = DateTime.UtcNow
			};

			if (!_service.Update(entity))
			{
				ModelState.AddModelError(string.Empty, "Failed to update department.");
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
				TempData["ErrorMessage"] = "Failed to delete department.";
				return RedirectToAction("Delete", new { id });
			}
			return RedirectToAction("Index");
		}

		[HttpGet]
		[Route("ExportToExcel")]
		public IActionResult ExportToExcel()
		{
			var departments = _service.GetAll();
			var schools = _schoolService.GetAll();

			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("Departments");
				var currentRow = 1;

				// Header
				worksheet.Cell(currentRow, 1).Value = "Department Code";
				worksheet.Cell(currentRow, 2).Value = "Department Name";
				worksheet.Cell(currentRow, 3).Value = "School";
				worksheet.Cell(currentRow, 4).Value = "Is Active";

				// Format header
				var headerRange = worksheet.Range(1, 1, 1, 4);
				headerRange.Style.Font.Bold = true;
				headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

				// Data
				foreach (var dept in departments)
				{
					currentRow++;
					var school = schools.FirstOrDefault(s => s.Id == dept.SchoolId);

					worksheet.Cell(currentRow, 1).Value = dept.DeptCode;
					worksheet.Cell(currentRow, 2).Value = dept.DeptName;
					worksheet.Cell(currentRow, 3).Value = school?.Name ?? string.Empty;
					worksheet.Cell(currentRow, 4).Value = dept.IsActive ? "Yes" : "No";
				}

				// Auto-fit columns
				worksheet.Columns().AdjustToContents();

				using (var stream = new MemoryStream())
				{
					workbook.SaveAs(stream);
					var content = stream.ToArray();
					return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Departments.xlsx");
				}
			}
		}

		[HttpGet]
		[Route("Import")]
		public IActionResult Import()
		{
			return View(new DeptImportViewModel());
		}

		[HttpGet]
		[Route("DownloadTemplate")]
		public IActionResult DownloadTemplate()
		{
			var schools = _schoolService.GetAll();

			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("Departments");
				var currentRow = 1;

				// Header with formatting
				worksheet.Cell(currentRow, 1).Value = "Department Code";
				worksheet.Cell(currentRow, 2).Value = "Department Name";
				worksheet.Cell(currentRow, 3).Value = "School Name";
				worksheet.Cell(currentRow, 4).Value = "Is Active (Yes/No)";

				// Format header
				var headerRange = worksheet.Range(1, 1, 1, 4);
				headerRange.Style.Font.Bold = true;
				headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
				headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

				// Set column widths
				worksheet.Column(1).Width = 20;
				worksheet.Column(2).Width = 30;
				worksheet.Column(3).Width = 30;
				worksheet.Column(4).Width = 20;

				// Add data validation for IsActive column
				var activeValidation = worksheet.Range("D2:D1000").CreateDataValidation();
				activeValidation.AllowedValues = XLAllowedValues.List;
				activeValidation.InCellDropdown = true;
				activeValidation.List(string.Join(",", new[] { "Yes", "No" }));

				// Add data validation for School Name column
				if (schools.Any())
				{
					var schoolNames = schools.Select(s => s.Name).ToArray();
					var schoolValidation = worksheet.Range("C2:C1000").CreateDataValidation();
					schoolValidation.AllowedValues = XLAllowedValues.List;
					schoolValidation.InCellDropdown = true;
					schoolValidation.List(string.Join(",", schoolNames));
				}

				// Add some example data
				currentRow++;
				worksheet.Cell(currentRow, 1).Value = "DEPT001";
				worksheet.Cell(currentRow, 2).Value = "Computer Science";
				worksheet.Cell(currentRow, 3).Value = schools.FirstOrDefault()?.Name ?? "";
				worksheet.Cell(currentRow, 4).Value = "Yes";

				currentRow++;
				worksheet.Cell(currentRow, 1).Value = "DEPT002";
				worksheet.Cell(currentRow, 2).Value = "Mathematics";
				if (schools.Count > 1)
					worksheet.Cell(currentRow, 3).Value = schools[1].Name;
				worksheet.Cell(currentRow, 4).Value = "Yes";

				// Add instructions
				currentRow += 2;
				worksheet.Cell(currentRow, 1).Value = "Instructions:";
				worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
				currentRow++;
				worksheet.Cell(currentRow, 1).Value = "1. Fill in the department details in the rows below";
				currentRow++;
				worksheet.Cell(currentRow, 1).Value = "2. School Name must match an existing school";
				currentRow++;
				worksheet.Cell(currentRow, 1).Value = "3. Is Active must be either 'Yes' or 'No'";
				currentRow++;
				worksheet.Cell(currentRow, 1).Value = "4. Do not modify or remove the header row";

				// Adjust row height for instructions
				for (int i = 1; i <= 4; i++)
				{
					worksheet.Row(currentRow - 3 + i).Height = 20;
				}

				// Protect the header row
				worksheet.SheetView.Freeze(1, 0);

				using (var stream = new MemoryStream())
				{
					workbook.SaveAs(stream);
					var content = stream.ToArray();
					return File(content,
						"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
						"Department_Import_Template.xlsx");
				}
			}
		}

		private string GetInnerExceptionMessages(Exception ex)
		{
			var messages = new List<string>();
			Exception? current = ex;
			while (current != null)
			{
				messages.Add(current.Message);
				current = current.InnerException;
			}
			return string.Join(" ", messages);
		}

		[HttpPost]
		[Route("Import")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Import(DeptImportViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userIdStr = HttpContext.Session.GetString("UserId");
			var companyIdStr = HttpContext.Session.GetString("CompanyId");

			if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId) ||
				string.IsNullOrWhiteSpace(companyIdStr) || !Guid.TryParse(companyIdStr, out var companyId))
			{
				ModelState.AddModelError("", "User session expired. Please login again.");
				return View(model);
			}

			if (model.ExcelFile == null || model.ExcelFile.Length == 0)
			{
				ModelState.AddModelError("", "Please select a file to upload.");
				return View(model);
			}

			// Check file extension
			var fileExtension = Path.GetExtension(model.ExcelFile.FileName).ToLowerInvariant();
			if (fileExtension != ".xlsx" && fileExtension != ".xls")
			{
				ModelState.AddModelError("", "Please upload a valid Excel file (.xlsx or .xls).");
				return View(model);
			}

			try
			{
				var departments = new List<DeptMaster>();
				var schools = _schoolService.GetAll().ToList();

				using (var memoryStream = new MemoryStream())
				{
					// Copy the file to a memory stream
					await model.ExcelFile.CopyToAsync(memoryStream);
			
					// Check if the file is empty
					if (memoryStream.Length == 0)
					{
						ModelState.AddModelError("", "The uploaded file is empty.");
						return View(model);
					}

					// Reset the position to the beginning of the stream
					memoryStream.Position = 0;

					try
					{
						// Ensure the stream is at the beginning
						memoryStream.Position = 0;

						try
						{
							using (var workbook = new XLWorkbook(memoryStream))
							{
								var worksheet = workbook.Worksheet(1) ?? workbook.Worksheet(0);
								if (worksheet == null)
								{
									ModelState.AddModelError("", "The Excel file does not contain any worksheets.");
									return View(model);
								}

								var rows = worksheet.RowsUsed().Skip(1); // Skip header row

								foreach (var row in rows)
								{
									var deptCode = row.Cell(1).GetString()?.Trim() ?? "";
									var deptName = row.Cell(2).GetString()?.Trim() ?? "";
									var schoolName = row.Cell(3).GetString()?.Trim() ?? "";
									var isActive = row.Cell(4).GetString()?.Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase) ?? false;

									if (string.IsNullOrEmpty(deptCode) || string.IsNullOrEmpty(deptName) || string.IsNullOrEmpty(schoolName))
										continue;

									var school = schools.FirstOrDefault(s =>
										s.Name.Equals(schoolName, StringComparison.OrdinalIgnoreCase));
									if (school == null) continue;

									var dept = new DeptMaster
									{
										Id = Guid.NewGuid(),
										DeptCode = deptCode,
										DeptName = deptName,
										SchoolId = school.Id,
										IsActive = isActive,
										CreatedBy = userId,
										CompanyId = companyId
									};

									departments.Add(dept);
								}
							}
						}
						catch (FileFormatException ex) when (ex.Message.Contains("corrupted"))
						{
							_logger.LogError(ex, "Corrupted Excel file detected");
							ModelState.AddModelError("", "The uploaded Excel file appears to be corrupted. Please ensure it's a valid Excel file and try again.");
							return View(model);
						}
					}
					catch (FileFormatException ex)
					{
						_logger.LogError(ex, "Invalid Excel file format");
						ModelState.AddModelError("", "The uploaded file is not a valid Excel file or is corrupted. Please upload a valid .xlsx or .xls file.");
						return View(model);
					}
					catch (IOException ex)
					{
						_logger.LogError(ex, "I/O error while reading Excel file");
						ModelState.AddModelError("", "An error occurred while reading the file. Please try again or use a different file.");
						return View(model);	
					}
					catch (Exception ex)
					{
						_logger.LogError(ex, "Error processing Excel file");
						// Try to get more specific error information
						string errorDetails = GetInnerExceptionMessages(ex);
						_logger.LogError("Error details: {ErrorDetails}", errorDetails);
						
						// Provide a more specific error message if possible
						if (errorDetails.Contains("corrupted", StringComparison.OrdinalIgnoreCase))
						{
							ModelState.AddModelError("", "The uploaded file appears to be corrupted. Please ensure it's a valid Excel file and try again.");
						}
						else
						{
							ModelState.AddModelError("", $"An error occurred while processing the file: {errorDetails}");
						}
						
						return View(model);
					}
				}

				if (departments.Any())
				{
					_service.BulkInsert(departments);
					return RedirectToAction("Index", new { message = "Departments imported successfully!" });
				}

				ModelState.AddModelError("", "No valid departments found in the Excel file.");
				return View(model);
			}
			catch (Exception ex)
			{
				// Log the full exception details for debugging
				_logger.LogError(ex, "Error in Import action");
				string errorDetails = GetInnerExceptionMessages(ex);
				_logger.LogError("Full error details: {ErrorDetails}", errorDetails);
				_logger.LogError(ex, "Error importing departments from Excel");
				ModelState.AddModelError("", "An error occurred while importing departments. Please check the file format and try again.");
				return View(model);
			}
		}
	}
}