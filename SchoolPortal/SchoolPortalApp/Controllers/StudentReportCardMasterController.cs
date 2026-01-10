using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;
using SchoolPortal.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortalApp.Controllers
{
    [Route("StudentReportCardMaster")]
    public class StudentReportCardMasterController : BaseController
    {
        private readonly IStudentReportCardMasterService _studentReportCardMasterService;
        private readonly IStudentMasterService _studentService;
        private readonly IClassService _classService;
        private readonly ISectionService _sectionService;
        private readonly IAcademicYearService _academicYearService;
        private new readonly ILogger<StudentReportCardMasterController> _logger;

        public StudentReportCardMasterController(
            IStudentReportCardMasterService studentReportCardMasterService,
            IStudentMasterService studentService,
            IClassService classService,
            ISectionService sectionService,
            IAcademicYearService academicYearService,
            ILogger<StudentReportCardMasterController> logger) : base(logger)
        {
            _studentReportCardMasterService = studentReportCardMasterService ?? throw new ArgumentNullException(nameof(studentReportCardMasterService));
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
            _classService = classService ?? throw new ArgumentNullException(nameof(classService));
            _sectionService = sectionService ?? throw new ArgumentNullException(nameof(sectionService));
            _academicYearService = academicYearService ?? throw new ArgumentNullException(nameof(academicYearService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            try
            {
                var reportCards = _studentReportCardMasterService.GetAll() ?? new List<StudentReportCardMaster>();
                var students = _studentService.GetAll() ?? new List<StudentMaster>();
                var classes = _classService.GetAll() ?? new List<ClassMaster>();
                var sections = _sectionService.GetAll() ?? new List<SectionMaster>();
                var academicYears = _academicYearService.GetAll() ?? new List<AcademicYear>();

                var reportCardViewModels = reportCards.Select(reportCard => new StudentReportCardMasterViewModel
                {
                    Id = reportCard.Id,
                    StudentId = reportCard.StudentId,
                    StudentName = students.FirstOrDefault(s => s.Id == reportCard.StudentId)?.FirstName + " " + 
                                  students.FirstOrDefault(s => s.Id == reportCard.StudentId)?.LastName ?? string.Empty,
                    ClassId = reportCard.ClassId,
                    ClassName = classes.FirstOrDefault(c => c.Id == reportCard.ClassId)?.Name ?? string.Empty,
                    SectionId = reportCard.SectionId,
                    SectionName = sections.FirstOrDefault(s => s.Id == reportCard.SectionId)?.Name ?? string.Empty,
                    SessionId = reportCard.SessionId,
                    AcademicYearId = reportCard.SessionId, // Using SessionId as AcademicYearId
                    AcademicYearName = academicYears.FirstOrDefault(ay => ay.Id == reportCard.SessionId)?.AcademicYearName ?? string.Empty,
                    ReportCardType = reportCard.ReportCardType,
                    ExamType = reportCard.ReportCardValue, // Using ReportCardValue as ExamType
                    ReportCardValue = reportCard.ReportCardValue,
                    Period = reportCard.Period,
                    GeneratedDate = reportCard.CreatedDate, // Using CreatedDate as GeneratedDate
                    CreatedDate = reportCard.CreatedDate,
                    ModifiedDate = reportCard.ModifiedDate, // Using ModifiedDate as ModifiedDate
                    IsActive = reportCard.IsActive,
                    IsDeleted = reportCard.IsDeleted,
                    CompanyId = reportCard.CompanyId,
                    SchoolId = reportCard.SchoolId,
                    CreatedBy = reportCard.CreatedBy,
                    CreatedDateValue = reportCard.CreatedDate,
                    ModifiedBy = reportCard.ModifiedBy,
                    ModifiedDateValue = reportCard.ModifiedDate,
                    Status = reportCard.Status,
                    StatusMessage = reportCard.StatusMessage
                }).ToList();

                return View(reportCardViewModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving student report cards");
                TempData["ErrorMessage"] = "An error occurred while retrieving student report cards.";
                return View(new List<StudentReportCardMasterViewModel>());
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var reportCard = await _studentReportCardMasterService.GetByIdAsync(id);
                if (reportCard == null)
                {
                    return NotFound();
                }

                // Convert to ViewModel with navigation properties
                var students = _studentService.GetAll() ?? new List<StudentMaster>();
                var classes = _classService.GetAll() ?? new List<ClassMaster>();
                var sections = _sectionService.GetAll() ?? new List<SectionMaster>();
                var academicYears = _academicYearService.GetAll() ?? new List<AcademicYear>();

                var reportCardViewModel = new StudentReportCardMasterViewModel
                {
                    Id = reportCard.Id,
                    StudentId = reportCard.StudentId,
                    StudentName = students.FirstOrDefault(s => s.Id == reportCard.StudentId)?.FirstName + " " + 
                                  students.FirstOrDefault(s => s.Id == reportCard.StudentId)?.LastName ?? string.Empty,
                    ClassId = reportCard.ClassId,
                    ClassName = classes.FirstOrDefault(c => c.Id == reportCard.ClassId)?.Name ?? string.Empty,
                    SectionId = reportCard.SectionId,
                    SectionName = sections.FirstOrDefault(s => s.Id == reportCard.SectionId)?.Name ?? string.Empty,
                    SessionId = reportCard.SessionId,
                    AcademicYearId = reportCard.SessionId, // Using SessionId as AcademicYearId
                    AcademicYearName = academicYears.FirstOrDefault(ay => ay.Id == reportCard.SessionId)?.AcademicYearName ?? string.Empty,
                    ReportCardType = reportCard.ReportCardType,
                    ExamType = reportCard.ReportCardValue, // Using ReportCardValue as ExamType
                    ReportCardValue = reportCard.ReportCardValue,
                    TotalMarks = 0, // Not available in base entity
                    ObtainedMarks = 0, // Not available in base entity
                    Percentage = 0, // Not available in base entity
                    Grade = "", // Not available in base entity
                    Rank = 0, // Not available in base entity
                    Remarks = "", // Not available in base entity
                    Period = reportCard.Period,
                    GeneratedDate = reportCard.CreatedDate, // Using CreatedDate as GeneratedDate
                    CreatedDate = reportCard.CreatedDate,
                    ModifiedDate = reportCard.ModifiedDate, // Using ModifiedDate as ModifiedDate
                    IsActive = reportCard.IsActive,
                    IsDeleted = reportCard.IsDeleted,
                    CompanyId = reportCard.CompanyId,
                    SchoolId = reportCard.SchoolId,
                    CreatedBy = reportCard.CreatedBy,
                    CreatedDateValue = reportCard.CreatedDate,
                    ModifiedBy = reportCard.ModifiedBy,
                    ModifiedDateValue = reportCard.ModifiedDate,
                    Status = reportCard.Status,
                    StatusMessage = reportCard.StatusMessage
                };

                return View(reportCardViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving student report card details");
                TempData["ErrorMessage"] = "An error occurred while retrieving student report card details.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentReportCardMasterViewModel reportCardViewModel)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(reportCardViewModel);
            }

            try
            {
                // Convert ViewModel to entity for database operations
                var entity = new StudentReportCardMaster
                {
                    Id = Guid.NewGuid(),
                    StudentId = reportCardViewModel.StudentId,
                    ClassId = reportCardViewModel.ClassId,
                    SectionId = reportCardViewModel.SectionId,
                    SessionId = reportCardViewModel.SessionId,
                    ReportCardType = reportCardViewModel.ReportCardType,
                    ReportCardValue = reportCardViewModel.ReportCardValue,
                    Period = reportCardViewModel.Period,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true,
                    IsDeleted = false,
                    CompanyId = reportCardViewModel.CompanyId,
                    SchoolId = reportCardViewModel.SchoolId,
                    CreatedBy = reportCardViewModel.CreatedBy,
                    Status = "ACT",
                    StatusMessage = "Active"
                };
                
                await _studentReportCardMasterService.CreateAsync(entity);
                TempData["SuccessMessage"] = "Student report card created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating student report card");
                ModelState.AddModelError(string.Empty, "Failed to create student report card.");
                PopulateDropdowns();
                return View(new StudentReportCardMaster());
            }
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var reportCard = await _studentReportCardMasterService.GetByIdAsync(id);
                if (reportCard == null)
                {
                    return NotFound();
                }

                // Convert to ViewModel for the view
                var students = _studentService.GetAll() ?? new List<StudentMaster>();
                var classes = _classService.GetAll() ?? new List<ClassMaster>();
                var sections = _sectionService.GetAll() ?? new List<SectionMaster>();
                var academicYears = _academicYearService.GetAll() ?? new List<AcademicYear>();

                var reportCardViewModel = new StudentReportCardMasterViewModel
                {
                    Id = reportCard.Id,
                    StudentId = reportCard.StudentId,
                    ClassId = reportCard.ClassId,
                    SectionId = reportCard.SectionId,
                    SessionId = reportCard.SessionId,
                    ReportCardType = reportCard.ReportCardType,
                    ReportCardValue = reportCard.ReportCardValue,
                    Period = reportCard.Period,
                    CompanyId = reportCard.CompanyId,
                    SchoolId = reportCard.SchoolId,
                    CreatedBy = reportCard.CreatedBy,
                    ModifiedBy = reportCard.ModifiedBy,
                    Status = reportCard.Status,
                    StatusMessage = reportCard.StatusMessage
                };

                PopulateDropdowns();
                return View(reportCardViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving student report card for edit");
                TempData["ErrorMessage"] = "An error occurred while retrieving student report card.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, StudentReportCardMasterViewModel reportCardViewModel)
        {
            if (id != reportCardViewModel.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(reportCardViewModel);
            }

            try
            {
                // Convert ViewModel to entity for database operations
                var entity = new StudentReportCardMaster
                {
                    Id = reportCardViewModel.Id,
                    StudentId = reportCardViewModel.StudentId,
                    ClassId = reportCardViewModel.ClassId,
                    SectionId = reportCardViewModel.SectionId,
                    SessionId = reportCardViewModel.SessionId,
                    ReportCardType = reportCardViewModel.ReportCardType,
                    ReportCardValue = reportCardViewModel.ReportCardValue,
                    Period = reportCardViewModel.Period,
                    ModifiedDate = DateTime.UtcNow,
                    CompanyId = reportCardViewModel.CompanyId,
                    SchoolId = reportCardViewModel.SchoolId,
                    ModifiedBy = reportCardViewModel.ModifiedBy,
                    Status = reportCardViewModel.Status,
                    StatusMessage = reportCardViewModel.StatusMessage
                };
                
                await _studentReportCardMasterService.UpdateAsync(entity);
                TempData["SuccessMessage"] = "Student report card updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating student report card");
                ModelState.AddModelError(string.Empty, "Failed to update student report card.");
                PopulateDropdowns();
                return View(new StudentReportCardMaster());
            }
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var reportCard = await _studentReportCardMasterService.GetByIdAsync(id);
                if (reportCard == null)
                {
                    return NotFound();
                }

                await _studentReportCardMasterService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Student report card deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting student report card");
                TempData["ErrorMessage"] = "An error occurred while deleting student report card.";
                return RedirectToAction(nameof(Index));
            }
        }

        private void PopulateDropdowns()
        {
            try
            {
                ViewBag.Students = _studentService.GetAll()?.Select(s => new { Value = s.Id, Text = $"{s.FirstName} {s.LastName} ({s.RegistrationNumber})" }) ?? Enumerable.Empty<object>();
                ViewBag.Classes = _classService.GetAll()?.Select(c => new { Value = c.Id, Text = c.Name }) ?? Enumerable.Empty<object>();
                ViewBag.Sections = _sectionService.GetAll()?.Select(s => new { Value = s.Id, Text = s.Name }) ?? Enumerable.Empty<object>();
                ViewBag.AcademicYears = _academicYearService.GetAll()?.Select(a => new { Value = a.Id, Text = a.AcademicYearName }) ?? Enumerable.Empty<object>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error populating dropdowns");
                ViewBag.Students = new List<object>();
                ViewBag.Classes = new List<object>();
                ViewBag.Sections = new List<object>();
                ViewBag.AcademicYears = new List<object>();
            }
        }
    }
}
