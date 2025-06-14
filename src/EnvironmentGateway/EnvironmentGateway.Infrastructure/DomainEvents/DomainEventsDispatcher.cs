using System.Collections.Concurrent;
using EnvironmentGateway.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EnvironmentGateway.Infrastructure.DomainEvents;

internal sealed class DomainEventsDispatcher(
    IServiceProvider serviceProvider) 
    : IDomainEventsDispatcher
{
    private static readonly ConcurrentDictionary<Type, Type> HandlerTypeDictionary = new();
    private static readonly ConcurrentDictionary<Type, Type> WrapperTypeDictionary = new();
    public async Task DispatchAsync(
        IEnumerable<IDomainEvent> domainEvents,
        CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            using var scope = serviceProvider.CreateScope();

            var domainEventType = domainEvent.GetType();
            var handlerType = HandlerTypeDictionary.GetOrAdd(
                domainEventType,
                eventType => typeof(IDomainEventHandler<>).MakeGenericType(eventType));
            
            var handlers = scope.ServiceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                if (handler is null)
                {
                    continue;
                }

                var handlerWrapper = HandlerWrapper.Create(handler, domainEvent.GetType());
                
                await handlerWrapper.Handle(domainEvent, cancellationToken);
            }
        }
    }

    private abstract class HandlerWrapper
    {
        public abstract Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken);

        public static HandlerWrapper Create(object handler, Type domainEventType)
        {
            var wrapperType = WrapperTypeDictionary.GetOrAdd(
                domainEventType,
                eventType => typeof(HandlerWrapper<>).MakeGenericType(eventType));
            
            return (HandlerWrapper)Activator.CreateInstance(wrapperType, handler);
        }
    }
    
    private sealed class HandlerWrapper<T>(object handler) : HandlerWrapper where T : IDomainEvent
    {
        private readonly IDomainEventHandler<T> _handler = (IDomainEventHandler<T>)handler;
        
        public override async Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await _handler.Handle((T)domainEvent, cancellationToken);
        }
    }
}