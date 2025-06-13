using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Application.GatewayConfigs.GetCurrentConfig;

namespace EnvironmentGateway.Api.Endpoints.CurrentConfig;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("current-config", async (
            IQueryHandler<GetCurrentConfigQuery, CurrentConfigResponse> handler,
            CancellationToken cancelationToken) =>
        {
            var query = new GetCurrentConfigQuery();

            var result = await handler.Handle(query, cancelationToken);

            return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound();
        });
    }
}