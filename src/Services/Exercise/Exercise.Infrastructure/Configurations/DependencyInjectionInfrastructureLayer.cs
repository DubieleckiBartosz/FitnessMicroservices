using Exercise.Application.Contracts;
using Exercise.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Exercise.Infrastructure.Configurations;

public static class DependencyInjectionInfrastructureLayer
{
    public static WebApplicationBuilder GetDependencyInjectionInfrastructureLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();

        return builder;
    }
}