using EnvironmentGateway.Application.Abstractions.Data;
using EnvironmentGateway.Application.Exceptions;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Clusters;
using EnvironmentGateway.Domain.Destinations;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.RouteMatches;
using EnvironmentGateway.Domain.Routes;
using EnvironmentGateway.Infrastructure.DomainEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EnvironmentGateway.Infrastructure;

public sealed class EnvironmentGatewayDbContext(
    DbContextOptions options,
    IDomainEventsDispatcher domainEventsDispatcher)
    : DbContext(options), IUnitOfWork, IEnvironmentGatewayDbContext
{
    public DbSet<GatewayConfig> GatewayConfigs { get; private set; }
    public DbSet<Route> Routes { get; private set; }
    public DbSet<Cluster> Clusters { get; private set; }
    public DbSet<RouteMatch> RouteMatches { get; private set; }
    public DbSet<Destination> Destinations { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EnvironmentGatewayDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventsAsync();

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred.", ex);
        }
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();

                entity.ClearDomainEvents();

                return domainEvents;
            }).ToList();


        await domainEventsDispatcher.DispatchAsync(domainEvents);
    }

    
}