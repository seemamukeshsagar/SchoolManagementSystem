using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;
using SchoolPortalApp.Models;
using Microsoft.AspNetCore.Hosting; 

namespace SchoolPortalApp.Controllers
{
    [Route("TimeTable")]
    public class TimeTableController : BaseController
    {
        private readonly ITimeTableSetupDetailsService _timeTableSetupService;
        private readonly ITimeTablePeriodService _timeTablePeriodService;
        private readonly IClassService _classService;
        private readonly ISubjectService _subjectService;
        private readonly IAcademicYearService _academicYearService;
        private readonly ISectionService _sectionService;
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TimeTableController> _logger;

        private readonly IWebHostEnvironment _hostEnvironment; 

        public TimeTableController(
            ITimeTablePeriodService timeTablePeriodService,
            ITimeTableSetupDetailsService timeTableSetupService,
            IClassService classService,
            ISubjectService subjectService,
            IAcademicYearService academicYearService,
            ISectionService sectionService,
            ITeacherService teacherService,
            ILogger<TimeTableController> logger,
            IWebHostEnvironment hostEnvironment)
        {
            _timeTableSetupService = timeTableSetupService ?? throw new ArgumentNullException(nameof(timeTableSetupService));
            _timeTablePeriodService = timeTablePeriodService ?? throw new ArgumentNullException(nameof(timeTablePeriodService));
            _classService = classService ?? throw new ArgumentNullException(nameof(classService));
            _subjectService = subjectService ?? throw new ArgumentNullException(nameof(subjectService));
            _academicYearService = academicYearService ?? throw new ArgumentNullException(nameof(academicYearService));
            _sectionService = sectionService ?? throw new ArgumentNullException(nameof(sectionService));
            _teacherService = teacherService ?? throw new ArgumentNullException(nameof(teacherService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
             _hostEnvironment = hostEnvironment;  
        }

        public async Task<IActionResult> Index(TimeTableFilterViewModel filter)
        {
            try
            {
                // Get all classes and first available class ID to populate sections
                var allClasses = _classService.GetAll();
                var firstClass = allClasses.FirstOrDefault();
                var classId = firstClass?.Id ?? Guid.Empty;
                
                var model = new TimeTableViewModel
                {
                    Classes = allClasses
                        .Select(c => new SelectListItem 
                        { 
                            Value = c.Id.ToString(), 
                            Text = c.Name 
                        })
                        .OrderBy(c => c.Text)
                        .ToList(),
                    Sections = classId != Guid.Empty ? await GetSectionListItemsAsync(classId) : new List<SelectListItem>(),
                    AcademicYears = await GetAcademicYearListItemsAsync()
                };

                if (filter.ClassId.HasValue)
                {
                    model.Sections = await GetSectionListItemsAsync(filter.ClassId.Value);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading TimeTable index page");
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Route("GetSectionsByClass/{classId}")]
        public async Task<IActionResult> GetSectionsByClass(Guid classId)
        {
            try
            {
                _logger.LogInformation("Getting sections for class {ClassId}", classId);
                
                if (classId == Guid.Empty)
                {
                    return BadRequest(new { error = "Class ID is required" });
                }

                var sections = await _sectionService.GetByClassIdAsync(classId);
                
                var result = sections
                    .Where(s => s != null)
                    .Select(s => new 
                    { 
                        value = s.Id.ToString(), 
                        text = s.Name ?? $"Section {s.Id}"
                    })
                    .OrderBy(s => s.text)
                    .ToList();

                return new JsonResult(result, new System.Text.Json.JsonSerializerOptions 
                {
                    PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sections for class {ClassId}", classId);
                return StatusCode(StatusCodes.Status500InternalServerError, new 
                { 
                    error = "An error occurred while loading sections",
                    details = _hostEnvironment.IsDevelopment() ? ex.Message : null
                });
            }
        }
      

        [HttpGet]
        [Route("Generate")]
        public async Task<IActionResult> Generate(Guid classId, Guid sectionId, Guid academicYearId)
        {
            try
            {
                if (classId == Guid.Empty || sectionId == Guid.Empty || academicYearId == Guid.Empty)
                {
                    TempData["ErrorMessage"] = "Please select all required fields.";
                    return RedirectToAction(nameof(Index));
                }

                var timeTable = await BuildTimeTableViewModel(classId, sectionId, academicYearId);
                return View(timeTable);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating timetable for Class: {ClassId}, Section: {SectionId}, Year: {AcademicYearId}", 
                    classId, sectionId, academicYearId);
                TempData["ErrorMessage"] = "An error occurred while generating the timetable.";
                return RedirectToAction(nameof(Index));
            }
        }

        #region Private Helper Methods

        private async Task<TimeTableViewModel> BuildTimeTableViewModel(Guid classId, Guid sectionId, Guid academicYearId)
        {
            var classInfo = await _classService.GetByIdAsync(classId);
            var section = await _sectionService.GetByIdAsync(sectionId);
            var academicYear = await _academicYearService.GetByIdAsync(academicYearId);

            if (classInfo == null || section == null || academicYear == null)
            {
                throw new InvalidOperationException("Invalid class, section, or academic year");
            }

            var timeTable = new TimeTableViewModel
            {
                ClassId = classId,
                SectionId = sectionId,
                AcademicYearId = academicYearId,
                ClassName = classInfo.Name,
                SectionName = section.Name,
                AcademicYearName = academicYear.AcademicYearName,
                EffectiveFrom = DateTime.Today,
                IsActive = true
            };

            var setup = await _timeTableSetupService.GetLatestSetupAsync(classId, sectionId, academicYearId);
            if (setup == null)
            {
                throw new InvalidOperationException("No timetable setup found for the selected class, section, and academic year. Please create a timetable setup first.");
            }

            var periods = await _timeTablePeriodService.GetBySetupIdAsync(setup.Id);
            if (periods == null)
            {
                throw new InvalidOperationException("No periods found for the timetable setup. Please configure periods first.");
            }

            // Add days and periods to the timetable
            // ... (your existing day/period logic here)

            return timeTable;
        }

        private async Task<IList<SelectListItem>> GetSectionListItemsAsync(Guid classId)
        {
            var sections = await _sectionService.GetByClassIdAsync(classId);
            return sections
                .Where(s => s != null)
                .Select(s => new SelectListItem 
                { 
                    Value = s.Id.ToString(), 
                    Text = s.Name 
                })
                .ToList() ?? new List<SelectListItem>();
        }

        private async Task<IList<SelectListItem>> GetAcademicYearListItemsAsync()
        {
            var academicYears = await _academicYearService.GetAllActiveAsync();
            return academicYears
                .Select(a => new SelectListItem 
                { 
                    Value = a.Id.ToString(), 
                    Text = a.AcademicYearName 
                })
                .OrderByDescending(a => a.Text)
                .ToList();
        }

        #endregion
    }
}

