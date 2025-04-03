using EnvironmentGateway.Domain.RouteMatches;
using EnvironmentGateway.Domain.Routes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnvironmentGateway.Infrastructure.Configurations;

public class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        builder.ToTable("routes");

        builder.HasKey(route => route.Id);

        builder.Property(route => route.GatewayConfigId)
            .IsRequired();

        builder.ComplexProperty(route => route.RouteName)
            .Property(name => name.Value)
            .HasMaxLength(100)
            .IsRequired();

        builder.ComplexProperty(route => route.ClusterName)
            .Property(clusterName => clusterName.Value)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(route => route.Match)
            .WithOne(match => match.Route)
            .HasForeignKey<RouteMatch>(match => match.RouteId)
            .IsRequired();
    }
}