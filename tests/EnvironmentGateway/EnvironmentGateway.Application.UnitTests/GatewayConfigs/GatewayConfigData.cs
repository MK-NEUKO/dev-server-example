using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.Shared;

namespace EnvironmentGateway.Application.UnitTests.GatewayConfigs;

internal static class GatewayConfigData
{
    public static GatewayConfig Create() => GatewayConfig.CreateInitialConfiguration(ConfigName);

    public static readonly string ConfigName = "TestConfig";
}