using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Clusters;
using EnvironmentGateway.Domain.GatewayConfigs.Events;
using EnvironmentGateway.Domain.Routes;
using EnvironmentGateway.Domain.Shared;

namespace EnvironmentGateway.Domain.GatewayConfigs;

public sealed class GatewayConfig : Entity
{
    private GatewayConfig(
        Guid id,
        Name name,
        bool isCurrentConfig,
        Route route,
        Cluster cluster)
        : base(id)
    {
        Name = name;
        IsCurrentConfig = isCurrentConfig;
        Routes.Add(route);
        Clusters.Add(cluster);
    }

    private GatewayConfig()
    {
    }

    public Name? Name { get; private set; }

    public bool IsCurrentConfig { get; private set; }

    public List<Route> Routes { get; private set; } = [];

    public List<Cluster> Clusters { get; private set; } = [];

    public static GatewayConfig CreateNewConfig()
    {
        var newRoute = Route.CreateNewRoute("route1", "cluster1", "{**catch-all}");
        var newCluster = Cluster.CreateNewCluster("cluster1", "https://neuko-know-how.com");

        var newConfig = new GatewayConfig(
            Guid.NewGuid(),
            new Name("New Configuration"),
            true,
            newRoute,
            newCluster);
        
        newConfig.RaiseDomainEvent(new NewConfigCreatedDomainEvent(newConfig.Id));

        return newConfig;
    }
}