using MediatR;

namespace Fitness.Common.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}