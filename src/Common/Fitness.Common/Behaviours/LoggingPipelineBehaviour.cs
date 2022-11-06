using MediatR.Pipeline;

namespace Fitness.Common.Behaviours;

public class LoggingPipelineBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILoggerManager<LoggingPipelineBehaviour<TRequest>> _logger;

    public LoggingPipelineBehaviour(ILoggerManager<LoggingPipelineBehaviour<TRequest>> logger)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var name = typeof(TRequest).Name;
        _logger.LogInformation(null, $"EventManagement Request: {name} - {request}");

        return Task.CompletedTask;
    }
}