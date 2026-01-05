using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolPortal.Services;

namespace SchoolPortalApp.Controllers
{
	[Authorize]
	public class ReportsController : Controller
	{
		private readonly IReportService _reportService;
		
		public ReportsController(IReportService reportService)
		{
			_reportService = reportService;
		}

		// Employee Reports
		public IActionResult AllEmployees()
		{
			return View();
		}

		public IActionResult EmpSalary()
		{
			return View();
		}

		public IActionResult EmpLeaves()
		{
			return View();
		}

		// Student Reports
		public IActionResult AllStudents()
		{
			return View();
		}

		public IActionResult FeesCollection()
		{
			return View();
		}

		public IActionResult FeesDefaulters()
		{
			return View();
		}

		// Inventory Reports
		public IActionResult AllItems()
		{
			return View();
		}

		public IActionResult Invoices()
		{
			return View();
		}

		public IActionResult Bills()
		{
			return View();
		}
	}
}