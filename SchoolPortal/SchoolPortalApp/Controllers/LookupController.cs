using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolPortal.Services.IServices;

namespace SchoolPortalApp.Controllers
{
    [Route("Lookup")]
    public class LookupController : Controller
    {
        private readonly ILookupService _lookup;
        private readonly ISchoolService _schoolService;

        public LookupController(ILookupService lookup, ISchoolService schoolService)
        {
            _lookup = lookup;
            _schoolService = schoolService;
        }

        [HttpGet]
        [Route("GetCompanies")]
        public IActionResult GetCompanies()
        {
            var list = _lookup.GetCompanies().Select(c => new { id = c.Id, name = c.Name });
            return Ok(list);
        }

        [HttpGet]
        [Route("GetSchoolsByCompany")]
        public IActionResult GetSchoolsByCompany(Guid companyId)
        {
            var schools = _schoolService.GetByCompany(companyId)
                .Select(s => new { id = s.Id, name = s.Name });
            return Ok(schools);
        }

        [HttpGet]
        [Route("SetCompany")]
        public IActionResult SetCompany(Guid companyId)
        {
            HttpContext.Session.SetString("CompanyId", companyId.ToString());
            HttpContext.Session.SetString("SchoolId", string.Empty);
            return Ok(new { success = true });
        }

        [HttpGet]
        [Route("SetSchool")]
        public IActionResult SetSchool(Guid schoolId)
        {
            HttpContext.Session.SetString("SchoolId", schoolId.ToString());
            return Ok(new { success = true });
        }
    }
}
