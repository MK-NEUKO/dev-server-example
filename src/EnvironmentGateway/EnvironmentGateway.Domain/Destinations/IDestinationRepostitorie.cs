namespace EnvironmentGateway.Domain.Destinations;

public interface IDestinationRepository
{
    Task<Destination?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(Destination destination);
}