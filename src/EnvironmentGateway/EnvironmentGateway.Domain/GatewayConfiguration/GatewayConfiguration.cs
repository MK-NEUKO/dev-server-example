using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfiguration.Events;

namespace EnvironmentGateway.Domain.GatewayConfiguration;

public sealed class GatewayConfiguration : Entity
{
    private GatewayConfiguration(Guid id, Name name)
        : base(id)
    {
        Name = name;
    }

    public Name Name { get; private set; }

    public List<Route> Routes { get; private set; } = new();

    public List<Cluster> Clusters { get; private set; } = new();

    public static GatewayConfiguration CreateInitialConfiguration(Name name)
    {
        var initialConfiguration = new GatewayConfiguration(Guid.NewGuid(), name);
        
        initialConfiguration.RaiseDomainEvent(new InitialConfigurationCreatedDomainEvent(initialConfiguration.Id));

        return initialConfiguration;
    }
}