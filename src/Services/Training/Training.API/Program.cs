using System.Reflection;
using Fitness.Common; 
using Fitness.Common.Logging;
using Fitness.Common.Projection;
using Serilog;
using Training.API.Common;
using Training.API.Configurations;
using Training.API.Database;
using Training.API.Repositories.Interfaces;
using Training.API.Trainings.TrainingProjections;

var builder = WebApplication.CreateBuilder(args);


var env = builder.Environment;
var commonFolder = Path.Combine(env.ContentRootPath, "..\\..\\..", "Common");

builder.Configuration.AddJsonFile(Path.Combine(commonFolder, "CommonSettings.json"), optional: true)
    .AddJsonFile("CommonSettings.json", optional: true)
    .AddJsonFile("appsettings.json", optional: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true).AddEnvironmentVariables();


// Add services to the container.
builder.EventStoreConfiguration(_ => new List<IProjection>
    {
        new TrainingDetailsProjection(_.GetService<IWrapperRepository>())
    }, typeof(Program), typeof(AssemblyCommonReference))
    .RegisterTypes().RegisterBackgroundProcess()
    .GetDatabaseConfiguration()
    .GetAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var issuer = builder.Configuration["JwtSettings:Issuer"];
var audience = builder.Configuration["JwtSettings:Audience"];
var key = builder.Configuration["JwtSettings:Key"];

builder.Services.RegisterJwtBearer(issuer, audience, key);

builder.Host.UseSerilog((ctx, lc) => lc.LogConfigurationService());

builder.GetSwaggerConfiguration();

var app = builder.Build()
    .MigrateDatabase<TrainingContext>((services, ex) =>
{
    var logger = services.GetRequiredService<ILoggerManager<Program>>();
    logger.LogError(ex, "An error occurred while migrating the database.");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler(ErrorMiddleware.GetStatusCode, ErrorMiddleware.GetErrorResponse);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
