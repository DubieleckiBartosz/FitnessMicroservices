namespace Fitness.Common.Abstractions;

public interface ICommandBus
{
    Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default(CancellationToken));
}