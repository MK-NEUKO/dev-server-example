using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.Clusters;

public static class ClusterErrors
{
    public static readonly Error NotFound = new(
        "Cluster.NotFound",
        "The cluster with the specified identifier was not found");
}
