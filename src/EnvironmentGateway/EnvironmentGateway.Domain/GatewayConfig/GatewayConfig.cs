using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfig.Events;
using EnvironmentGateway.Domain.Route;

namespace EnvironmentGateway.Domain.GatewayConfig;

public sealed class GatewayConfig : Entity
{
    private GatewayConfig(
        Guid id,
        Name name,
        bool isCurrentConfig,
        Route.Route route,
        Cluster.Cluster cluster)
        : base(id)
    {
        Name = name;
        IsCurrentConfig = isCurrentConfig;
        Route = route;
        Cluster = cluster;
    }

    public Name Name { get; private set; }

    public bool IsCurrentConfig { get; private set; }
    public Route.Route Route { get; }
    public Cluster.Cluster Cluster { get; }

    public List<Route.Route> Routes { get; private set; } = new();

    public List<Cluster.Cluster> Clusters { get; private set; } = new();

    public static GatewayConfig CreateInitialConfiguration(string configName)
    {
        var initialRoute = Domain.Route.Route.CreateInitialRoute("route1", "cluster1", "{**catch-all}");
        var initialCluster = Domain.Cluster.Cluster.CreateInitialCluster("cluster1", "https://github.com");

        var initialConfiguration = new GatewayConfig(
            Guid.NewGuid(),
            new Name(configName),
            true,
            initialRoute,
            initialCluster);
        
        initialConfiguration.RaiseDomainEvent(new InitialConfigCreatedDomainEvent(initialConfiguration.Id));

        return initialConfiguration;
    }
}