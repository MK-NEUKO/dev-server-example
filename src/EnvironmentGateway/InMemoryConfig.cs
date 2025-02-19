using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGateway;

public class InMemoryConfig
{
    public IReadOnlyList<RouteConfig> Routes { get; set; } = new List<RouteConfig>();
    public IReadOnlyList<ClusterConfig> Clusters { get; set; } = new List<ClusterConfig>();
}