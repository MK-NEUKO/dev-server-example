using EnvironmentGateway.Domain.RouteMatches;
using EnvironmentGateway.Domain.Routes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnvironmentGateway.Infrastructure.Configurations;

public class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        builder.ToTable("Routs");

        builder.HasKey(route => route.Id);

        builder.Property(route => route.GatewayConfigId)
            .IsRequired();

        builder.ComplexProperty(route => route.RouteName)
            .IsRequired();

        builder.ComplexProperty(route => route.ClusterName)
            .IsRequired();

        builder.HasOne(route => route.Match)
            .WithOne()
            .HasForeignKey<RouteMatch>(match => match.RouteId)
            .IsRequired();
    }
}