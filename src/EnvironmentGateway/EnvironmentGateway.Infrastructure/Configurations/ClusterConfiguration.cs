using EnvironmentGateway.Domain.Clusters;
using EnvironmentGateway.Domain.Destinations;
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
            .Property(name => name.Value)
            .HasMaxLength(100)
            .IsRequired();

        //builder.HasMany(cluster => cluster.Destinations)
        //    .WithOne(destination => destination.Cluster)
        //    .HasForeignKey(destination => destination.ClusterId)
        //    .IsRequired();

        builder.OwnsMany(
            cluster => cluster.Destinations,
            destination =>
            {
                destination.WithOwner().HasForeignKey(dest => dest.ClusterId);
                destination.HasKey(dest => dest.Id);
                destination.Property(dest => dest.DestinationName)
                    .HasConversion(
                        name => name.Value,
                        name => new Name(name));
                    
                destination.Property(dest => dest.Address)
                    .HasConversion(
                        address => address.Value,
                        address => new Address(address));

            });
    }
}
