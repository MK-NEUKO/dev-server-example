using System;
using EnvironmentGateway.Domain.Routes.Match;
using FluentAssertions;
using Xunit;

namespace EnvironmentGateway.Domain.UnitTests.Routes.Match;

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

    [Fact]
    public void CreateRouteMatch_Should_ThrowArgumentNullException_WhenPathIsNull()
    {
        // Act
        Action act = () => RouteMatch.Create(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }
}
