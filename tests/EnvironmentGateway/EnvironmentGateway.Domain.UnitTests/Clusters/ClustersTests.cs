using EnvironmentGateway.Domain.Clusters;
using FluentAssertions;

namespace EnvironmentGateway.Domain.UnitTests.Clusters;

public class ClustersTests
{
    [Fact]
    public void CreateInitialCluster_Should_SetPropertyValues()
    {
        const string clusterName = "testName";
        const string destinationAddress = "https://tests.test";

        var initialCluster = Cluster.CreateInitialCluster(clusterName, destinationAddress);

        initialCluster.ClusterName.Value.Should().Be(clusterName);
        initialCluster.Destinations[0].Address.Value.Should().Be(destinationAddress);
    }
}