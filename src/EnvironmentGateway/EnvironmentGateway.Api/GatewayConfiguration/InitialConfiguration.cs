using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGateway.Api.GatewayConfiguration;

internal sealed record InitialConfiguration(
    RouteConfig[] Routes,
    ClusterConfig[] Clusters);