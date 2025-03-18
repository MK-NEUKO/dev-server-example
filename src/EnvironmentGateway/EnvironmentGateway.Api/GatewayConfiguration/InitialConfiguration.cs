using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGatewayApi.GatewayConfiguration;

internal record InitialConfiguration
{
    public RouteConfig[] Routes { get; init; }
    public ClusterConfig[] Clusters { get; init; }
}