var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

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
builder.Services.AddScoped<SchoolPortal.Services.ILoginService, SchoolPortal.Services.LoginService>();
builder.Services.AddScoped<SchoolPortal.Services.ICompanyService, SchoolPortal.Services.CompanyService>();
builder.Services.AddScoped<SchoolPortal.Services.ILookupService, SchoolPortal.Services.LookupService>();

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
