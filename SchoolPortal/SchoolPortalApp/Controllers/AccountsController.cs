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

        [HttpGet]
        [Route("Account/GetUserProfile")]
        public async Task<IActionResult> GetUserProfile()
        {
            //var user = await _userManager.GetUserAsync(User);
            //if (user == null)
            //{
            //    return Unauthorized();
            //}

            //return Json(new 
            //{
            //    userId = user.Id,
            //    userName = user.UserName,
            //    email = user.Email
            //    // Add other user properties as needed
            //});
            return Unauthorized();
        }
    }
}
