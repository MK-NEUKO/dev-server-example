using System.ComponentModel.DataAnnotations;
using EnvironmentGateway.Domain.Clusters;
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
            .Property(name => name.Value)
            .HasMaxLength(100)
            .IsRequired();

        builder.ComplexProperty(destination => destination.Address)
            .Property(address => address.Value)
            .IsRequired();
    }
}