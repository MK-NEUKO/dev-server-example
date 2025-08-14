namespace EnvironmentGateway.Domain.Clusters.Destinations;

public interface IDestinationRepository
{
    Task<Destination?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Destination?> GetByIdAsync(Guid clusterId, Guid destinationId, CancellationToken cancellationToken = default);

    void Add(Destination destination);
}
