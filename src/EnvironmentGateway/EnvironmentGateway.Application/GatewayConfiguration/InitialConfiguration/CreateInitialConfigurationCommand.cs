using EnvironmentGateway.Application.Abstractions.Messaging;

namespace EnvironmentGateway.Application.GatewayConfiguration.StartConfiguration;

public record CreateInitialConfigurationCommand() : ICommand<Guid>;