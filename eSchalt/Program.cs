using System.Globalization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using eSchalt.Backend;
using eSchalt.Frontend.Classes.Tasks;
using Microsoft.AspNetCore.Identity;
using eSchalt.Backend.HelperClasses;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#if DEBUG
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
#else
builder.Services.AddRazorPages();
#endif

// Set culture
var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Register DbContext with PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")) 
);

// Register Ai Client with http 

builder.Services.AddHttpClient<AiClient>(client =>
{
    // because of the docker settings --> might be changed!
    client.BaseAddress = new Uri("http://localhost:8000");
    client.Timeout = TimeSpan.FromSeconds(120);

});


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>();

// Redis cache
// builder.Services.AddStackExchangeRedisCache(options =>
// {
//     options.Configuration = builder.Configuration.GetConnectionString("Redis");
// });
// builder.Services.AddSession();

// Tasks
builder.Services.AddHostedService<UploadCleanUpTask>();

var app = builder.Build();

var defaultUrl = app.Environment.IsDevelopment() ? "http://*:5000" : "https://*:5001";
builder.WebHost.UseUrls(Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? defaultUrl);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseForwardedHeaders(
    new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto }
);
app.UseStaticFiles();

app.UseRouting();

// for Identity
app.UseAuthentication();
app.UseAuthorization();

// app.UseSession();

app.MapRazorPages();

// run the database migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        logger.LogInformation("Applying database migrations...");
        dbContext.Database.Migrate();
        logger.LogInformation("Database migrations applied successfully.");
        DynamicDataSeeder.EnsureSeedData(dbContext);
        logger.LogInformation("Database seeding completed successfully.");

    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while applying database migrations. Most likely docker desktop is not started and docker-compose up command was not written");
        Environment.Exit(1);
    }
}

app.Run();