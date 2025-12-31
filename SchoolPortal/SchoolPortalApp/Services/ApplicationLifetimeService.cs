using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolPortalApp.Services
{
    public class ApplicationLifetimeService : IHostedService, IDisposable
    {
        private readonly ILogger<ApplicationLifetimeService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private bool _disposed = false;

        public ApplicationLifetimeService(
            ILogger<ApplicationLifetimeService> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ApplicationLifetimeService started");
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ApplicationLifetimeService stopping...");
            OnStopping();
            await OnStoppedAsync();
        }

        public void OnStopping()
        {
            try
            {
                _logger.LogInformation("Application is stopping. Cleaning up resources...");
                
                // Close any open database connections
                var connectionManager = _serviceProvider.GetService<SchoolPortal.DBAccess.ConnectionManager>();
                if (connectionManager != null)
                {
                    _logger.LogInformation("Closing database connections...");
                    connectionManager.Dispose();
                }

                // Add any other cleanup tasks here
                // For example, close any open files, network connections, etc.

                _logger.LogInformation("Cleanup completed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during application stopping");
            }
        }

        public async Task OnStoppedAsync()
        {
            try
            {
                _logger.LogInformation("Application has stopped. Final cleanup...");
                
                // Force garbage collection to clean up any remaining resources
                GC.Collect();
                GC.WaitForPendingFinalizers();
                
                // Add a small delay to ensure all resources are released
                await Task.Delay(500);
                
                _logger.LogInformation("Application shutdown complete.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during application stopped");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                }
                _disposed = true;
            }
        }

        ~ApplicationLifetimeService()
        {
            Dispose(false);
        }
    }
}
