using EnvironmentGateway.Domain.Abstractions;
using MediatR;

namespace EnvironmentGateway.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}