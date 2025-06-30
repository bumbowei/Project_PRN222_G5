using Project_PRN222_G5.Web;
using Project_PRN222_G5.Web.Middleware;
using Project_PRN222_G5.Web.Utilities;
using System.Threading.RateLimiting;

const string unknown = "Unknown";

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

#region Razor Pages & MVC

services.AddRazorPages();
services.AddControllersWithViews();
services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? unknown,
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 60,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst
            }));

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

#endregion Razor Pages & MVC

#region App Services

services
    .AddBusinessLogicServices(configuration)
    .AddDataAccessServices(configuration)
    .AddCookieAuthentication(configuration)
    .AddCustomLogging();

#endregion App Services

var app = builder.Build();

#region Exception Handle

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
    app.UseExceptionHandler(PageRoutes.Public.Error);
    app.UseRequestTimeoutMiddleware(TimeSpan.FromSeconds(15));
}

#endregion Exception Handle

app.UseRateLimiter();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthenticatedUserMiddleware();
app.UseAuthorizationMiddleware();
app.UseLoggerMiddleware();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pages}/{action=Home}/{id?}");
app.MapRazorPages();

try
{
    var logger = app.Services.CreateScope().ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Running application...");
    await app.RunAsync();
}
catch (Exception ex)
{
    var logger = app.Services.CreateScope().ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Application failed to start.");
    throw;
}