using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGateway.Api.Endpoints.GatewayConfiguration;

public class Get(IProxyConfigProvider configProvider) : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/get-configuration", (CancellationToken CancellationToken) =>
        {
            var config = configProvider.GetConfig();

            return Results.Json(config);
        });
    }
}