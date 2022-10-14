using System.Reflection;
using Fitness.Common.Cache;
using Fitness.Common.Communication.Email;
using Fitness.Common.Mongo;
using Fitness.Common.Outbox.MongoOutbox;
using Fitness.Common.Outbox;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fitness.Common.EventStore;
using Fitness.Common.EventStore.Events;
using Fitness.Common.EventStore.Repository;
using Fitness.Common.RabbitMQ;

namespace Fitness.Common;

public static class CommonConfigurations
{
    public static IServiceCollection GetCommonDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IEmailRepository, EmailRepository>();
        services.AddScoped(typeof(ILoggerManager<>), typeof(LoggerManager<>));

        return services;
    }

    public static IServiceCollection GetAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
    public static IServiceCollection GetMediatR(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }

    public static IServiceCollection EventStoreConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.GetMediatR();
        services.AddScoped<IStore, Store>();
        services.AddScoped<IEventBus, EventBus>();
        services.AddScoped<IOutboxListener, OutboxListener>(); 
        services.AddScoped<IOutboxStore, OutboxStore>();
        services.AddScoped<IRabbitEventListener, RabbitEventListener>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.Configure<EventStoreOptions>(configuration.GetSection("EventStoreOptions"));

        return services;
    }
    public static IServiceCollection RegisterBackgroundProcess(this IServiceCollection services)
    {
        services.AddHostedService<OutboxProcessor>();

        return services;
    }

    public static IServiceCollection GetMongoDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new MongoOutboxOptions();
        configuration.GetSection(nameof(MongoOutboxOptions)).Bind(options);

        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

        services.Configure<MongoOutboxOptions>(configuration.GetSection(nameof(MongoOutboxOptions)));
        services.AddSingleton<MongoContext>(_ => new MongoContext(options.ConnectionString,
            options.DatabaseName, options.CollectionName));

        services.AddScoped<IOutboxStore, OutboxStore>();

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

    public static IApplicationBuilder UseSubscribeEvent<T>(this IApplicationBuilder app) where T : IEvent
    {
        app.ApplicationServices.GetRequiredService<IRabbitEventListener>().Subscribe<T>();

        return app;
    }
}

