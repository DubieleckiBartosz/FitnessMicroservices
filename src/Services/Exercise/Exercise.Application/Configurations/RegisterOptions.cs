using Exercise.Application.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Exercise.Application.Configurations;

public static class RegisterOptions
{
    public static WebApplicationBuilder GetOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<DatabaseConnection>(builder.Configuration.GetSection("DatabaseConnection")); 

        return builder;
    }
}