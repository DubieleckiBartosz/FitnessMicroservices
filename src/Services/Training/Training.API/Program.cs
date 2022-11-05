using Fitness.Common;
using Fitness.Common.EventStore;
using Fitness.Common.Projection;
using Pipelines.Sockets.Unofficial.Arenas;
using Training.API.Configurations; 
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
    .RegisterTypes()
    .GetDatabaseConfiguration();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var issuer = builder.Configuration["JwtSettings:Issuer"];
var audience = builder.Configuration["JwtSettings:Audience"];
var key = builder.Configuration["JwtSettings:Key"];

builder.Services.RegisterJwtBearer(issuer, audience, key);

builder.GetSwaggerConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider; 
//    var result = app.RegisterProjections(new List<IProjection>
//    {
//        new TrainingDetailsProjection(services.GetService<IWrapperRepository>())
//    });

//    var resultService =builder?.Services?.FirstOrDefault(_ => _.ServiceType == typeof(IEventStore));
//    if (resultService == null)
//    {
//        var descriptorToAdd = new ServiceDescriptor(typeof(IEventStore), _ => resultService, ServiceLifetime.Scoped);

//        builder?.Services?.Add(descriptorToAdd);
//    }
//}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
