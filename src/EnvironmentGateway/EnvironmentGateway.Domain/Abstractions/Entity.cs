using System.Security.Cryptography.X509Certificates;

namespace EnvironmentGateway.Domain.Abstractions;

public abstract class Entity
{
    protected Entity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; init; }
}