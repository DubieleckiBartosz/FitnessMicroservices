using Enrollment.API.Configurations;
using Enrollment.Application;
using Enrollment.Application.Enrollments.ProjectionSection;
using Enrollment.Application.Interfaces;
using Enrollment.Infrastructure.Configurations;
using Fitness.Common;
using Fitness.Common.Projection;
using Serilog;
using System.Reflection;
using Fitness.Common.Logging;

var builder = WebApplication.CreateBuilder(args);


var env = builder.Environment;
var commonFolder = Path.Combine(env.ContentRootPath, "..\\..\\..", "Common");

builder.Configuration.AddJsonFile(Path.Combine(commonFolder, "CommonSettings.json"), optional: true)
    .AddJsonFile("CommonSettings.json", optional: true)
    .AddJsonFile("appsettings.json", optional: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true).AddEnvironmentVariables();

// Add services to the container. 

builder.GetDatabaseConfiguration();

builder
    .EventStoreConfiguration(_ => new List<IProjection>
    {
        new EnrollmentProjections(_.GetService<IEnrollmentRepository>()!)
    }, typeof(Program), typeof(AssemblyCommonReference), typeof(AssemblyEnrollmentApplicationReference))
    .GetDependencyInjectionInfrastructureLayer()
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

var app = builder.Build();

app.SubscribeEvents();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); 

app.Run();


