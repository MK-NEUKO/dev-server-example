using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfig.Events;

namespace EnvironmentGateway.Domain.GatewayConfig;

public sealed class GatewayConfig : Entity
{
    private GatewayConfig(Guid id, Name name)
        : base(id)
    {
        Name = name;
    }

    public Name Name { get; private set; }

    public List<Route.Route> Routes { get; private set; } = new();

    public List<Cluster.Cluster> Clusters { get; private set; } = new();

    public static GatewayConfig CreateInitialConfiguration(string name)
    {
        var initialConfiguration = new GatewayConfig(Guid.NewGuid(), new Name(name));
        
        initialConfiguration.RaiseDomainEvent(new InitialConfigCreatedDomainEvent(initialConfiguration.Id));

        return initialConfiguration;
    }
}