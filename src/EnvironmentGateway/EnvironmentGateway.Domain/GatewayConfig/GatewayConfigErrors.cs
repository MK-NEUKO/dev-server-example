using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.GatewayConfig;

public static class GatewayConfigErrors
{
    public static Error CreateInitialConfigFailed = new(
        "CreateInitialConfigFailed", 
        "Failed to create initial configuration.");
}