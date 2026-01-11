#nullable enable
using ClosedXML.Excel;
using ClosedXML.Excel.Drawings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;  // For DependencyInjection
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using SchoolPortal.DBAccess;
using SchoolPortal.Services;
using SchoolPortal.Services.Common;
using SchoolPortal.Services.IServices;
using SchoolPortal.Services.Services;
using SchoolPortalApp.Helpers;
using SchoolPortalApp.Services;
using SchoolPortalApp.Utilities;  // For ConnectionStringHelper
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolPortal.Entities.Models;

var builder = WebApplication.CreateBuilder(args);

var serverNameRaw = Environment.MachineName;
var serverName = Regex.Replace(serverNameRaw, "[^A-Za-z0-9_.-]", "_");

if (serverName == "DESKTOP-L9I46P8")
{
	serverName = "Office";
}
else
{
	serverName = "Home";
}

builder.Configuration
		.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
		.AddJsonFile($"appsettings.{serverName}.json", optional: true, reloadOnChange: true)
		.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.PropertyNamingPolicy = null; // Preserve property casing
	});

// Add configuration
var configuration = builder.Configuration;
builder.Services.AddSingleton<IConfiguration>(configuration);

// Initialize ConnectionStringHelper
ConnectionStringHelper.Initialize(configuration);

// Get connection string using the helper
string connectionString;
try
{
	connectionString = ConnectionStringHelper.GetConnectionString("DefaultConnectionString");
}
catch
{
	// Fallback: try DefaultConnection key
	try
	{
		connectionString = ConnectionStringHelper.GetConnectionString("DefaultConnection");
	}
	catch
	{
		// Last resort: use ConnectionManager's FindWorkingConnectionString
		connectionString = SchoolPortal.DBAccess.ConnectionManager.FindWorkingConnectionString()
			?? throw new InvalidOperationException("Could not determine a valid connection string. Please check your appsettings.json and ensure SQL Server is running.");
	}
}

// Test the connection string and try alternatives if it fails
try
{
	using (var testConnection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
	{
		testConnection.Open();
	}
}
catch
{
	// Try to find a working connection string
	var workingConnectionString = SchoolPortal.DBAccess.ConnectionManager.FindWorkingConnectionString();
	if (workingConnectionString != null)
	{
		connectionString = workingConnectionString;
	}
	else
	{
		// Log warning but continue - connection will be tested again when actually used
		using var loggerFactory = LoggerFactory.Create(loggingBuilder =>
		{
			loggingBuilder.AddConsole();
			loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
		});
		var logger = loggerFactory.CreateLogger<Program>();
		logger.LogWarning("Initial connection test failed. Will retry when connection is actually needed.");
	}
}

// Set the connection string in configuration
builder.Configuration["ConnectionStrings:DefaultConnectionString"] = connectionString;

// Update ConnectionManager with the connection string from appsettings
// This ensures ConnectionManager uses the configured connection string
try
{
	var connectionManager = SchoolPortal.DBAccess.ConnectionManager.DefaultConnectionManager;
	if (!string.IsNullOrWhiteSpace(connectionString) &&
		!connectionString.Contains("{SQL_SERVER}") &&
		!connectionString.Contains("{DATABASE_NAME}"))
	{
		connectionManager.SetConnectionString(connectionString);
	}
}
catch (Exception ex)
{
	// Create a logger factory manually to avoid BuildServiceProvider anti-pattern
	using var loggerFactory = LoggerFactory.Create(loggingBuilder =>
	{
		loggingBuilder.AddConsole();
		loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
	});
	var logger = loggerFactory.CreateLogger<Program>();
	logger.LogWarning(ex, "Failed to update ConnectionManager with appsettings connection string. Using default.");
}

// Add data services and configure dependency injection
builder.Services.AddDataServices();

// Register AppDbContext with Entity Framework
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(connectionString));

// Add memory cache
builder.Services.AddMemoryCache();

// Authentication & Authorization
builder.Services
	.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Authentication/Login";
		options.LogoutPath = "/Authentication/Logout";
		options.AccessDeniedPath = "/Authentication/AccessDenied";
		options.ReturnUrlParameter = "returnUrl";
		options.ExpireTimeSpan = TimeSpan.FromHours(2);
		options.SlidingExpiration = true;
		options.Events = new CookieAuthenticationEvents
		{
			OnRedirectToLogin = context =>
			{
				// Don't redirect to login page for unauthorized requests
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				return Task.CompletedTask;
			}
		};
	});

// Configure default authorization policy
builder.Services.AddAuthorization(options =>
{
	options.DefaultPolicy = new AuthorizationPolicyBuilder()
		.RequireAuthenticatedUser()
		.Build();

	// Add a fallback policy that allows anonymous access
	options.FallbackPolicy = new AuthorizationPolicyBuilder()
		.RequireAssertion(_ => true) // This allows all requests by default
		.Build();
});

// Add health checks
builder.Services.AddHealthChecks()
	.AddCheck<DatabaseHealthCheck>("Database");

// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddAntiforgery(o =>
{
	o.Cookie.SecurePolicy = builder.Environment.IsDevelopment()
		? Microsoft.AspNetCore.Http.CookieSecurePolicy.None
		: Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
	o.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
	o.Cookie.HttpOnly = true;
	o.HeaderName = "X-CSRF-TOKEN";
	o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

// Add CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll",
		builder => builder
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader());
});

builder.Services.AddRateLimiter(options =>
{
	options.AddFixedWindowLimiter("fixed", opt =>
	{
		opt.AutoReplenishment = true;
		opt.PermitLimit = 100;
		opt.Window = TimeSpan.FromMinutes(1);
		opt.QueueLimit = 0;
	});
});

// Register IHttpContextAccessor (singleton)
builder.Services.AddHttpContextAccessor();

// Register logging
builder.Services.AddLogging(loggingBuilder =>
{
	loggingBuilder.AddConsole();
	loggingBuilder.AddDebug();
});

// Register connection manager and database connection
builder.Services.AddSingleton<SchoolPortal.DBAccess.ConnectionManager>(_ =>
	SchoolPortal.DBAccess.ConnectionManager.DefaultConnectionManager);

builder.Services.AddScoped<System.Data.IDbConnection>(sp =>
	 sp.GetRequiredService<SchoolPortal.DBAccess.ConnectionManager>().GetConnection());

// Core services
builder.Services.AddScoped<ILoginService, SchoolPortal.Services.LoginService>();
builder.Services.AddScoped<ILookupService, SchoolPortal.Services.LookupService>();
builder.Services.AddScoped<ISystemParametersService, SchoolPortal.Services.SystemParametersService>();

// School management services
builder.Services.AddScoped<ICompanyService, SchoolPortal.Services.CompanyService>();
builder.Services.AddScoped<ISchoolService, SchoolPortal.Services.SchoolService>();
builder.Services.AddScoped<ISchoolContactService, SchoolPortal.Services.SchoolContactService>();

// Academic services
builder.Services.AddScoped<IClassService, SchoolPortal.Services.ClassService>();
builder.Services.AddScoped<ISectionService, SchoolPortal.Services.SectionService>();
builder.Services.AddScoped<ISubjectService, SchoolPortal.Services.SubjectService>();
builder.Services.AddScoped<ISubjectCategoryService, SchoolPortal.Services.SubjectCategoryService>();
builder.Services.AddScoped<IClassRoomService, SchoolPortal.Services.ClassRoomService>();
builder.Services.AddScoped<IClassSubjectService, SchoolPortal.Services.ClassSubjectService>();
builder.Services.AddScoped<IClassSectionDetailService, SchoolPortal.Services.ClassSectionDetailService>();

// Student services
builder.Services.AddScoped<IStudentService, SchoolPortal.Services.StudentService>();
builder.Services.AddScoped<IStudentAttendanceService, StudentAttendanceService>();

// Teacher services
builder.Services.AddScoped<ITeacherService, SchoolPortal.Services.TeacherService>();
builder.Services.AddScoped<ITeacherClassDetailsService, SchoolPortal.Services.TeacherClassDetailsService>();
builder.Services.AddScoped<ITeacherSubjectDetailsService, SchoolPortal.Services.TeacherSubjectDetailsService>();
builder.Services.AddScoped<ITeacherSectionDetailsService, SchoolPortal.Services.TeacherSectionDetailsService>();
builder.Services.AddScoped<ITeacherDocumentDetailsService, SchoolPortal.Services.TeacherDocumentDetailsService>();
builder.Services.AddScoped<ITeacherQualificationDetailsService, SchoolPortal.Services.TeacherQualificationDetailsService>();

// Employee services
builder.Services.AddScoped<IEmpService, SchoolPortal.Services.EmpService>();
builder.Services.AddScoped<IEmpTypeService, SchoolPortal.Services.EmpTypeService>();
builder.Services.AddScoped<IEmpAttendanceService, EmpAttendanceService>();

// Parent services
builder.Services.AddScoped<IParentService, SchoolPortal.Services.ParentService>();

// Role and security services
builder.Services.AddScoped<IRoleMasterService, SchoolPortal.Services.RoleMasterService>();
builder.Services.AddScoped<IPrivilegeService, SchoolPortal.Services.PrivilegeService>();
builder.Services.AddScoped<IRolePrivilegeService, SchoolPortal.Services.RolePrivilegeService>();

// Lookup and master data services
builder.Services.AddScoped<ICategoryMasterService, SchoolPortal.Services.CategoryMasterService>();
builder.Services.AddScoped<IDesigMasterService, SchoolPortal.Services.DesigMasterService>();
builder.Services.AddScoped<IProfessionMasterService, SchoolPortal.Services.ProfessionMasterService>();
builder.Services.AddScoped<IQualificationMasterService, SchoolPortal.Services.QualificationMasterService>();
builder.Services.AddScoped<IDeptMasterService, SchoolPortal.Services.DeptMasterService>();
builder.Services.AddScoped<IDeptDesigDetailsService, SchoolPortal.Services.DeptDesigDetailsService>();

// Time and attendance services
builder.Services.AddScoped<IHolidayMasterService, SchoolPortal.Services.HolidayMasterService>();
builder.Services.AddScoped<IHolidayTypeMasterService, SchoolPortal.Services.HolidayTypeMasterService>();
builder.Services.AddScoped<IAttendanceReasonMasterService, SchoolPortal.Services.AttendanceReasonMasterService>();

// Visitor and facility services
builder.Services.AddScoped<IVisitorService, SchoolPortal.Services.VisitorService>();

// Staff services
builder.Services.AddScoped<ICleanerMasterService, SchoolPortal.Services.CleanerMasterService>();
builder.Services.AddScoped<ICleanerDocumentDetailsService, SchoolPortal.Services.CleanerDocumentDetailsService>();
builder.Services.AddScoped<ICleanerQualificationDetailsService, SchoolPortal.Services.CleanerQualificationDetailsService>();
builder.Services.AddScoped<IDriverMasterService, SchoolPortal.Services.DriverMasterService>();
builder.Services.AddScoped<IDriverDocumentDetailsService, SchoolPortal.Services.DriverDocumentDetailsService>();
builder.Services.AddScoped<IDriverQualificationDetailsService, SchoolPortal.Services.DriverQualificationDetailsService>();

// Inventory and resource services
builder.Services.AddScoped<ISupplierService, SchoolPortal.Services.SupplierService>();
builder.Services.AddScoped<IVendorService, SchoolPortal.Services.VendorService>();
builder.Services.AddScoped<IVehicleMasterService, SchoolPortal.Services.VehicleMasterService>();
builder.Services.AddScoped<IVehicleTypeMasterService, SchoolPortal.Services.VehicleTypeMasterService>();
builder.Services.AddScoped<IVehicleExpenseDetailsService, SchoolPortal.Services.VehicleExpenseDetailsService>();
builder.Services.AddScoped<IBookCategoryService, SchoolPortal.Services.BookCategoryService>();
builder.Services.AddScoped<IBookTypeService, SchoolPortal.Services.BookTypeService>();
builder.Services.AddScoped<IItemTypeService, SchoolPortal.Services.ItemTypeService>();
builder.Services.AddScoped<IItemService, SchoolPortal.Services.ItemService>();
builder.Services.AddScoped<IInventoryService, SchoolPortal.Services.InventoryService>();

// User and account services
builder.Services.AddScoped<IUserDetailsService, SchoolPortal.Services.UserDetailsService>();

// Backup service
builder.Services.AddScoped<IBackupService, SchoolPortal.Services.BackupService>();

// Cache service
builder.Services.AddScoped<ICacheService, SchoolPortal.Services.CacheService>();

// Maintenance service
builder.Services.AddScoped<IMaintenanceService, SchoolPortal.Services.MaintenanceService>();

// Security service
builder.Services.AddScoped<ISecurityService, SchoolPortal.Services.SecurityService>();

// Academic services
builder.Services.AddScoped<IFeesCategoryMasterService, SchoolPortal.Services.FeesCategoryMasterService>();
builder.Services.AddScoped<IAssesmentMasterService, SchoolPortal.Services.AssesmentMasterService>();
builder.Services.AddScoped<ITimeTablePeriodMasterService, SchoolPortal.Services.TimeTablePeriodMasterService>();
builder.Services.AddScoped<ITimeTableSetupDetailsService, SchoolPortal.Services.TimeTableSetupDetailsService>();
builder.Services.AddScoped<ISessionMasterService, SchoolPortal.Services.SessionMasterService>();
builder.Services.AddScoped<ITimeTablePeriodService, SchoolPortal.Services.TimeTablePeriodService>();
builder.Services.AddScoped<IAcademicYearService, SchoolPortal.Services.AcademicYearService>();

// Audit logging
builder.Services.AddScoped<IAuditLogger, AuditLogger>();

// Non-teaching staff services
builder.Services.AddScoped<INonTeachingService, NonTeachingService>();
builder.Services.AddScoped<INonTeachingDocumentDetailsService, NonTeachingDocumentDetailsService>();
builder.Services.AddScoped<INonTeachingQualificationDetailsService, NonTeachingQualificationDetailsService>();

// Add services to the container
builder.Services.AddScoped<IReportService, ReportService>();

// Add application lifetime and logging for shutdown handling
builder.Services.AddSingleton<ApplicationLifetimeService>();
builder.Services.AddSingleton<IHostedService>(sp => sp.GetRequiredService<ApplicationLifetimeService>());

// Build the application
var app = builder.Build();

// Get the application lifetime service
var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
var lifetimeService = app.Services.GetRequiredService<ApplicationLifetimeService>();

// Register application stopping handler
lifetime.ApplicationStopping.Register(() =>
{
	try
	{
		lifetimeService.OnStopping();
	}
	catch (Exception ex)
	{
		var logger = app.Services.GetRequiredService<ILogger<Program>>();
		logger.LogError(ex, "An error occurred during application stopping");
	}
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add rate limiting middleware
app.UseRateLimiter();

// Add request/response logging middleware
app.UseMiddleware<SchoolPortalApp.Middleware.RequestResponseLoggingMiddleware>();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();

// Health check endpoint
app.MapHealthChecks("/health");

// Map specific controller routes (if needed)
app.MapControllerRoute(
	name: "account",
	pattern: "Authentication/{action=Login}",
	defaults: new { controller = "Authentication" });

// The default route handles all controller/action patterns
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

// Map Razor Pages (must be after controller routes to avoid conflicts)
app.MapRazorPages()
   .WithStaticAssets();



// Register application stopped handler
lifetime.ApplicationStopped.Register(async () =>
{
	try
	{
		// The StopAsync method will be called automatically by the host
		// But we can also call OnStoppedAsync directly if needed
		await lifetimeService.StopAsync(CancellationToken.None);
	}
	catch (Exception ex)
	{
		var logger = app.Services.GetRequiredService<ILogger<Program>>();
		logger.LogError(ex, "An error occurred during application shutdown");
	}
});

app.Run();