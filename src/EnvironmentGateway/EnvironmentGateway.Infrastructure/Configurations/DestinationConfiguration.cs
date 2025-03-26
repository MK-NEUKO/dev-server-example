using System.ComponentModel.DataAnnotations;
using EnvironmentGateway.Domain.GatewayConfig.Cluster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnvironmentGateway.Infrastructure.Configurations;

public class DestinationConfiguration : IEntityTypeConfiguration<Destination>
{
    public void Configure(EntityTypeBuilder<Destination> builder)
    {
        builder.ToTable("destinations");

        builder.HasKey(destination => destination.Id);

        builder.Property(destination => destination.ClusterId)
            .IsRequired();

        builder.ComplexProperty(destination => destination.DestinationName)
            .IsRequired();

        builder.ComplexProperty(destination => destination.Address)
            .IsRequired();
    }
}