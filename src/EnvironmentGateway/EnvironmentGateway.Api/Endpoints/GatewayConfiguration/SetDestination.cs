using EnvironmentGateway.Api.GatewayConfiguration.Abstractions;

namespace EnvironmentGateway.Api.Endpoints.GatewayConfiguration;

public class SetDestination : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/set-address/{address}", (string address, CancellationToken cancellationToken) =>
        {
            var value = $"https://{address}";

            var configurator = app.ServiceProvider.GetRequiredService<IRuntimeConfigurator>();

            configurator.ChangeDestinationAddress(value);

            return Results.Ok($"Property: Address of destination1 has changed; Statuscode: {StatusCodes.Status201Created}");
        });
    }
}