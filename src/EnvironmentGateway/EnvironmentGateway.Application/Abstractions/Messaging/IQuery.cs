using MediatR;

namespace EnvironmentGateway.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<TResponse>
{
}