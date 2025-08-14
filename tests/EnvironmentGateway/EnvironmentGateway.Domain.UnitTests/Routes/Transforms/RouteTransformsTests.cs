using EnvironmentGateway.Domain.Routes.Transforms;
using FluentAssertions;

namespace EnvironmentGateway.Domain.UnitTests.Routes.Transforms;

public class RouteTransformsTests
{
    [Fact]
    public void CreateRouteTransforms_Should_SetPropertyValues()
    {
        // Arrange
        const string key = "PathPrefix";
        const string value = "/prefix";

        // Act
        var newTransforms = RouteTransforms.Create(key, value);

        // Assert
        newTransforms.TransformsItems[0].Key.Should().Be(key);
        newTransforms.TransformsItems[0].Value.Should().Be(value);
    }
    
    [Fact]
    public void AddTransformsItem_Should_AddTransformItemToList()
    {
        // Arrange
        var routeTransforms = RouteTransforms.Create("key1", "value1");
        const string key = "key2";
        const string value = "value2";

        // Act
        routeTransforms.AddTransformsItem(key, value);

        // Assert
        routeTransforms.TransformsItems.Count.Should().Be(2);
        routeTransforms.TransformsItems[1].Key.Should().Be(key);
        routeTransforms.TransformsItems[1].Value.Should().Be(value);
    }
}
