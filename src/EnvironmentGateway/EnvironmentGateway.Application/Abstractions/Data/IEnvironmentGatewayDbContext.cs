using EnvironmentGateway.Domain.Clusters;
using EnvironmentGateway.Domain.Clusters.Destinations;
using Microsoft.EntityFrameworkCore;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.Routes;
using EnvironmentGateway.Domain.Routes.Matches;
using EnvironmentGateway.Domain.Routes.Transforms;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EnvironmentGateway.Application.Abstractions.Data;

public interface IEnvironmentGatewayDbContext
{
    DbSet<GatewayConfig> GatewayConfigs { get; }
    DbSet<Route> Routes { get; }
    DbSet<Cluster> Clusters { get; }
    DbSet<RouteMatch> RouteMatches { get; }
    DatabaseFacade Database { get; }
    DbSet<RouteTransforms> RouteTransforms { get; }
    DbSet<Destination> Destinations { get; }
}
