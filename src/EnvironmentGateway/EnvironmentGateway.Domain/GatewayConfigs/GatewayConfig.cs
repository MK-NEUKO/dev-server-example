using System.Linq.Expressions;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Clusters;
using EnvironmentGateway.Domain.GatewayConfigs.Events;
using EnvironmentGateway.Domain.Routes;
using EnvironmentGateway.Domain.Shared;

namespace EnvironmentGateway.Domain.GatewayConfigs;

public sealed class GatewayConfig : Entity
{
    private GatewayConfig(Guid id, Name name)
        : base(id)
    {
        Name = name;
    }

    private GatewayConfig()
    {
    }

    public Name? Name { get; private set; }

    public bool IsCurrentConfig { get; private set; }

    public List<Route> Routes { get; private set; } = [];

    public List<Cluster> Clusters { get; private set; } = [];

    public static GatewayConfig Create()
    {
        var route = Route.Create(
            ConfigDefaultParams.RouteName, 
            ConfigDefaultParams.ClusterName, 
            ConfigDefaultParams.RouteMatchPath);
        var cluster = Cluster.Create(
            ConfigDefaultParams.ClusterName, 
            ConfigDefaultParams.DestinationName,
            ConfigDefaultParams.DestinationAddress);
        var gatewayConfig = new GatewayConfig(
            Guid.NewGuid(),
            new Name(ConfigDefaultParams.ConfigName));
        
        gatewayConfig.AddCluster(cluster);
        gatewayConfig.AddRoute(route);
        gatewayConfig.IsCurrentConfig = true;
        
        gatewayConfig.RaiseDomainEvent(new NewConfigCreatedDomainEvent(gatewayConfig.Id));
        
        return gatewayConfig;
    }

    public void AddCluster(Cluster cluster)
    {
        ArgumentNullException.ThrowIfNull(cluster);
        
        Clusters.Add(cluster);
    }
    
    public void AddRoute(Route route)
    {
        ArgumentNullException.ThrowIfNull(route);

        Routes.Add(route);
    }
}
