using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.GatewayConfig.Events;

public sealed record InitialConfigCreatedDomainEvent(Guid ConfigurationId) : IDomainEvent;