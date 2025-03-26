using EnvironmentGateway.Application.Abstractions.Messaging;

namespace EnvironmentGateway.Application.GatewayConfig.InitialConfig;

public record CreateInitialConfigCommand(string Name) : ICommand<Guid>;