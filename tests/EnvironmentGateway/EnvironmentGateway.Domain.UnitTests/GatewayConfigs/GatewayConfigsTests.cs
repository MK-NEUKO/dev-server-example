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
        var configName = "New Configuration";

        // Act
        var initialConfig = GatewayConfig.CreateNewConfig();

        // Assert
        initialConfig.Name.Value.Should().Be(configName);
        initialConfig.IsCurrentConfig.Should().BeTrue();
        initialConfig.Routes.Count.Should().BeGreaterThanOrEqualTo(1);
        initialConfig.Clusters.Count.Should().BeGreaterThanOrEqualTo(1);
    }

    [Fact]
    public void CreateInitialConfig_Should_RaiseInitialConfigCreatedDomainEvent()
    {
        // Act
        var initialConfig = GatewayConfig.CreateNewConfig();

        // Assert
        var domainEvent = AssertDomainEventWasPublished<NewConfigCreatedDomainEvent>(initialConfig);

        domainEvent.ConfigurationId.Should().Be(initialConfig.Id);
    }

}