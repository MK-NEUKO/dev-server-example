using Microsoft.AspNetCore.Http.HttpResults;
using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGatewayApi.Endpoints.ReverseProxy;

public class Get(IProxyConfigProvider proxyConfigProvider) : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/reverseProxy/get", (CancellationToken cancelationToken) =>
        {
            var config = proxyConfigProvider.GetConfig();
            return Results.Ok(config);
        });
    }
}