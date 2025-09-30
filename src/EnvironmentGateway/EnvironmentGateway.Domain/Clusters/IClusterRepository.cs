namespace EnvironmentGateway.Domain.Clusters;

public interface IClusterRepository
{
    Task<Cluster?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(Cluster cluster);
}
