using EnvironmentGateway.Domain.Clusters;
using EnvironmentGateway.Domain.Clusters.Destinations;
using FluentAssertions;

namespace EnvironmentGateway.Domain.UnitTests.Clusters;

public class ClustersTests
{
    private const string TestClusterName = "testCluster";
    private const string TestDestinationName = "testDestination";
    private const string TestDestinationAddress = "https://test-address.test";
    private const string NewDestinationName = "newDestination";
    private const string NewDestinationAddress = "https://new-address.test";
    

    [Fact]
    public void CreateInitialCluster_Should_SetPropertyValues()
    {
        // Arrange & Act
        var newCluster = Cluster.Create(
            TestClusterName,
            TestDestinationName,
            TestDestinationAddress);

        // Assert
        newCluster.ClusterName.Value.Should().Be(TestClusterName);
        newCluster.Destinations[0].DestinationName.Value.Should().Be(TestDestinationName);
        newCluster.Destinations[0].Address.Value.Should().Be(TestDestinationAddress);
    }
    
    [Fact]
    public void AddDestination_Should_AddDestinationToTheList()
    {
        //Arrange
        var cluster = Cluster.Create(TestClusterName, TestDestinationName, TestDestinationAddress);
        var newDestination = Destination.Create(NewDestinationName, NewDestinationAddress);

        //Act
        cluster.AddDestination(newDestination);

        //Assert
        cluster.Destinations.Count.Should().Be(2);
        cluster.Destinations[1].DestinationName.Value.Should().Be(NewDestinationName);
        cluster.Destinations[1].Address.Value.Should().Be(NewDestinationAddress);
    }

    [Theory]
    [InlineData(null, TestDestinationName, TestDestinationAddress)]
    [InlineData(TestClusterName, null, TestDestinationAddress)]
    [InlineData(TestClusterName, TestDestinationName, null)]
    public void Create_Should_ThrowArgumentNullException_When_ParameterIsNull(
        string? clusterName, string? destinationName, string? destinationAddress)
    {
        // Arrange & Act & Assert
        Action act = () => Cluster.Create(clusterName!, destinationName!, destinationAddress!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddDestination_Should_ThrowException_When_DestinationIsNull()
    {
        //Arrange
        var cluster = Cluster.Create(TestClusterName, TestDestinationName, TestDestinationAddress);
        Destination newDestination = null!;

        //Act & Assert
        Action act = () => cluster.AddDestination(newDestination!);
        act.Should().Throw<ArgumentNullException>();
    }
}
