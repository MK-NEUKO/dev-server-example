using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.Shared;

namespace EnvironmentGateway.Domain.Clusters;

public sealed class Destination : Entity
{
    private Destination()
    {
    }

    private Destination(Guid id, Name destinationName, Url address)
        : base(id)
    {
        DestinationName = destinationName;
        Address = address;
    }

    public Guid ClusterId { get; private set; }
    public Name DestinationName { get; private set; }
    public Url Address { get; private set; }

    public static Destination CreateInitialDestination(string destinationName, string address)
    {
        var destination = new Destination(Guid.NewGuid(), new Name(destinationName), new Url(address));
        return destination;
    }
}