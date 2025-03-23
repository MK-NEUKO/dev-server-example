using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.GatewayConfiguration.Events;

public sealed record InitialConfigurationCreatedDomainEvent(Guid ConfigurationId) : IDomainEvent;