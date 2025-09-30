using EnvironmentGateway.Application.Abstractions.Messaging;

namespace EnvironmentGateway.Application.Clusters.ChangeClusterName;

public sealed record ChangeClusterNameCommand(
    Guid ClusterId,
    string ClusterName)
    : ICommand;
