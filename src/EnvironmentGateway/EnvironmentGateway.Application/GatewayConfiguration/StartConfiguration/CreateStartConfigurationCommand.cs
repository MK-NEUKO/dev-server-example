using EnvironmentGateway.Application.Abstractions.Messaging;

namespace EnvironmentGateway.Application.GatewayConfiguration.StartConfiguration;

public record CreateStartConfigurationCommand() : ICommand<Guid>;