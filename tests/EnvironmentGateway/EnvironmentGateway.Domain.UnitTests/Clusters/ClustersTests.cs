using EnvironmentGateway.Domain.Clusters;
using FluentAssertions;

namespace EnvironmentGateway.Domain.UnitTests.Clusters;

public class ClustersTests
{
    [Fact]
    public void CreateInitialCluster_Should_SetPropertyValues()
    {
        // Arrange
        const string clusterName = "TestName";
        const string destinationAddress = "https://tests.test";

        // Act
        var newCluster = Cluster.CreateNewCluster(clusterName, 1);

        // Assert
        newCluster.ClusterName.Value.Should().Be(clusterName);
        newCluster.Destinations[0].Address.Value.Should().Be(destinationAddress);
    }
}
