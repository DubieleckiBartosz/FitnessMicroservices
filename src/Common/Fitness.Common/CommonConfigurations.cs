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
using Fitness.Common.Abstractions;
using Fitness.Common.Behaviours;
using Fitness.Common.CommonServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Fitness.Common;

public static class CommonConfigurations
{
    public static IServiceCollection GetAccessoriesDependencyInjection(this IServiceCollection services)
    {
        //EMAIL
        services.AddScoped<IEmailRepository, EmailRepository>();

        //LOGGER
        services.AddSingleton(typeof(ILoggerManager<>), typeof(LoggerManager<>));
         
        return services;
    }
    public static IServiceCollection GetFullDependencyInjection(this IServiceCollection services,
        Func<IServiceProvider, List<IProjection>>? projectionFunc)
    {
        //EMAIL
        services.GetAccessoriesDependencyInjection();

        //EVENT
        services.AddScoped<ICommandBus, CommandBus>();
        services.AddScoped<IQueryBus, QueryBus>();
        services.AddScoped<IStore, Store>();
        services.AddScoped<IEventBus, EventBus>();
        services.AddScoped<IEventStore, EventStore.EventStore>(_ =>
            new EventStore.EventStore(_.GetService<IStore>() ?? throw new ArgumentNullException(nameof(IStore)),
                projectionFunc?.Invoke(_)));
        services.AddScoped<IOutboxListener, OutboxListener>();
        services.AddScoped<IOutboxStore, OutboxStore>();
        services.AddScoped<IRabbitEventListener, RabbitEventListener>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        //USER
        services.AddHttpContextAccessor();
        services.AddTransient<ICurrentUser, CurrentUser>();

        //MONGO
        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

        //PIPELINES
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));

        return services;
    }

    public static WebApplicationBuilder GetAutoMapper(this WebApplicationBuilder builder, Assembly assembly)
    {
        builder.Services.AddAutoMapper(assembly);

        return builder;
    }
    public static IServiceCollection GetMediatR(this IServiceCollection services, params Type[] types)
    {
        var assemblies = types.Select(type => type.GetTypeInfo().Assembly);

        foreach (var assembly in assemblies)
        {
            services.AddMediatR(assembly);
        } 

        return services;
    }

    public static WebApplicationBuilder EventStoreConfiguration(this WebApplicationBuilder builder, Func<IServiceProvider, List<IProjection>>? projectionFunc = null, params Type[] types)
    {
        builder.Services.ConfigurationMongoOutboxDatabase(builder.Configuration);
        builder.Services.GetFullDependencyInjection(projectionFunc);
        builder.Services.GetMediatR(types);
        builder.RegisterRabbitMq();

        builder.Services.Configure<EventStoreOptions>(builder.Configuration.GetSection("EventStoreOptions"));

        return builder;
    }

    public static void RegisterRabbitMq(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IRabbitBase, RabbitBase>();
        builder.Services.Configure<RabbitOptions>(builder.Configuration.GetSection("RabbitOptions"));
    }
    public static WebApplicationBuilder RegisterBackgroundProcess(this WebApplicationBuilder builder)
    {
        builder.Services.AddHostedService<OutboxProcessor>();

        return builder;
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
    public static WebApplication UseSubscribeAllEvents(this WebApplication app, Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(mytype => mytype.GetInterfaces().Contains(typeof(IEvent)));

        foreach (var type in types)
        {
            app.UseSubscribeEvent(type);
        }

        return app;
    }

    public static WebApplication UseSubscribeEvent(this WebApplication app, Type type)
    {
        using var scope = app.Services.CreateScope();

        var requiredService = scope.ServiceProvider.GetRequiredService<IRabbitEventListener>();

        requiredService?.Subscribe(type);

        return app;
    }

    public static IServiceCollection RegisterJwtBearer(this IServiceCollection services, string issuer, string audience, string key)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
                o.Events = new JwtBearerEvents()
                {
                    //OnChallenge = async (context) =>
                    //{

                    //    if (context.AuthenticateFailure != null)
                    //    {
                    //        var error = string.IsNullOrEmpty(context.ErrorDescription) ? context.AuthenticateFailure?.Message : context.ErrorDescription;
                    //        context.HandleResponse();
                    //        context.Response.ContentType = "application/json";
                    //        context.Response.StatusCode = 401;
                    //        await context.Response.WriteAsJsonAsync(new {Message = $"401 Not authorized: {error}" });
                    //    }
                    //}

                    OnChallenge = async (context) =>
                    {
                        if (context.AuthenticateFailure != null)
                        {
                            var error = string.IsNullOrEmpty(context.ErrorDescription)
                                ? context.AuthenticateFailure?.Message
                                : context.ErrorDescription;
                            context.HandleResponse();
                            context.Response.ContentType = "application/json";
                            var statusCode = context?.AuthenticateFailure is SecurityTokenExpiredException
                                ? 403
                                : 401;
                            if (context != null)
                            {
                                context.Response.StatusCode = statusCode;
                                await context.Response.WriteAsJsonAsync(new
                                {
                                    ErrorMessage = statusCode == 403
                                        ? $"403 Expired token: {error}"
                                        : $"401 Not authorized: {error}"
                                });
                            }
                        }
                    }
                };
            });

        return services;
    }

    public static WebApplication MigrateDatabase<TDbContext>(this WebApplication webApplication,
        Action<IServiceProvider, Exception> handlingExceptionAction) where TDbContext : DbContext
    {
        using var scope = webApplication.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var db = services.GetRequiredService<TDbContext>();
            var dbExists = db.Database.GetService<IRelationalDatabaseCreator>().Exists();
            if (dbExists)
            {
                if (db.Database.CanConnect())
                {
                    if (db.Database.IsRelational())
                    {
                        var pendingMigrations = db.Database.GetPendingMigrations();
                        if (pendingMigrations.Any())
                        {
                            db.Database.Migrate();
                        }
                    }
                }
            }
            else
            {
                var pendingMigrations = db.Database.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                    db.Database.Migrate();
                }
            }
        }
        catch (Exception ex)
        {
            handlingExceptionAction.Invoke(services, ex);
        }

        return webApplication;
    }
}

