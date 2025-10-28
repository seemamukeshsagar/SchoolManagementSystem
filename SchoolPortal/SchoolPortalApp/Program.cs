using SchoolPortal.Services.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Detect environment automatically
var hostName = System.Environment.MachineName;

if (hostName.Equals("SAGAR\\SQl2025", StringComparison.OrdinalIgnoreCase))
{
    builder.Configuration.AddJsonFile("appsettings.Home.json", optional: true);
}
else if (hostName.Equals("DESKTOP-L9I46P8", StringComparison.OrdinalIgnoreCase))
{
    builder.Configuration.AddJsonFile("appsettings.Office.json", optional: true);
}

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

// DI registrations
builder.Services.AddScoped<ILoginService, SchoolPortal.Services.LoginService>();
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
builder.Services.AddScoped<IDeptMasterService, SchoolPortal.Services.DeptMasterService>();
builder.Services.AddScoped<IDesigMasterService, SchoolPortal.Services.DesigMasterService>();
builder.Services.AddScoped<IDeptDesigDetailsService, SchoolPortal.Services.DeptDesigDetailsService>();

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

app.Run();
