using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SchoolPortal.Services.IServices;
using SchoolPortal.Entities.Models;

namespace SchoolPortalApp.Helpers
{
    public class UserHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserDetailsService _userService;
        private readonly IRoleMasterService _roleService;

        public UserHelper(
            IHttpContextAccessor httpContextAccessor,
            IUserDetailsService userService,
            IRoleMasterService roleService)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
        }

        public UserDetails? GetCurrentUser()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var id))
                return null;

            return _userService.GetById(id);
        }

        public RoleMaster? GetUserRole(UserDetails user)
        {
            if (user?.UserRoleId == null)
                return null;

            return _roleService.GetById(user.UserRoleId.Value);
        }
    }
}
