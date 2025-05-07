using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.GatewayConfigs;

public static class GatewayConfigErrors
{
    public static readonly Error CreateNewConfigFailed = new(
        "CreateNewConfigFailed", 
        "Failed to create new configuration.");
}