using Exercise.API.Configurations;
using Fitness.Common;
using System.Reflection;
using Exercise.Infrastructure.Configurations; 
using Exercise.Application;
using Exercise.Application.Configurations;
using Fitness.Common.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


var env = builder.Environment;
var commonFolder = Path.Combine(env.ContentRootPath, "..\\..\\..", "Common");

builder.Configuration.AddJsonFile(Path.Combine(commonFolder, "CommonSettings.json"), optional: true)
    .AddJsonFile("CommonSettings.json", optional: true)
    .AddJsonFile("appsettings.json", optional: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true).AddEnvironmentVariables();

// Add services to the container. 

builder
    .EventStoreConfiguration(null, typeof(Program), typeof(AssemblyCommonReference),
        typeof(AssemblyExerciseApplicationReference))
    .GetDependencyInjectionInfrastructureLayer().GetOptions()
    .GetAutoMapper(Assembly.GetExecutingAssembly());

// Add services to the container.

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
