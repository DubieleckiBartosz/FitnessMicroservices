using MediatR;

namespace Fitness.Common.Abstractions;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}