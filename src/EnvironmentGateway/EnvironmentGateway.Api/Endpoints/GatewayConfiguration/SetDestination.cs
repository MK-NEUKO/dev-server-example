using EnvironmentGatewayApi.GatewayConfiguration.Abstractions;

namespace EnvironmentGatewayApi.Endpoints.GatewayConfiguration;

public class SetDestination : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/set-address/{address}", (string address, CancellationToken cancellationToken) =>
        {
            var value = $"https://{address}";

            var configurator = app.ServiceProvider.GetRequiredService<IRuntimeConfigurator>();

            configurator.ChangeDestinationAddress(value);

            return Results.Ok();
        });
    }
}