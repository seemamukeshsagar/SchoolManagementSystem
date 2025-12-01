using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolPortalApp.Models;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Controllers
{
    [Route("TimeTable")]
    public class TimeTableController : BaseController
    {
        private readonly ITimeTableSetupDetailsService _timeTableSetupService;
        private readonly ITimeTablePeriodService _timeTablePeriodService;
        private readonly ITimeTablePeriodMasterService _timeTablePeriodMasterService;
        private readonly IClassService _classService;
        private readonly ISubjectService _subjectService;
        private readonly IAcademicYearService _academicYearService;
        private readonly ISectionService _sectionService;
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TimeTableController> _logger;

        public TimeTableController(
            ITimeTablePeriodService timeTablePeriodService,
            ITimeTableSetupDetailsService timeTableSetupService,
            ITimeTablePeriodMasterService timeTablePeriodMasterService,
            IClassService classService,
            ISubjectService subjectService,
            IAcademicYearService academicYearService,
            ISectionService sectionService,
            ITeacherService teacherService,
            ILogger<TimeTableController> logger)
        {
            _timeTableSetupService = timeTableSetupService;
            _timeTablePeriodService = timeTablePeriodService;
            _timeTablePeriodMasterService = timeTablePeriodMasterService;
            _classService = classService;
            _subjectService = subjectService;
            _academicYearService = academicYearService;
            _sectionService = sectionService;
            _teacherService = teacherService;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index(TimeTableFilterViewModel filter)
        {
            var model = new TimeTableViewModel
            {
                Classes = _classService.GetAllActive()
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }),
                    
                Sections = filter.ClassId.HasValue 
                    ? _sectionService.GetByClassId(filter.ClassId.Value)
                        .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                    : Enumerable.Empty<SelectListItem>(),
                    
                AcademicYears = _academicYearService.GetAllActive()
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.AcademicYearName })
            };

            if (filter.ClassId.HasValue)
            {
                model.Sections = _sectionService.GetByClassId(filter.ClassId.Value)
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                    .ToList();
            }

            return View(model);
        }

        [HttpGet]
        [Route("GetSectionsByClass")]
        public IActionResult GetSectionsByClass(Guid classId)
        {
            try
            {
                var sections = _sectionService.GetByClassId(classId)
                    .Select(s => new { value = s.Id.ToString(), text = s.Name });
                return Json(sections);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sections for class {ClassId}", classId);
                return Json(Enumerable.Empty<object>());
            }
        }

        [HttpGet]
        [Route("Generate")]
        public async Task<IActionResult> Generate(Guid classId, Guid sectionId, Guid academicYearId)
        {
            var timeTable = new TimeTableViewModel
            {
                ClassId = classId,
                SectionId = sectionId,
                AcademicYearId = academicYearId,
                ClassName = _classService.GetById(classId)?.Name ?? string.Empty,
                SectionName = _sectionService.GetById(sectionId)?.Name ?? string.Empty,
                AcademicYearName = _academicYearService.GetById(academicYearId)?.AcademicYearName ?? string.Empty,
                EffectiveFrom = DateTime.Today,
                IsActive = true
            };

            // Get the latest timetable setup
            var setup = _timeTableSetupService.GetAll()
                .OrderByDescending(s => s.CreatedDate)
                .FirstOrDefault();

            if (setup == null)
            {
                TempData["ErrorMessage"] = "No timetable setup found. Please create a timetable setup first.";
                return RedirectToAction("Index");
            }

            // Get all periods for the setup
            var periods = await _timeTablePeriodService.GetBySetupIdAsync(setup.Id);

            if (periods == null)
            {
                TempData["ErrorMessage"] = "No periods found for the timetable setup. Please configure periods first.";
                return RedirectToAction("Index");
            }

            // Ensure periods is a collection (e.g., List<TimeTableClassPeriodDetails>)
            var periodList = (periods as IEnumerable<TimeTableClassPeriodDetails>)?.ToList() ?? new List<TimeTableClassPeriodDetails>();

            var days = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            for (int i = 0; i < days.Length; i++)
            {
                var day = new TimeTableDayViewModel
                {
                    DayId = i + 1,
                    DayName = days[i],
                    Periods = periodList.Select(p => new TimeTablePeriodViewModel
                    {
                        Id = p.Id,
                        PeriodNumber = p.PeriodNumber,
                        //StartTime = p.StartTime,
                        //EndTime = p.EndTime,
                        IsBreak = p.IsBreak,
                        BreakName = p.BreakName
                    }).ToList()
                };

                timeTable.Days.Add(day);
            }

            // Get subjects for the class
            ViewBag.Subjects = _subjectService.GetByClassId(classId)
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.SubjectName })
                .ToList();

            // Get teachers
            ViewBag.Teachers = _teacherService.GetAllActive()
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = $"{t.FirstName} {t.LastName}" })
                .ToList();

            return View(timeTable);
        }

        [HttpPost]
        [Route("Save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TimeTableViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid data. Please check the form and try again.";
                return RedirectToAction("Index");
            }

            try
            {
                // Get current user ID (you'll need to implement this based on your authentication)
                //var currentUserId = GetCurrentUserId();
                var currentDate = DateTime.UtcNow;

                // Get the current session ID
                var currentSession = _academicYearService.GetById(model.AcademicYearId);
                if (currentSession == null)
                {
                    TempData["ErrorMessage"] = "No active academic session found.";
                    return RedirectToAction("Index");
                }

                // Get company and school IDs (you'll need to implement these based on your setup)
                var currentUserId = CurrentUserId ?? throw new UnauthorizedAccessException("User not authenticated");
                var companyId = CurrentCompanyId ?? throw new InvalidOperationException("Company ID not found");
                var schoolId = CurrentSchoolId ?? throw new InvalidOperationException("School ID not found");

                // Delete existing timetable for this class, section and academic year
                await _timeTablePeriodService.DeleteByClassSectionAndAcademicYearAsync(
                    model.ClassId, model.SectionId, model.AcademicYearId, currentUserId);

                // Save the timetable periods
                foreach (var day in model.Days)
                {
                    foreach (var period in day.Periods)
                    {
                        if (!period.IsBreak && period.SubjectId.HasValue && period.TeacherId.HasValue)
                        {
                            var timeTablePeriod = new TimeTableClassPeriodDetails
                            {
                                Id = Guid.NewGuid(),
                                ClassId = model.ClassId,
                                SectionId = model.SectionId,
                                SubjectId = period.SubjectId.Value,
                                PeriodId = period.Id,
                                DayOfWeek = day.DayId,
                                SessionId = currentSession.Id,
                                CompanyId = companyId,
                                SchoolId = schoolId,
                                IsActive = model.IsActive,
                                IsDeleted = false,
                                CreatedBy = currentUserId,
                                CreatedDate = currentDate,
                                Status = "COM",
                                StatusMessage = "Completed"
                            };

                            // Save the period
                            await _timeTablePeriodService.SaveAsync(timeTablePeriod);
                        }
                    }
                }

                TempData["SuccessMessage"] = "Timetable saved successfully.";
                return RedirectToAction("ViewTimetable", new { 
                    classId = model.ClassId, 
                    sectionId = model.SectionId, 
                    academicYearId = model.AcademicYearId 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving timetable");
                TempData["ErrorMessage"] = "An error occurred while saving the timetable. Please try again.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [Route("ViewTimetable")]
        public async Task<IActionResult> ViewTimetable(Guid classId, Guid sectionId, Guid academicYearId)
        {
            try
            {
                var model = new TimeTableViewModel
                {
                    ClassId = classId,
                    SectionId = sectionId,
                    AcademicYearId = academicYearId,
                    ClassName = _classService.GetById(classId)?.Name ?? string.Empty,
                    SectionName = _sectionService.GetById(sectionId)?.Name ?? string.Empty,
                    AcademicYearName = _academicYearService.GetById(academicYearId)?.AcademicYearName ?? string.Empty,
                    Days = new List<TimeTableDayViewModel>()
                };

                // Get the latest timetable setup
                var setup = _timeTableSetupService.GetAll()
                    .OrderByDescending(s => s.CreatedDate)
                    .FirstOrDefault();

                if (setup == null)
                {
                    TempData["ErrorMessage"] = "No timetable setup found.";
                    return RedirectToAction("Index");
                }

                // Get all periods for the setup
                var periods = await _timeTablePeriodService.GetBySetupIdAsync(setup.Id);

                if (periods == null)
                {       
                    TempData["ErrorMessage"] = "No periods found for the timetable setup.";
                    return RedirectToAction("Index");
                }

                // Get saved timetable periods
                var savedPeriods = await _timeTablePeriodService.GetByClassSectionAndAcademicYearAsync(
                    classId, sectionId, academicYearId);

                // Initialize days (Monday to Friday)
                var days = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
                
                for (int i = 0; i < days.Length; i++)
                {
                    var day = new TimeTableDayViewModel
                    {
                        DayId = i + 1,
                        DayName = days[i],
                        Periods = new List<TimeTablePeriodViewModel>()
                    };

                    var allPeriods = await _timeTablePeriodService.GetAllAsync();
                    foreach (var period in allPeriods)
                    {
                        var savedPeriod = savedPeriods.FirstOrDefault(p => 
                            p.PeriodId == period.Id && p.DayOfWeek == day.DayId);

                        day.Periods.Add(new TimeTablePeriodViewModel
                        {
                            Id = period.Id,
                            PeriodNumber = period.PeriodNumber,
                            //StartTime = period.StartTime,
                            //EndTime = period.EndTime,
                            SubjectId = savedPeriod?.SubjectId,
                            SubjectName = savedPeriod?.Subject?.SubjectName ?? string.Empty,
                            TeacherId = savedPeriod?.TeacherId,
                            TeacherName = savedPeriod?.Teacher != null
                                ? $"{savedPeriod.Teacher.FirstName} {savedPeriod.Teacher.LastName}"
                                : string.Empty,
                            IsBreak = period.IsBreak,
                            BreakName = period.BreakName
                        });
                    }

                    model.Days.Add(day);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error viewing timetable");
                TempData["ErrorMessage"] = "An error occurred while loading the timetable. Please try again.";
                return RedirectToAction("Index");
            }                
        }

        [HttpGet]
        [Route("ViewTimetablePrint")]
        public IActionResult ViewTimetablePrint(Guid classId, Guid sectionId, Guid academicYearId)
        {
            var model = new TimeTableViewModel
            {
                ClassId = classId,
                SectionId = sectionId,
                AcademicYearId = academicYearId,
                ClassName = _classService.GetById(classId)?.Name ?? string.Empty,
                SectionName = _sectionService.GetById(sectionId)?.Name ?? string.Empty,
                AcademicYearName = _academicYearService.GetById(academicYearId)?.AcademicYearName ?? string.Empty
            };

            return View(model);
        }
    }
}
