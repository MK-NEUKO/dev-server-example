using EnvironmentGateway.Domain.Clusters;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnvironmentGateway.Infrastructure.Configurations;

public class ClusterConfiguration : IEntityTypeConfiguration<Cluster>
{
    public void Configure(EntityTypeBuilder<Cluster> builder)
    {
        builder.ToTable("clusters");

        builder.HasKey(cluster => cluster.Id);

        builder.Property(cluster => cluster.GatewayConfigId)
            .IsRequired();

        builder.ComplexProperty(cluster => cluster.ClusterName)
            .IsRequired();

        builder.HasMany(cluster => cluster.Destinations)
            .WithOne(destination => destination.Cluster)
            .HasForeignKey(destination => destination.ClusterId)
            .IsRequired();
    }
}