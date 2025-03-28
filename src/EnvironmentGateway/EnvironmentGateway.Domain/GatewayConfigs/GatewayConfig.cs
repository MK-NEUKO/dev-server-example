using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Clusters;
using EnvironmentGateway.Domain.GatewayConfigs.Events;
using EnvironmentGateway.Domain.Routes;
using EnvironmentGateway.Domain.Shared;

namespace EnvironmentGateway.Domain.GatewayConfigs;

public sealed class GatewayConfig : Entity
{
    private GatewayConfig()
    {
    }

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

    public Name Name { get; private set; }

    public bool IsCurrentConfig { get; private set; }
    
    public Cluster Cluster { get; }

    public List<Route> Routes { get; private set; } = new();

    public List<Cluster> Clusters { get; private set; } = new();

    public static GatewayConfig CreateInitialConfiguration(string configName)
    {
        var initialRoute = Route.CreateInitialRoute("route1", "cluster1", "{**catch-all}");
        var initialCluster = Cluster.CreateInitialCluster("cluster1", "https://github.com");

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