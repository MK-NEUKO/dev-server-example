using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Application.GatewayConfigs.GetCurrentConfig;

namespace EnvironmentGateway.Api.Endpoints.CurrentConfig;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("current-config", async (
                IQueryHandler<GetCurrentConfigQuery, CurrentConfigResponse> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetCurrentConfigQuery();

                var result = await handler.Handle(query, cancellationToken);

                return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound();
            })
            .RequireAuthorization();
    }
}
