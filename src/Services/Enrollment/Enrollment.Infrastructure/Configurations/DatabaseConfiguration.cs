using Enrollment.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Enrollment.Infrastructure.Configurations;

public static class DatabaseConfiguration
{
    public static WebApplicationBuilder GetDatabaseConfiguration(this WebApplicationBuilder webApplicationBuilder)
    {
        var connectionString = webApplicationBuilder.Configuration["EnrollmentPostgresConnection:ConnectionString"];

        webApplicationBuilder.Services.AddDbContext<EnrollmentContext>(options =>
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