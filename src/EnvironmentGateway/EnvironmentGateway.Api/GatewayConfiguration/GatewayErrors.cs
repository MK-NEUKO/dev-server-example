using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Api.GatewayConfiguration;

public static class GatewayErrors
{
    public static Error UpdateDefaultProxyConfigFailed = new(
        "UpdateDefaultProxyConfigFailed",
        "Failed to update the default proxy config to current config from the database.");

    public static Error UpdateProxyConfigFailed = new(
        "UpdateProxyConfigError",
        "Failed to update proxy config to current config from the database.");
}