using Microsoft.AspNetCore.Mvc;

namespace SchoolPortalApp.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/Database")]
        public IActionResult Database()
        {
            return View("DatabaseError");
        }
    }
}
