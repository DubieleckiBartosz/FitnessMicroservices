using Fitness.Common;
using Microsoft.AspNetCore.Builder;

namespace Enrollment.Application;

public static class Configurations
{
    public static WebApplication SubscribeEvents(this WebApplication app)
    {
        app.UseSubscribeAllEvents(typeof(AssemblyEnrollmentApplicationReference).Assembly);

        return app;
    }
}