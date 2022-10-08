using System.Reflection;
using Fitness.Common.Cache;
using Fitness.Common.Communication.Email;
using Fitness.Common.Mongo;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fitness.Common;

public static class CommonConfigurations
{
    public static IServiceCollection GetCommonDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IEmailRepository, EmailRepository>();
        services.AddScoped(typeof(ILoggerManager<>), typeof(LoggerManager<>));
        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

        return services;
    }

    public static IServiceCollection GetMediatR(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }

    public static IServiceCollection GetCacheConfiguration(this IServiceCollection services, CacheOptions options)
    {
        services.AddScoped<ICacheRepository, CacheRepository>();

        if (!options.Enabled)
        {
            services.AddDistributedMemoryCache();
        }        else
        {
            services.AddStackExchangeRedisCache(cacheOptions =>
                cacheOptions.Configuration = options.RedisConnection);
        }

        return services;
    }
}

