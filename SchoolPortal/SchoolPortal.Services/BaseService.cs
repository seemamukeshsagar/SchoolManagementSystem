using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SchoolPortal.Services
{
    public abstract class BaseService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected BaseService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        protected Guid GetCurrentCompanyId()
        {
            var companyId = _httpContextAccessor?.HttpContext?.User?.FindFirstValue("CompanyId");
            if (string.IsNullOrEmpty(companyId) || !Guid.TryParse(companyId, out var companyGuid))
            {
                throw new UnauthorizedAccessException("Company ID not found in user claims");
            }
            return companyGuid;
        }

        protected Guid GetCurrentSchoolId()
        {
            var schoolId = _httpContextAccessor?.HttpContext?.User?.FindFirstValue("SchoolId");
            if (string.IsNullOrEmpty(schoolId) || !Guid.TryParse(schoolId, out var schoolGuid))
            {
                throw new UnauthorizedAccessException("School ID not found in user claims");
            }
            return schoolGuid;
        }

        protected Guid GetCurrentUserId()
        {
            var userId = _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var userGuid))
            {
                throw new UnauthorizedAccessException("User ID not found in claims");
            }
            return userGuid;
        }
    }
}