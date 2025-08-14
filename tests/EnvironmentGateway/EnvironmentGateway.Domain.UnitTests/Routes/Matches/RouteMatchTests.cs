using EnvironmentGateway.Domain.Routes.Matches;
using FluentAssertions;

namespace EnvironmentGateway.Domain.UnitTests.Routes.Matches;

public class RouteMatchTests
{
    [Fact]
    public void CreateRouteMatch_Should_SetPropertyValues()
    {
        // Arrange
        const string matchPath = "{**catch-all}";

        // Act
        var newMatch = RouteMatch.Create(matchPath);

        // Assert
        newMatch.Path.Value.Should().Be(matchPath);
    }
}
