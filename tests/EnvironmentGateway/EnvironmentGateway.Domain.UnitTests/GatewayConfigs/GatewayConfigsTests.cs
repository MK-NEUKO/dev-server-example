using EnvironmentGateway.Domain.Clusters;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.GatewayConfigs.Events;
using EnvironmentGateway.Domain.Routes;
using EnvironmentGateway.Domain.UnitTests.Infrastructure;
using FluentAssertions;

namespace EnvironmentGateway.Domain.UnitTests.GatewayConfigs;

public class GatewayConfigsTests : BaseTest
{
    private const string TestClusterName = "TestCluster";
    private const string TestDestinationName = "TestDestination";
    private const string TestDestinationAddress = "http://test-address.com";
    private const string TestRouteName = "TestRoute";
    private const string TestRoutePath = "/test";

    [Fact]
    public void CreateConfig_Should_SetPropertyValues()
    {
        // Arrange & Act
        var gatewayConfig = CreateTestConfig();

        // Assert
        gatewayConfig.Name?.Value.Should().Be(ConfigDefaultParams.ConfigName);
        gatewayConfig.IsCurrentConfig.Should().BeTrue();
        gatewayConfig.Routes.Count.Should().BeGreaterThanOrEqualTo(1);
        gatewayConfig.Clusters.Count.Should().BeGreaterThanOrEqualTo(1);
    }

    [Fact]
    public void CreateConfig_Should_RaiseInitialConfigCreatedDomainEvent()
    {
        // Act
        var gatewayConfig = CreateTestConfig();

        // Assert
        NewConfigCreatedDomainEvent domainEvent = AssertDomainEventWasPublished<NewConfigCreatedDomainEvent>(gatewayConfig);
        domainEvent.ConfigurationId.Should().Be(gatewayConfig.Id);
    }

    [Fact]
    public void AddCluster_Should_AddClusterToList()
    {
        // Arrange
        var gatewayConfig = CreateTestConfig();
        var cluster = Cluster.Create(TestClusterName, TestDestinationName, TestDestinationAddress);

        // Act
        gatewayConfig.AddCluster(cluster);

        // Assert
        gatewayConfig.Clusters.Should().Contain(cluster);
    }

    [Fact]
    public void AddRoute_Should_AddRouteToList()
    {
        // Arrange
        var gatewayConfig = CreateTestConfig();
        var route = Route.Create(TestRouteName, TestClusterName, TestRoutePath);

        // Act
        gatewayConfig.AddRoute(route);

        // Assert
        gatewayConfig.Routes.Should().Contain(route);
    }

    [Fact]
    public void AddCluster_Should_ThrowArgumentNullException_WhenClusterIsNull()
    {
        // Arrange
        var gatewayConfig = CreateTestConfig();

        // Act
        Action act = () => gatewayConfig.AddCluster(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddRoute_Should_ThrowArgumentNullException_WhenRouteIsNull()
    {
        // Arrange
        var gatewayConfig = CreateTestConfig();

        // Act
        Action act = () => gatewayConfig.AddRoute(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    private static GatewayConfig CreateTestConfig() => GatewayConfig.Create();
}
