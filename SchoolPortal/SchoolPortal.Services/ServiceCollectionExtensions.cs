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

            // Register new services
            services.AddScoped<IAuthorMasterService, AuthorMasterService>();
            services.AddScoped<IBookMasterService, BookMasterService>();
            services.AddScoped<IPublisherMasterService, PublisherMasterService>();
            services.AddScoped<IEmpCategoryMasterService, EmpCategoryMasterService>();
            services.AddScoped<IParentMasterService, ParentMasterService>();
            services.AddScoped<IRegistrationMasterService, RegistrationMasterService>();
            services.AddScoped<IStudentReportCardMasterService, StudentReportCardMasterService>();

            return services;
        }
    }
}
