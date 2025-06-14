using EnvironmentGateway.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EnvironmentGateway.Infrastructure.DomainEvents;

internal sealed class DomainEventsDispatcher(
    IServiceProvider serviceProvider) 
    : IDomainEventsDispatcher
{
    public async Task DispatchAsync(
        IEnumerable<IDomainEvent> domainEvents,
        CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            using var scope = serviceProvider.CreateScope();

            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            var handlers = scope.ServiceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                if (handler is null)
                {
                    continue;
                }

                var handleMethod = handlerType.GetMethod("Handle");

                if (handleMethod is not null)
                {
                    await (Task)handleMethod.Invoke(handler, [domainEvent, cancellationToken])!;
                }
            }
        }
    }
}