using EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;
using EnvironmentGateway.Domain.Abstractions;
using MediatR;

namespace EnvironmentGateway.Api.Endpoints.CurrentConfig;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("current-config", async (
            ISender ISender,
            CancellationToken cancelationToken) =>
        {
            var query = new GetStartConfigQuery(true);

            Result<StartConfigResponse> result = await ISender.Send(query, cancelationToken);

            return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound();
        });
    }
}