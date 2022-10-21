using Microsoft.EntityFrameworkCore;
using Npgsql;
using Training.API.Database;

namespace Training.API.Configurations;

public static class DatabaseConfiguration
{
    public static WebApplicationBuilder GetDatabaseConfiguration(this WebApplicationBuilder webApplicationBuilder)
    {
        var connectionString = webApplicationBuilder.Configuration["TrainingPostgresConnection:ConnectionString"];
        var password = webApplicationBuilder.Configuration["TrainingPostgresConnection:PasswordDatabase"];

        var builder = new NpgsqlConnectionStringBuilder(connectionString)
        {
            Password = password
        };

        webApplicationBuilder.Services.AddDbContext<TrainingContext>(options =>
            options.UseNpgsql(builder.ConnectionString));

        return webApplicationBuilder;
    }
}