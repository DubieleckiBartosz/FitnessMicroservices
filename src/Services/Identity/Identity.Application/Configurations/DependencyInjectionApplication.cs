using Fitness.Common.Logging;
using Identity.Application.Services;

namespace Identity.Application.Configurations;

public static class DependencyInjectionApplication
{
    public static IServiceCollection GetValidators(this IServiceCollection services)
    { 
        services.AddScoped<IValidator<RegisterParameters>, RegisterParametersValidator>();
        services.AddScoped<IValidator<UserNewRoleParameters>, UserNewRoleParametersValidator>();
        services.AddScoped<IValidator<LoginParameters>, LoginParametersValidator>();

        return services;
    }

    public static IServiceCollection GetDependencyInjectionApplication(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped(typeof(ILoggerManager<>), typeof(LoggerManager<>));

        return services;
    }
}