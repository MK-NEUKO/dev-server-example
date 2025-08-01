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
        List<Route> routes,
        List<Cluster> clusters)
        : base(id)
    {
        Name = name;
        IsCurrentConfig = isCurrentConfig;
        Routes = routes;
        Clusters = clusters;
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
        var routes = new List<Route>();
        var clusters = new List<Cluster>();
        for (var i = 1; i < 3; i++)
        {
            var newRoute = Route.CreateNewRoute($"route{i}", $"cluster{i}", $"/service{i}");
            var newCluster = Cluster.CreateNewCluster($"cluster{i}", i);
            routes.Add(newRoute);
            clusters.Add(newCluster);
        }

        var newConfig = new GatewayConfig(
            Guid.NewGuid(),
            new Name("New Configuration"),
            true,
            routes,
            clusters);
        
        newConfig.RaiseDomainEvent(new NewConfigCreatedDomainEvent(newConfig.Id));

        return newConfig;
    }
}
