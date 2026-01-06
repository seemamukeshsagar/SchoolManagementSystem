using Microsoft.Extensions.Diagnostics.HealthChecks;
using SchoolPortal.DBAccess;

namespace SchoolPortalApp.Services
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly ConnectionManager _connectionManager;

        public DatabaseHealthCheck(ConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                using var connection = _connectionManager.GetConnection();
                await connection.OpenAsync(cancellationToken);
                return HealthCheckResult.Healthy("Database is available");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Database is unavailable", ex);
            }
        }
    }
}
