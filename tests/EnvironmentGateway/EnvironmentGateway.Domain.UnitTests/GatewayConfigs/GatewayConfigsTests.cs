using System.ComponentModel;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.GatewayConfigs.Events;
using EnvironmentGateway.Domain.UnitTests.Infrastructure;
using FluentAssertions;

namespace EnvironmentGateway.Domain.UnitTests.GatewayConfigs;

public class GatewayConfigsTests : BaseTest
{
    [Fact]
    public void CreateInitialConfig_Should_SetPropertyValues()
    {
        // Arrange
        const string configName = "New Configuration";

        // Act
        var newConfig = GatewayConfig.CreateNewConfig();

        // Assert
        newConfig.Name.Value.Should().Be(configName);
        newConfig.IsCurrentConfig.Should().BeTrue();
        newConfig.Routes.Count.Should().BeGreaterThanOrEqualTo(1);
        newConfig.Clusters.Count.Should().BeGreaterThanOrEqualTo(1);
    }

    [Fact]
    public void CreateInitialConfig_Should_RaiseInitialConfigCreatedDomainEvent()
    {
        // Act
        var initialConfig = GatewayConfig.CreateNewConfig();

        // Assert
        NewConfigCreatedDomainEvent domainEvent = AssertDomainEventWasPublished<NewConfigCreatedDomainEvent>(initialConfig);
        domainEvent.ConfigurationId.Should().Be(initialConfig.Id);
    }

}
