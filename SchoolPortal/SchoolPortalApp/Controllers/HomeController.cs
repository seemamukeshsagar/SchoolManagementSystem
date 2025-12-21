using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolPortal.Services.IServices;
using SchoolPortalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;

namespace SchoolPortalApp.Controllers
{
	[Route("Home")]
	public class HomeController : BaseController
	{
		private readonly IStudentService _studentService;
		private readonly IClassService _classService;
		private readonly ILookupService _lookupService;

		public HomeController(
			IStudentService studentService, 
			IClassService classService, 
			ILookupService lookupService,
			ILogger<HomeController> logger) : base(logger)
		{
			_studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
			_classService = classService ?? throw new ArgumentNullException(nameof(classService));
			_lookupService = lookupService ?? throw new ArgumentNullException(nameof(lookupService));
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		[Microsoft.AspNetCore.Authorization.AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var viewModel = await GetStudentGenderByClassDataAsync();
			return View(viewModel);
		}

		private async Task<StudentGenderByClassChartViewModel> GetStudentGenderByClassDataAsync()
		{
			try
			{
				// Get all students and classes asynchronously
				var allStudents = await _studentService.GetAllAsync().ConfigureAwait(false) ?? new List<StudentMaster>();
				var allClasses = await _classService.GetAllAsync().ConfigureAwait(false) ?? new List<ClassMaster>();

				var students = allStudents
					.Where(s => s != null && s.IsActive && !s.IsDeleted)
					.ToList();
						
				var classes = allClasses
					.Where(c => c != null && c.IsActive && !c.IsDeleted)
					.ToList();

				// Get gender lookup data
				var genders = _lookupService.GetGenders() ?? new List<LookupItem>();
				var lookup = genders.ToLookup(x => x.Id, x => x.Name);
				
				var maleGenderId = genders
					.Where(g => g != null && !string.IsNullOrEmpty(g.Name))
					.FirstOrDefault(g => g.Name.Equals("male", StringComparison.OrdinalIgnoreCase))?.Id ?? Guid.Empty;
				var femaleGenderId = genders
					.Where(g => g != null && !string.IsNullOrEmpty(g.Name))
					.FirstOrDefault(g => g.Name.Equals("female", StringComparison.OrdinalIgnoreCase))?.Id ?? Guid.Empty;

				// If no data available, use dummy data
				if (!students.Any() || !classes.Any() || maleGenderId == Guid.Empty || femaleGenderId == Guid.Empty)
				{
					return GetDummyData();
				}

				var viewModel = new StudentGenderByClassChartViewModel { HasData = true };

				// Group students by class and gender
				foreach (var classItem in classes.Where(c => c != null && !string.IsNullOrEmpty(c.Name)))
				{
					var classStudents = students
						.Where(s => s != null && s.ClassId == classItem.Id)
						.ToList();
					
					var boysCount = classStudents
						.Count(s => s.Gender.HasValue && s.Gender.Value == maleGenderId);
					var girlsCount = classStudents
						.Count(s => s.Gender.HasValue && s.Gender.Value == femaleGenderId);

					viewModel.Data.Add(new StudentGenderByClassViewModel
					{
						ClassName = classItem.Name!,
						BoysCount = boysCount,
						GirlsCount = girlsCount
					});
				}

				return viewModel;
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, "Error in GetStudentGenderByClassDataAsync");
				// If any error occurs, return dummy data
				return GetDummyData();
			}
		}

		private static StudentGenderByClassChartViewModel GetDummyData()
		{
			return new StudentGenderByClassChartViewModel
			{
				HasData = false,
				Data = new List<StudentGenderByClassViewModel>
				{
					new StudentGenderByClassViewModel { ClassName = "Class 1", BoysCount = 15, GirlsCount = 12 },
					new StudentGenderByClassViewModel { ClassName = "Class 2", BoysCount = 18, GirlsCount = 16 },
					new StudentGenderByClassViewModel { ClassName = "Class 3", BoysCount = 14, GirlsCount = 17 },
					new StudentGenderByClassViewModel { ClassName = "Class 4", BoysCount = 16, GirlsCount = 14 },
					new StudentGenderByClassViewModel { ClassName = "Class 5", BoysCount = 13, GirlsCount = 15 }
				}
			};
		}
	}
}