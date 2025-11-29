using Microsoft.AspNetCore.Mvc;

namespace SchoolPortalApp.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RecentTransactions()
        {
            return View();
        }

        [HttpGet]
        public IActionResult FinancialSummary()
        {
            return View();
        }
    }
}
