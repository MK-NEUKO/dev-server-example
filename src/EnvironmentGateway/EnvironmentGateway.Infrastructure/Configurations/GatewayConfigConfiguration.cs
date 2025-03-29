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
            .IsRequired();

        builder.Property(gatewayConfig => gatewayConfig.IsCurrentConfig)
            .IsRequired();

        builder.HasMany(gatewayConfig => gatewayConfig.Routes)
            .WithOne()
            .HasForeignKey(route => route.GatewayConfigId)
            .IsRequired();

        builder.HasMany(gatewayConfig => gatewayConfig.Clusters)
            .WithOne()
            .HasForeignKey(cluster => cluster.GatewayConfigId)
            .IsRequired();
    }
}