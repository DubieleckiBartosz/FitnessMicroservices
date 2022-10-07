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

builder.Host.UseSerilog((ctx, lc) => lc.LogConfigurationService()); 

builder.ApiConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
app.UseLoggerHandler();
app.UseCustomExceptionHandler(ErrorMiddleware.GetStatusCode, ErrorMiddleware.GetErrorResponse);

app.UseHttpsRedirection();

app.UseCors(config =>
{
    config
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
