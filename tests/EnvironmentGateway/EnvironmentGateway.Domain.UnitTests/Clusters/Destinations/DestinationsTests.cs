using EnvironmentGateway.Domain.Clusters.Destinations;
using FluentAssertions;

namespace EnvironmentGateway.Domain.UnitTests.Clusters.Destinations;

public class DestinationsTests
{
    private const string TestName = "TestName";
    private const string TestAddress = "https://test-address.com";
    private const string NewAddress = "https://new-address.com";

    [Fact]
    public void CreateNewDestination_Should_SetPropertyValues()
    {
        // Arrange & Act
        var destination = Destination.Create(TestName, TestAddress);

        // Assert
        destination.DestinationName.Value.Should().Be(TestName);
        destination.Address.Value.Should().Be(TestAddress);
    }

    [Theory]
    [InlineData(null, TestAddress)]
    [InlineData(TestName, null)]
    public void Create_Should_ThrowArgumentNullException_When_ParameterIsNull(string? name, string? address)
    {
        // Act
        Action act = () => Destination.Create(name!, address!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ChangeAddress_Should_ChangeAddress()
    {
        // Arrange
        var destination = Destination.Create(TestName, TestAddress);

        // Act
        destination.ChangeAddress(NewAddress);

        // Assert
        destination.Address.Value.Should().Be(NewAddress);
    }

    [Fact]
    public void ChangeAddress_Should_ThrowArgumentNullException_When_AddressIsNull()
    {
        // Arrange
        var destination = Destination.Create(TestName, TestAddress);

        // Act
        Action act = () => destination.ChangeAddress(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }
}
