using Microsoft.AspNetCore.Mvc;
using SchoolPortal.Services.IServices;
using SchoolPortalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolPortalApp.Controllers
{
	[Route("Home")]
	public class HomeController : BaseController
	{
		private readonly IStudentService _studentService;
		private readonly IClassService _classService;
		private readonly ILookupService _lookupService;

		public HomeController(IStudentService studentService, IClassService classService, ILookupService lookupService)
		{
			_studentService = studentService;
			_classService = classService;
			_lookupService = lookupService;
		}

		[HttpGet]
		[Route("")]
		[Route("Index")]
		[Microsoft.AspNetCore.Authorization.AllowAnonymous]
		public IActionResult Index()
		{
			var viewModel = GetStudentGenderByClassData();
			return View(viewModel);
		}

		private StudentGenderByClassChartViewModel GetStudentGenderByClassData()
		{
			try
			{
				// Get all students and classes
				var students = _studentService.GetAll().Where(s => s.IsActive && !s.IsDeleted).ToList();
				var classes = _classService.GetAll().Where(c => c.IsActive && !c.IsDeleted).ToList();

				// Get gender lookup data
				var genders = _lookupService.GetGenders();
				var maleGenderId = genders.FirstOrDefault(g => g.Name.ToLower() == "male")?.Id ?? Guid.Empty;
				var femaleGenderId = genders.FirstOrDefault(g => g.Name.ToLower() == "female")?.Id ?? Guid.Empty;

				// If no data available, use dummy data
				if (!students.Any() || !classes.Any() || maleGenderId == Guid.Empty || femaleGenderId == Guid.Empty)
				{
					return GetDummyData();
				}

				var viewModel = new StudentGenderByClassChartViewModel { HasData = true };

				// Group students by class and gender
				foreach (var classItem in classes)
				{
					var classStudents = students.Where(s => s.ClassId == classItem.Id).ToList();
					
					var boysCount = classStudents.Count(s => s.Gender.HasValue && s.Gender.Value == maleGenderId);
					var girlsCount = classStudents.Count(s => s.Gender.HasValue && s.Gender.Value == femaleGenderId);

					viewModel.Data.Add(new StudentGenderByClassViewModel
					{
						ClassName = classItem.Name,
						BoysCount = boysCount,
						GirlsCount = girlsCount
					});
				}

				return viewModel;
			}
			catch
			{
				// If any error occurs, return dummy data
				return GetDummyData();
			}
		}

		private StudentGenderByClassChartViewModel GetDummyData()
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