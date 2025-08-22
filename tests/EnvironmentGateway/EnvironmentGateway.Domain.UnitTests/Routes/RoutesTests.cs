using EnvironmentGateway.Domain.Routes;
using EnvironmentGateway.Domain.Routes.Transforms;
using FluentAssertions;

namespace EnvironmentGateway.Domain.UnitTests.Routes;

public class RoutesTests
{
    private const string TestKey = "key";
    private const string TestValue = "value";
    private const string TestRouteName = "testRoute";
    private const string TestClusterName = "testCluster";
    private const string TestMatchPath = "{**catch-all}";

    [Fact]
    public void CreateRoute_Should_SetPropertyValues()
    {
        var newRoute = Route.Create(TestRouteName, TestClusterName, TestMatchPath);

        newRoute.RouteName.Value.Should().Be(TestRouteName);
        newRoute.ClusterName.Value.Should().Be(TestClusterName);
        newRoute.Match?.Path.Value.Should().Be(TestMatchPath);
    }

    [Theory]
    [InlineData(null, TestClusterName, TestMatchPath)]
    [InlineData(TestRouteName, null, TestMatchPath)]
    [InlineData(TestRouteName, TestClusterName, null)]
    public void CreateRoute_Should_ThrowArgumentNullException_WhenArgumentIsNull(
        string? routeName,
        string? clusterName, 
        string? matchPath)
    {
        Action act = () => Route.Create(routeName!, clusterName!, matchPath!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddTransforms_Should_SetTransformsProperty()
    {
        var route = Route.Create(TestRouteName, TestClusterName, TestMatchPath);
        var transforms = RouteTransforms.Create(TestKey, TestValue);
        route.AddTransforms(transforms);
        route.Transforms.Should().Be(transforms);
    }

    [Fact]
    public void AddTransforms_Should_ThrowArgumentNullException_WhenNull()
    {
        var route = Route.Create(TestRouteName, TestClusterName, TestMatchPath);
        Action act = () => route.AddTransforms(null!);
        act.Should().Throw<ArgumentNullException>();
    }
}
