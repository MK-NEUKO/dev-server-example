using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.Shared;

namespace EnvironmentGateway.Domain.Clusters;

public sealed class Destination : Entity
{
    private Destination(Guid id, Name destinationName, Url address)
        : base(id)
    {
        DestinationName = destinationName;
        Address = address;
    }

    private Destination()
    {
    }

    public Guid ClusterId { get; init; }
    public Cluster Cluster { get; init; } = null!;
    public Name DestinationName { get; init; } = new Name("null");
    public Url Address { get; init; } = new Url("null");

    public static Destination CreateInitialDestination(string destinationName, string address)
    {
        var destination = new Destination(Guid.NewGuid(), new Name(destinationName), new Url(address));
        return destination;
    }
}