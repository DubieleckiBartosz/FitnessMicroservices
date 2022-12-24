using Enrollment.Application.Interfaces;
using Enrollment.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Enrollment.Infrastructure.Configurations;

public static class DependencyInjectionInfrastructureLayer
{
    public static WebApplicationBuilder GetDependencyInjectionInfrastructureLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

        return builder;
    }
}