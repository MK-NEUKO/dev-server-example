using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.GatewayConfigs.Events;

public sealed record NewConfigCreatedDomainEvent(Guid ConfigurationId) : IDomainEvent;