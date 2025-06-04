using EnvironmentGateway.Api.GatewayConfiguration.Abstractions;
using EnvironmentGateway.Application.Destinations.UpdateDestination;
using MediatR;

namespace EnvironmentGateway.Api.Endpoints.UpdateConfig.UpdateDestination;

public class UpdateDestination : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("update-destination", async (
            UpdateDestinationRequest request,
            ISender sender,
            IRuntimeConfigurator runtimeConfigurator,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateDestinationCommand(
                request.Id,
                request.Address);

            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            var updateResult = await runtimeConfigurator.UpdateProxyConfig();

            return Results.Ok(updateResult.IsSuccess);
        });
    }
}