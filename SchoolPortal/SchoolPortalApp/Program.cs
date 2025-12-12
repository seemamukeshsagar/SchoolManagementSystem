#nullable enable
using SchoolPortal.Services.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text.RegularExpressions;
using SchoolPortal.Services.Common;
using Microsoft.Extensions.DependencyInjection;
using SchoolPortal.DBAccess;
using SchoolPortal.Services.Services;
using Microsoft.AspNetCore.Authorization;
using SchoolPortal.Services;

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

// Add configuration
var configuration = builder.Configuration;
builder.Services.AddSingleton<IConfiguration>(configuration);

builder.Services.AddMemoryCache();

// Authentication & Authorization
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Index";
        options.LogoutPath = "/Authentication/Logout";
        options.AccessDeniedPath = "/Home/Index";
        options.ReturnUrlParameter = "returnUrl";
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

builder.Services.AddScoped<System.Data.IDbConnection>(sp =>
     sp.GetRequiredService<SchoolPortal.DBAccess.ConnectionManager>().GetConnection());

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
builder.Services.AddScoped<IHolidayTypeMasterService, SchoolPortal.Services.HolidayTypeMasterService>();
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
builder.Services.AddScoped<IBookTypeService, SchoolPortal.Services.BookTypeService>();
builder.Services.AddScoped<IItemTypeService, SchoolPortal.Services.ItemTypeService>();
builder.Services.AddScoped<IItemService, SchoolPortal.Services.ItemService>();
builder.Services.AddScoped<IInventoryService, SchoolPortal.Services.InventoryService>();
builder.Services.AddScoped<IUserDetailsService, SchoolPortal.Services.UserDetailsService>();
builder.Services.AddScoped<IFeesCategoryMasterService, SchoolPortal.Services.FeesCategoryMasterService>();
builder.Services.AddScoped<IAssesmentMasterService, SchoolPortal.Services.AssesmentMasterService>();
builder.Services.AddScoped<IAttendanceReasonMasterService, SchoolPortal.Services.AttendanceReasonMasterService>();
builder.Services.AddScoped<ITimeTablePeriodMasterService, SchoolPortal.Services.TimeTablePeriodMasterService>();
builder.Services.AddScoped<ITimeTableSetupDetailsService, SchoolPortal.Services.TimeTableSetupDetailsService>();
builder.Services.AddScoped<ISessionMasterService, SchoolPortal.Services.SessionMasterService>();
builder.Services.AddScoped<INonTeachingService, NonTeachingService>();
builder.Services.AddScoped<INonTeachingDocumentDetailsService, NonTeachingDocumentDetailsService>();
builder.Services.AddScoped<INonTeachingQualificationDetailsService, NonTeachingQualificationDetailsService>();
builder.Services.AddScoped<ITimeTablePeriodService, SchoolPortal.Services.TimeTablePeriodService>();
builder.Services.AddScoped<IAcademicYearService, SchoolPortal.Services.AcademicYearService>();
builder.Services.AddScoped<IEmpAttendanceService, EmpAttendanceService>(); // You'll need to implement this
builder.Services.AddScoped<IStudentAttendanceService, StudentAttendanceService>();

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

// Configure AuthorizedManager for each request
app.Use(async (context, next) =>
{
    AuthorizedManager.Configure(context.RequestServices.GetRequiredService<IHttpContextAccessor>());
    await next();
});

app.Run();