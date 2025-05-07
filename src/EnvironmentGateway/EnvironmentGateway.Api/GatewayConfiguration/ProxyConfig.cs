using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGateway.Api.GatewayConfiguration;

public record ProxyConfig(
    RouteConfig[] Routes,
    ClusterConfig[] Clusters);