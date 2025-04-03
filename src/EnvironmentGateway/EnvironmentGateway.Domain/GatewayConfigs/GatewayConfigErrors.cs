using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.GatewayConfigs;

public static class GatewayConfigErrors
{
    public static readonly Error CreateInitialConfigFailed = new(
        "CreateInitialConfigFailed", 
        "Failed to create initial configuration.");
}