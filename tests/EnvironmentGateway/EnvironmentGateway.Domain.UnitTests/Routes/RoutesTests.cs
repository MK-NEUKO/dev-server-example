using EnvironmentGateway.Domain.Routes;
using FluentAssertions;

namespace EnvironmentGateway.Domain.UnitTests.Routes;

public class RoutesTests
{
    [Fact]
    public void CreateRoute_Should_SetPropertyValues()
    {
        const string routeName = "testRoute";
        const string clusterName = "testClusterName";
        const string matchPath = "{**catch-all}";

        var newRoute = Route.Create(routeName, clusterName, matchPath);

        newRoute.RouteName.Value.Should().Be(routeName);
        newRoute.ClusterName.Value.Should().Be(clusterName);
        newRoute.Match?.Path.Value.Should().Be(matchPath);
    }
}
