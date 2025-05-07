using EnvironmentGateway.Api.GatewayConfiguration.Abstractions;
using EnvironmentGateway.Application.Destinations.UpdateDestination;
using EnvironmentGateway.Domain.Abstractions;
using MediatR;

namespace EnvironmentGateway.Api.Endpoints.UpdateConfig;

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

            Result result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            await runtimeConfigurator.UpdateYarpProxy();

            return Results.Ok();
        });
    }
}