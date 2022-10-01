using Identity.Infrastructure.Repositories;

namespace Identity.Infrastructure.Configurations;

public static class DependencyInjectionInfrastructure
{
    public static IServiceCollection GetDependencyInjectionInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITransaction, TransactionSupervisor>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}