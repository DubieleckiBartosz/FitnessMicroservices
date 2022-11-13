using Fitness.Common;
using Microsoft.AspNetCore.Builder;

namespace Enrollment.Application;

public static class Configurations
{
    public static IApplicationBuilder SubscribeEvents(this IApplicationBuilder builder)
    {
        builder.UseSubscribeAllEvents(typeof(AssemblyEnrollmentApplicationReference).Assembly);

        return builder;
    }
}