using EnvironmentGateway.Domain.GatewayConfig;
using EnvironmentGateway.Domain.GatewayConfig.Cluster;
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

        builder.Property(cluster => cluster.ClusterName)
            .HasConversion(clusterName => clusterName.Value, value => new Name(value))
            .HasMaxLength(200)
            .IsRequired();

        builder.HasMany(cluster => cluster.Destinations)
            .WithOne()
            .HasForeignKey(destination => destination.ClusterId)
            .IsRequired();
    }
}