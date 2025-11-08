using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Linq;

namespace SchoolPortal.Services.Common
{
	public static class AuthorizedManager
	{
		private static IHttpContextAccessor? _httpContextAccessor;

		/// <summary>
		/// Configures the AuthorizedManager with an IHttpContextAccessor instance.
		/// </summary>
		/// <param name="httpContextAccessor">The HTTP context accessor.</param>
		public static void Configure(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor
				?? throw new ArgumentNullException(nameof(httpContextAccessor));
		}

		/// <summary>
		/// Checks whether the currently logged-in user has a specified privilege.
		/// </summary>
		/// <param name="privilegeName">The name of the privilege to check.</param>
		/// <returns>True if the user has the privilege; otherwise, false.</returns>
		public static bool HasPrivilege(string privilegeName)
		{
			if (string.IsNullOrWhiteSpace(privilegeName))
				return false;

			var httpContext = _httpContextAccessor?.HttpContext;
			if (httpContext == null)
				return false;

			if (!(httpContext.User?.Identity?.IsAuthenticated ?? false))
				return false;

			var session = httpContext.Session;
			if (session == null)
				return false;

			// Retrieve privileges from session
			var privilegesStr = session.GetString("Privileges");
			if (string.IsNullOrWhiteSpace(privilegesStr))
				return false;

			// Convert privileges to a string array
			var privileges = privilegesStr
				.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

			// Case-insensitive comparison
			return privileges.Contains(privilegeName, StringComparer.OrdinalIgnoreCase);
		}
	}
}
