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
using Fitness.Common.Projection;

namespace Fitness.Common;

public static class CommonConfigurations
{
    public static IServiceCollection GetCommonDependencyInjection(this IServiceCollection services)
    {
        //EMAIL
        services.AddScoped<IEmailRepository, EmailRepository>();

        //LOGGER
        services.AddScoped(typeof(ILoggerManager<>), typeof(LoggerManager<>));

        //EVENT
        services.AddScoped<IStore, Store>();
        services.AddScoped<IEventBus, EventBus>();
        services.AddScoped<IProjection, Projection.Projection>();
        services.AddScoped<IOutboxListener, OutboxListener>();
        services.AddScoped<IOutboxStore, OutboxStore>();
        services.AddScoped<IRabbitEventListener, RabbitEventListener>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        //MONGO
        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

        //CACHE
        services.AddScoped<ICacheRepository, CacheRepository>();


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
        services.ConfigurationMongoOutboxDatabase(configuration);
        services.GetCommonDependencyInjection();
        services.GetMediatR();

        services.Configure<EventStoreOptions>(configuration.GetSection("EventStoreOptions"));

        return services;
    }
    public static IServiceCollection RegisterBackgroundProcess(this IServiceCollection services)
    {
        services.AddHostedService<OutboxProcessor>();

        return services;
    }

    public static IServiceCollection ConfigurationMongoOutboxDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new MongoOutboxOptions();
        configuration.GetSection(nameof(MongoOutboxOptions)).Bind(options);

        services.Configure<MongoOutboxOptions>(configuration.GetSection(nameof(MongoOutboxOptions)));
        services.AddSingleton<MongoContext>(_ => new MongoContext(options.ConnectionString,
            options.DatabaseName, options.CollectionName));

        return services;
    }
     
    public static IServiceCollection GetCacheConfiguration(this IServiceCollection services, CacheOptions options)
    {
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

