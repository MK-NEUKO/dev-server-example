using EnvironmentGateway.Application.Abstractions.Messaging;

namespace EnvironmentGateway.Application.GatewayConfigs.CreateNewConfig;

public record CreateNewConfigCommand() : ICommand<Guid>;