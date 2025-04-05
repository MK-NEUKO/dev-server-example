using EnvironmentGateway.Domain.GatewayConfigs;
using FluentAssertions;

namespace EnvironmentGateway.Domain.UnitTests.GatewayConfigs;

public class GatewayConfigsTests
{
    [Fact]
    public void CreateInitialConfig_Should_SetPropertyValues()
    {
        const string configName = "testConfig";

        var initConfig = GatewayConfig.CreateInitialConfiguration(configName);

        initConfig.Name.Value.Should().Be(configName);
        initConfig.IsCurrentConfig.Should().BeTrue();
        initConfig.Routes.Count.Should().BeGreaterThanOrEqualTo(1);
        initConfig.Clusters.Count.Should().BeGreaterThanOrEqualTo(1);
    }

}