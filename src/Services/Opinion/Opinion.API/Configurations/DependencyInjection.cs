using Opinion.API.Contracts.Repositories;
using Opinion.API.Infrastructure.Repositories;

namespace Opinion.API.Configurations;

public static class DependencyInjection
{
    public static WebApplicationBuilder GetDependencyInjection(this WebApplicationBuilder builder)
    { 
        builder.Services.AddScoped<IOpinionRepository, OpinionRepository>();
        builder.Services.AddScoped<IReactionRepository, ReactionRepository>();

        return builder;
    }
}