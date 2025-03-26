using EnvironmentGateway.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentGateway.Infrastructure;

public sealed class EnvironmentGatewayDbContext : DbContext, IUnitOfWork
{
    public EnvironmentGatewayDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EnvironmentGatewayDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}