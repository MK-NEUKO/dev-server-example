using EnvironmentGateway.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentGateway.Infrastructure.Repositories;

internal abstract class Repository<T>
    where T : Entity
{
    protected readonly EnvironmentGatewayDbContext DbContext;

    protected Repository(EnvironmentGatewayDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<T> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<T>()
            .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public void Add(T entity)
    {
        DbContext.Add(entity);
    }
}