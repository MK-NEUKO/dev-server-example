using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnvironmentGateway.Infrastructure.Configurations;

public class GatewayConfigConfiguration : IEntityTypeConfiguration<GatewayConfig>
{
    public void Configure(EntityTypeBuilder<GatewayConfig> builder)
    {
        builder.ToTable("gateway_configs");

        builder.HasKey(gatewayConfig => gatewayConfig.Id);

        builder.ComplexProperty(gatewayConfig => gatewayConfig.Name)
            .Property(name => name.Value)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(gatewayConfig => gatewayConfig.IsCurrentConfig)
            .IsRequired();

        builder.HasMany(gatewayConfig => gatewayConfig.Routes)
            .WithOne(route => route.GatewayConfig)
            .HasForeignKey(route => route.GatewayConfigId)
            .IsRequired();

        builder.HasMany(gatewayConfig => gatewayConfig.Clusters)
            .WithOne(cluster => cluster.GatewayConfig)
            .HasForeignKey(cluster => cluster.GatewayConfigId)
            .IsRequired();
    }
}