using Fitness.Common;

namespace Training.API.Configurations;

public static class QueueListenerConfiguration
{
    public static WebApplication ListenEvents(this WebApplication app)
    {
        app.UseSubscribeAllEvents(typeof(AssemblyTrainingApplicationReference).Assembly);

        return app;
    }
}
