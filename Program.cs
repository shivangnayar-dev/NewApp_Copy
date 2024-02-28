using Microsoft.EntityFrameworkCore;
using NewApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
var configuration = builder.Configuration;
builder.Services.AddDbContext<CandidateDbContext>(options =>
{
    var connectionString = configuration.GetConnectionString("DefaultConnection");
   var serverVersion = ServerVersion.AutoDetect(connectionString);

    options.UseMySql(connectionString, serverVersion);
});

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy => policy.WithOrigins("http://example.com", "http://www.contoso.com")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseForwardedHeaders(); // Place it here
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Login",
        pattern: "Log-In",
        defaults: new { controller = "Home", action = "Login" });

    // Your existing default route
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Test}/{id?}");
});

app.Run();
