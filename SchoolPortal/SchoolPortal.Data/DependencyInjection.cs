using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SchoolPortal.Data.Repositories;
using SchoolPortal.Entities.Models;
using System;

namespace SchoolPortal.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // Register generic repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            
            // Register specific repositories
            services.AddScoped<IRepository<StudentMaster>, StudentRepository>();
            
            // Register EmpAttendanceDetails repository
            services.AddScoped<IRepository<EmpAttendanceDetails>>(provider => 
                new Repository<EmpAttendanceDetails>(
                    provider.GetRequiredService<ILogger<Repository<EmpAttendanceDetails>>>(),
                    "EmpAttendanceDetails"
                )
            );
            
            // Register other repositories as needed
            // services.AddScoped<IRepository<ClassMaster>, ClassRepository>();
            // services.AddScoped<IRepository<RoleMaster>, RoleRepository>();
            
            return services;
        }
    }
}
