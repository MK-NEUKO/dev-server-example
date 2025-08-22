using EnvironmentGateway.Domain.Routes.Transforms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnvironmentGateway.Infrastructure.Configurations;

public class RouteTransformsConfiguration : IEntityTypeConfiguration<RouteTransforms>
{
    public void Configure(EntityTypeBuilder<RouteTransforms> builder)
    {
        builder.ToTable("route_transforms");

        builder.HasKey(routeTransforms => routeTransforms.Id);

        builder.Property(routeTransforms => routeTransforms.RouteId)
            .IsRequired();

        builder.OwnsMany(routeTransform => routeTransform.TransformsItems, transform =>
        {
            transform.ToTable("route_transform_items");
            transform.WithOwner()
                .HasForeignKey("RouteTransformsId");

            transform.Property(tr => tr.Key)
                .HasColumnName("key");
            
            transform.Property(tr => tr.Value)
                .HasColumnName("value");
        });
    }
}
