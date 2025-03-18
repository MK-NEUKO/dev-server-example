namespace EnvironmentGatewayApi.Endpoints.GatewayConfiguration;

public class SetDestination : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/set-address/{address}", (string address, CancellationToken cancellationToken) =>
        {
            var value = address;

            return Results.Json(address);
        });
    }
}