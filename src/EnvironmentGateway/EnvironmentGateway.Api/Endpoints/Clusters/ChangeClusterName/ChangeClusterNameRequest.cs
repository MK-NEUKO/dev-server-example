namespace EnvironmentGateway.Api.Endpoints.Clusters.ChangeClusterName;

public sealed record ChangeClusterNameRequest(
    Guid ClusterId,
    string ClusterName);
