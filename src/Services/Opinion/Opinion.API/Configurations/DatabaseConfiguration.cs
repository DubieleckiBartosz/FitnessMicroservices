using Microsoft.EntityFrameworkCore;
using Opinion.API.Infrastructure.Database;

namespace Opinion.API.Configurations;

public static class DatabaseConfiguration
{
    public static WebApplicationBuilder GetDatabaseConfiguration(this WebApplicationBuilder webApplicationBuilder)
    {
        var connectionString = webApplicationBuilder.Configuration["OpinionPostgresConnection:ConnectionString"];

        webApplicationBuilder.Services.AddDbContext<OpinionContext>(options =>
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