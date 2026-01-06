using Microsoft.AspNetCore.Mvc;
using SchoolPortal.Services.IServices;

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
        public IActionResult FeeStructure()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ReceivePayment()
        {
            return View();
        }

        private readonly IUserDetailsService _userDetailsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountsController(IUserDetailsService userDetailsService, IHttpContextAccessor httpContextAccessor)
        {
            _userDetailsService = userDetailsService ?? throw new ArgumentNullException(nameof(userDetailsService));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        [HttpGet]
        [Route("Account/GetUserProfile")]
        public async Task<IActionResult> GetUserProfile()
        {
            try
            {
                var userName = _httpContextAccessor.HttpContext?.Session.GetString("UserName");
                if (string.IsNullOrEmpty(userName))
                {
                    return Unauthorized(new { message = "User not authenticated" });
                }

                var users = _userDetailsService.GetAll();
                var user = users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

                if (user == null)
                {
                    return NotFound(new { message = "User not found" });
                }

                return Json(new 
                {
                    userId = user.Id,
                    userName = user.UserName,
                    email = user.EmailAddress,
                    fullName = user.FullName,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    isActive = user.IsActive,
                    designation = user.DesignationName,
                    role = user.RoleName,
                    //address = user.Address,
                    //city = user.City,
                    //state = user.State
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching user profile", error = ex.Message });
            }
        }
    }
}
