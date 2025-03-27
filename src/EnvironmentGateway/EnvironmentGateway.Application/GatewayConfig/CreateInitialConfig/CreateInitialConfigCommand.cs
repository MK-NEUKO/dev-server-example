using EnvironmentGateway.Application.Abstractions.Messaging;

namespace EnvironmentGateway.Application.GatewayConfig.CreateInitialConfig;

public record CreateInitialConfigCommand(string Name) : ICommand<Guid>;