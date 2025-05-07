using EnvironmentGateway.Domain.Routes;
using FluentAssertions;

namespace EnvironmentGateway.Domain.UnitTests.Routes;

public class RoutesTests
{
    [Fact]
    public void CreateInitialRoute_Should_SetPropertyValues()
    {
        const string routeName = "testRoute";
        const string clusterName = "testClusterName";
        const string matchPath = "{**catch-all}";

        var initRoute = Route.CreateNewRoute(routeName, clusterName, matchPath);

        initRoute.RouteName.Value.Should().Be(routeName);
        initRoute.ClusterName.Value.Should().Be(clusterName);
        initRoute.Match.Path.Value.Should().Be(matchPath);
    }
}