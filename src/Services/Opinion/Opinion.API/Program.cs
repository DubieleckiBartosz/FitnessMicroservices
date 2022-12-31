using Fitness.Common;
using Fitness.Common.Core;
using Fitness.Common.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Opinion.API;
using Opinion.API.Common;
using Opinion.API.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


var env = builder.Environment;
var commonFolder = Path.Combine(env.ContentRootPath, "..\\..\\..", "Common");

builder.Configuration.AddJsonFile(Path.Combine(commonFolder, "CommonSettings.json"), optional: true)
    .AddJsonFile("CommonSettings.json", optional: true)
    .AddJsonFile("appsettings.json", optional: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true).AddEnvironmentVariables();


// Add services to the container.
builder.EventStoreConfiguration(null, typeof(AssemblyOpinionReference), typeof(AssemblyCommonReference))
    .GetDependencyInjection()
    .GetDatabaseConfiguration();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


var issuer = builder.Configuration["JwtSettings:Issuer"];
var audience = builder.Configuration["JwtSettings:Audience"];
var key = builder.Configuration["JwtSettings:Key"];

builder.Services.RegisterJwtBearer(issuer, audience, key);

builder.Host.UseSerilog((ctx, lc) => lc.LogConfigurationService());


builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Fitness Opinion API",
        Version = "v1",
        Description = "ASP.NET Core 6.0 Web API",
        Contact = new OpenApiContact
        {
            Name = "Github",
            Url = new Uri("https://github.com/DubieleckiBartosz"),
        }
    });
    var securityScheme = new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Description = "Enter JWT Bearer token",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, new string[] { }}
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSubscribeAllEvents(typeof(AssemblyOpinionReference).Assembly);

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
