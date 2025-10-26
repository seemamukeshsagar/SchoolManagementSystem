using Microsoft.AspNetCore.Mvc;

namespace SchoolPortalApp.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
