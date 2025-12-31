using Microsoft.Extensions.DependencyInjection;
using SchoolPortal.Services.IServices;
using SchoolPortal.Services.Repositories;
using SchoolPortal.Services.Services;
using System;
using System.Data;
using SchoolPortal.DBAccess;

namespace SchoolPortal.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // Register connection manager
            services.AddSingleton<ConnectionManager>(_ => ConnectionManager.DefaultConnectionManager);

            // Register IDbConnection
            services.AddScoped<IDbConnection>(sp =>
                sp.GetRequiredService<ConnectionManager>().GetConnection());

            // Register repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Register other services as needed
            // Example: services.AddScoped<IMyService, MyService>();

            return services;
        }
    }
}
