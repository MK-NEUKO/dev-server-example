using EnvironmentGateway.Domain.Clusters;
using Microsoft.EntityFrameworkCore;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.Routes;

namespace EnvironmentGateway.Application.Abstractions.Data;

public interface IEnvironmentGatewayDbContext
{
    DbSet<GatewayConfig> GatewayConfigs { get; }

    DbSet<Route> Routes { get; }

    DbSet<Cluster> Clusters { get; }

    DbSet<RouteMatch> RouteMatches { get; }

    DbSet<Destination> Destinations { get; }
}