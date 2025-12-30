using Microsoft.Extensions.Configuration;
using System;

namespace SchoolPortalApp.Utilities
{
    public static class ConnectionStringHelper
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public static string GetConnectionString(string name = "DefaultConnection")
        {
            if (_configuration == null)
            {
                throw new InvalidOperationException("ConnectionStringHelper has not been initialized. Call Initialize() first.");
            }

            return _configuration.GetConnectionString(name) ?? 
                   throw new InvalidOperationException($"Connection string '{name}' not found in configuration.");
        }
    }
}
