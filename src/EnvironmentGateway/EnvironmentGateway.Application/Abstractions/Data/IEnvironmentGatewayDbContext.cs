using EnvironmentGateway.Domain.Clusters;
using EnvironmentGateway.Domain.Destinations;
using Microsoft.EntityFrameworkCore;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.RouteMatches;
using EnvironmentGateway.Domain.Routes;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EnvironmentGateway.Application.Abstractions.Data;

public interface IEnvironmentGatewayDbContext
{
    DbSet<GatewayConfig> GatewayConfigs { get; }

    DbSet<Route> Routes { get; }

    DbSet<Cluster> Clusters { get; }

    DbSet<RouteMatch> RouteMatches { get; }

    //DbSet<Destination> Destinations { get; }

    DatabaseFacade Database { get; }
}
