using EnvironmentGateway.Domain.Clusters.Destinations;
using FluentAssertions;

namespace EnvironmentGateway.Domain.UnitTests.Destinations;

public class DestinationsTests
{
    [Fact]
    public void CreateNewDestination_Should_SetPropertyValues()
    {
        // Arrange
        const string destinationName = "TestName";
        const string destinationAddress = "https://example.com";
        
        // Act
        var newDestination = Destination.Create(destinationName, destinationAddress);

        // Assert
        newDestination.DestinationName.Value.Should().Be(destinationName);
        newDestination.Address.Value.Should().Be(destinationAddress);
    }
}
