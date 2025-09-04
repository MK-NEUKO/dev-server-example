using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Shared;

namespace EnvironmentGateway.Domain.Clusters.Destinations;

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
    public Name DestinationName { get; private set; } = new Name("null");
    public Address Address { get; private set; } = new Address("null");

    public static Destination Create(string destinationName, string address)
    {
        ArgumentNullException.ThrowIfNull(destinationName);
        ArgumentNullException.ThrowIfNull(address);
        
        var destination = new Destination(Guid.NewGuid(), new Name(destinationName), new Address(address));
        return destination;
    }

    public void ChangeAddress(string address)
    {
        ArgumentNullException.ThrowIfNull(address);
        
        Address = new Address(address);
    }

    public void ChangeName(string destinationName)
    {
        ArgumentNullException.ThrowIfNull(destinationName);
        
        DestinationName = new Name(destinationName);
    }
}
