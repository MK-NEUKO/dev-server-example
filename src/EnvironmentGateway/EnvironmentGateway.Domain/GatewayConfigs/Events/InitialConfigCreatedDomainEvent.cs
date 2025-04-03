using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.GatewayConfigs.Events;

public sealed record InitialConfigCreatedDomainEvent(Guid ConfigurationId) : IDomainEvent;