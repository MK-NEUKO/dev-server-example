using EnvironmentGateway.Domain.Routes.Transforms;
using FluentAssertions;
using System;
using Xunit;

namespace EnvironmentGateway.Domain.UnitTests.Routes.Transforms;

public class RouteTransformsTests
{
    private const string Key2 = "key2";
    private const string Value2 = "value2";
    private const string TestKey = "PathPrefix";
    private const string TestValue = "/prefix";

    [Fact]
    public void CreateRouteTransforms_Should_SetPropertyValues()
    {
        // Act
        var newTransforms = RouteTransforms.Create(TestKey, TestValue);

        // Assert
        newTransforms.TransformsItems[0].Key.Should().Be(TestKey);
        newTransforms.TransformsItems[0].Value.Should().Be(TestValue);
    }
    
    [Fact]
    public void AddTransformsItem_Should_AddTransformItemToList()
    {
        // Arrange
        var routeTransforms = RouteTransforms.Create(TestKey, TestValue);

        // Act
        routeTransforms.AddTransformsItem(Key2, Value2);

        // Assert
        routeTransforms.TransformsItems.Count.Should().Be(2);
        routeTransforms.TransformsItems[1].Key.Should().Be(Key2);
        routeTransforms.TransformsItems[1].Value.Should().Be(Value2);
    }

    [Theory]
    [InlineData(null, TestValue)]
    [InlineData(TestKey, null)]
    public void Create_Should_ThrowArgumentNullException_WhenKeyOrValueIsNull(string? key, string? value)
    {
        // Act
        Action act = () => RouteTransforms.Create(key!, value!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData(null, Value2)]
    [InlineData(Key2, null)]
    public void AddTransformsItem_Should_ThrowArgumentNullException_WhenKeyOrValueIsNull(string? key, string? value)
    {
        // Arrange
        var routeTransforms = RouteTransforms.Create(TestKey, TestValue);

        // Act
        Action act = () => routeTransforms.AddTransformsItem(key!, value!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void TransformsItems_Should_Contain_OneItem_AfterCreate()
    {
        // Arrange & Act
        var routeTransforms = RouteTransforms.Create(TestKey, TestValue);

        // Assert
        routeTransforms.TransformsItems.Should().HaveCount(1);
    }
}
