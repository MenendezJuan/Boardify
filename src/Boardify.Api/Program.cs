using Boardify.Api;
using Boardify.Application;
using Boardify.Application.Exceptions;
using Boardify.Common;
using Boardify.External;
using Boardify.Persistence;
using Boardify.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

ConfigurationManager Configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .Build();

builder.Services.AddHttpContextAccessor();

var _policyName = "configuracionCors";

builder.Services.AddCors(x =>
{
    x.AddPolicy(_policyName, builder =>
    {
        var urlCors = Configuration["UrlCors"];
        if (!string.IsNullOrEmpty(urlCors))
        {
            builder.WithOrigins(urlCors)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .SetPreflightMaxAge(TimeSpan.FromDays(365));
        }
    });
});

builder.Services
    .AddWebApi()
    .AddCommon()
    .AddApplication()
    .AddExternal(Configuration)
    .AddPersistence(Configuration);

builder.Services.AddControllers();

var app = builder.Build();


void ApplyMigrations(IApplicationBuilder app, IWebHostEnvironment environment)
{
    using var scope = app.ApplicationServices.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<DatabaseService>();
    if (db.Database.GetPendingMigrations().Any())
    {
        db.Database.Migrate();
    }
}
ApplyMigrations(app, environment);
app.UseMiddleware<GlobalExceptionHandler>();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v2");
    options.RoutePrefix = string.Empty;
});
app.UseCors(_policyName);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();