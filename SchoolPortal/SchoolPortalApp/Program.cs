#nullable enable
using SchoolPortal.Services.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text.RegularExpressions;
using SchoolPortal.Services.Common;
using Microsoft.Extensions.DependencyInjection;
using SchoolPortal.DBAccess;

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
builder.Services.AddControllersWithViews();

// Authentication & Authorization
builder.Services
	.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Account/Login";
		options.LogoutPath = "/Account/Logout";
		options.AccessDeniedPath = "/Account/Login";
		options.SlidingExpiration = true;
	});
builder.Services.AddAuthorization();

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
});

// Register IHttpContextAccessor (singleton)
builder.Services.AddHttpContextAccessor();

// Register logging
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

// DI registrations
builder.Services.AddSingleton<SchoolPortal.DBAccess.ConnectionManager>(_ => 
	SchoolPortal.DBAccess.ConnectionManager.DefaultConnectionManager);
builder.Services.AddScoped<ILoginService, SchoolPortal.Services.LoginService>();
builder.Services.AddScoped<ICategoryMasterService, SchoolPortal.Services.CategoryMasterService>();
builder.Services.AddScoped<ICompanyService, SchoolPortal.Services.CompanyService>();
builder.Services.AddScoped<ILookupService, SchoolPortal.Services.LookupService>();
builder.Services.AddScoped<ISchoolService, SchoolPortal.Services.SchoolService>();
builder.Services.AddScoped<ISchoolContactService, SchoolPortal.Services.SchoolContactService>();
builder.Services.AddScoped<IClassService, SchoolPortal.Services.ClassService>();
builder.Services.AddScoped<ISectionService, SchoolPortal.Services.SectionService>();
builder.Services.AddScoped<ISubjectService, SchoolPortal.Services.SubjectService>();
builder.Services.AddScoped<ISubjectCategoryService, SchoolPortal.Services.SubjectCategoryService>();
builder.Services.AddScoped<IClassRoomService, SchoolPortal.Services.ClassRoomService>();
builder.Services.AddScoped<ITeacherService, SchoolPortal.Services.TeacherService>();
builder.Services.AddScoped<IStudentService, SchoolPortal.Services.StudentService>();
builder.Services.AddScoped<ITeacherClassDetailsService, SchoolPortal.Services.TeacherClassDetailsService>();
builder.Services.AddScoped<ITeacherSubjectDetailsService, SchoolPortal.Services.TeacherSubjectDetailsService>();
builder.Services.AddScoped<ITeacherSectionDetailsService, SchoolPortal.Services.TeacherSectionDetailsService>();
builder.Services.AddScoped<ITeacherDocumentDetailsService, SchoolPortal.Services.TeacherDocumentDetailsService>();
builder.Services.AddScoped<ITeacherQualificationDetailsService, SchoolPortal.Services.TeacherQualificationDetailsService>();
builder.Services.AddScoped<ISystemParametersService, SchoolPortal.Services.SystemParametersService>();
builder.Services.AddScoped<IDesigMasterService, SchoolPortal.Services.DesigMasterService>();
builder.Services.AddScoped<IProfessionMasterService, SchoolPortal.Services.ProfessionMasterService>();
builder.Services.AddScoped<IQualificationMasterService, SchoolPortal.Services.QualificationMasterService>();
builder.Services.AddScoped<IDeptMasterService, SchoolPortal.Services.DeptMasterService>();
builder.Services.AddScoped<IDeptDesigDetailsService, SchoolPortal.Services.DeptDesigDetailsService>();
builder.Services.AddScoped<IClassSubjectService, SchoolPortal.Services.ClassSubjectService>();
builder.Services.AddScoped<IClassSectionDetailService, SchoolPortal.Services.ClassSectionDetailService>();
builder.Services.AddScoped<IEmpService, SchoolPortal.Services.EmpService>();
builder.Services.AddScoped<IParentService, SchoolPortal.Services.ParentService>();
builder.Services.AddScoped<IRoleMasterService, SchoolPortal.Services.RoleMasterService>();
builder.Services.AddScoped<IPrivilegeService, SchoolPortal.Services.PrivilegeService>();
builder.Services.AddScoped<IRolePrivilegeService, SchoolPortal.Services.RolePrivilegeService>();
builder.Services.AddScoped<IHolidayMasterService, SchoolPortal.Services.HolidayMasterService>();
builder.Services.AddScoped<IEmpTypeService, SchoolPortal.Services.EmpTypeService>();
builder.Services.AddScoped<IVisitorService, SchoolPortal.Services.VisitorService>();
builder.Services.AddScoped<ICleanerMasterService, SchoolPortal.Services.CleanerMasterService>();
builder.Services.AddScoped<ICleanerDocumentDetailsService, SchoolPortal.Services.CleanerDocumentDetailsService>();
builder.Services.AddScoped<ICleanerQualificationDetailsService, SchoolPortal.Services.CleanerQualificationDetailsService>();
builder.Services.AddScoped<IDriverMasterService, SchoolPortal.Services.DriverMasterService>();
builder.Services.AddScoped<IDriverDocumentDetailsService, SchoolPortal.Services.DriverDocumentDetailsService>();
builder.Services.AddScoped<IDriverQualificationDetailsService, SchoolPortal.Services.DriverQualificationDetailsService>();
builder.Services.AddScoped<ISupplierService, SchoolPortal.Services.SupplierService>();
builder.Services.AddScoped<IVendorService, SchoolPortal.Services.VendorService>();
builder.Services.AddScoped<IVehicleMasterService, SchoolPortal.Services.VehicleMasterService>();
builder.Services.AddScoped<IVehicleTypeMasterService, SchoolPortal.Services.VehicleTypeMasterService>();
builder.Services.AddScoped<IVehicleExpenseDetailsService, SchoolPortal.Services.VehicleExpenseDetailsService>();
builder.Services.AddScoped<IBookCategoryService, SchoolPortal.Services.BookCategoryService>();
builder.Services.AddScoped<IItemTypeService, SchoolPortal.Services.ItemTypeService>();
builder.Services.AddScoped<IItemService, SchoolPortal.Services.ItemService>();
builder.Services.AddScoped<IItemService, SchoolPortal.Services.ItemService>();
builder.Services.AddScoped<IInventoryService, SchoolPortal.Services.InventoryService>();
builder.Services.AddScoped<IUserDetailsService, SchoolPortal.Services.UserDetailsService>();
builder.Services.AddScoped<IFeesCategoryMasterService, SchoolPortal.Services.FeesCategoryMasterService>();
builder.Services.AddScoped<IAssesmentMasterService, SchoolPortal.Services.AssesmentMasterService>();
builder.Services.AddScoped<ITimeTablePeriodMasterService, SchoolPortal.Services.TimeTablePeriodMasterService>();
builder.Services.AddScoped<ITimeTableSetupDetailsService, SchoolPortal.Services.TimeTableSetupDetailsService>();
builder.Services.AddScoped<ISessionMasterService, SchoolPortal.Services.SessionMasterService>();

var app = builder.Build();

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

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

// Map MVC controller routes
app.MapControllerRoute(
	name: "account",
	pattern: "Account/{action=Login}/{id?}",
	defaults: new { controller = "Account" });

app.MapControllerRoute(
	name: "home",
	pattern: "Home/{action=Index}/{id?}",
	defaults: new { controller = "Home" });

// Default MVC route
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Account}/{action=Login}/{id?}");

// Map Razor Pages (must be after controller routes to avoid conflicts)
app.MapRazorPages()
   .WithStaticAssets();

// Add this before app.Run()
app.Use(async (context, next) =>
{
	// This ensures the AuthorizedManager is configured per-request
	AuthorizedManager.Configure(context.RequestServices.GetRequiredService<IHttpContextAccessor>());
	await next();
});

app.Run();