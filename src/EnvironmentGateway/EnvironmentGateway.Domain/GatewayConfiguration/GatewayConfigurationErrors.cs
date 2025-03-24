using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.GatewayConfiguration;

public static class GatewayConfigurationErrors
{
    public static Error CreateInitialConfigurationFailed = new(
        "CreateInitialConfigurationFailed", 
        "Failed to create initial configuration.");
}