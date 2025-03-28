using EnvironmentGateway.Application.Abstractions.Messaging;

namespace EnvironmentGateway.Application.GatewayConfigs.CreateInitialConfig;

public record CreateInitialConfigCommand(string Name) : ICommand<Guid>;