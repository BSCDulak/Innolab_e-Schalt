using System.Globalization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using eSchalt.Backend;
using eSchalt.Frontend.Classes.Tasks;
using Microsoft.AspNetCore.Identity;

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

app.UseAuthentication();
app.UseAuthorization();

// app.UseSession();

app.MapRazorPages();

// this code doesn�t work anymore if we start via http. It used to work with docker-compose build and docker-compose up.
// Even dotnet ef database update is not working anymore.
// and when I try to conect to the database via demopage it says "Host is unreachable".
// I figured out that the changes in the docker-compose.yml file are the reason for the host being unreachable. I reverted the changes in the docker-compose.yml file.
// This means that the sasswatcher and the upload cleaning task will not work at this moment.
// Since I have no good ideas why this problem exists I will focus on other things for now.


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
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while applying database migrations.");
        // Exit app on error, alternatively you can log the error and continue if you don't care about the migration not being applied.
        // Currently the migrations work only on docker-compose build/up but will break on running http (which I am not doing at the moment since I am using docker-compos up for all).
        //Environment.Exit(1);
    }
}

app.Run();