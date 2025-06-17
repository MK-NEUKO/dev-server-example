using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Clusters;
using EnvironmentGateway.Domain.Shared;

namespace EnvironmentGateway.Domain.Destinations;

public sealed class Destination : Entity
{
    private Destination(Guid id, Name destinationName, Address address)
        : base(id)
    {
        DestinationName = destinationName;
        Address = address;
    }

    private Destination()
    {
    }

    public Guid ClusterId { get; init; }
    public Name DestinationName { get; init; } = new Name("null");
    public Address Address { get; private set; } = new Address("null");

    public static Destination CreateNewDestination(string destinationName, string address)
    {
        var destination = new Destination(Guid.NewGuid(), new Name(destinationName), new Address(address));
        return destination;
    }

    public void Update(string address)
    {
        Address = new Address(address);
    }
}
