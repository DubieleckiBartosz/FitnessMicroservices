using Microsoft.EntityFrameworkCore;
using Training.API.Database;

namespace Training.API.Configurations;

public static class DatabaseConfiguration
{
    public static WebApplicationBuilder GetDatabaseConfiguration(this WebApplicationBuilder webApplicationBuilder)
    {
        var connectionString = webApplicationBuilder.Configuration["TrainingPostgresConnection:ConnectionString"];

        webApplicationBuilder.Services.AddDbContext<TrainingContext>(options =>
        {
            options.UseNpgsql(connectionString,
                sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(30), null);
                });
        });

        return webApplicationBuilder;
    }
}