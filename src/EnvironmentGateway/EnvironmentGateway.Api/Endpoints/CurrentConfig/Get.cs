using EnvironmentGateway.Application.GatewayConfigs.GetCurrentConfig;
using EnvironmentGateway.Domain.Abstractions;
using MediatR;

namespace EnvironmentGateway.Api.Endpoints.CurrentConfig;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("current-config", async (
            ISender sender,
            CancellationToken cancelationToken) =>
        {
            var query = new GetCurrentConfigQuery();

            var result = await sender.Send(query, cancelationToken);
            
            // TODO: remove, only for test
            await Task.Delay(2000);

            return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound();
        });
    }
}