using EnvironmentGateway.Domain.Routes.Match;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnvironmentGateway.Infrastructure.Configurations;

public class RouteMatchConfiguration : IEntityTypeConfiguration<RouteMatch>
{
    public void Configure(EntityTypeBuilder<RouteMatch> builder)
    {
        builder.ToTable("route_matches");

        builder.HasKey(routeMatch => routeMatch.Id);

        builder.Property(routeMatch => routeMatch.RouteId)
            .IsRequired();

        builder.ComplexProperty(routeMatch => routeMatch.Path)
            .Property(path => path.Value)
            .IsRequired();
    }
}