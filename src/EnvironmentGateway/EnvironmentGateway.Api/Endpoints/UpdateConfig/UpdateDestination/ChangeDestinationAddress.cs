using EnvironmentGateway.Api.GatewayConfiguration.Abstractions;
using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Application.Destinations.UpdateDestination;

namespace EnvironmentGateway.Api.Endpoints.UpdateConfig.UpdateDestination;

public class ChangeDestinationAddress : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("update-destination", 
            async (
            UpdateDestinationRequest request,
            ICommandHandler<ChangeDestinationAddressCommand> handler,
            IRuntimeConfigurator runtimeConfigurator,
            CancellationToken cancellationToken) =>
        {
            var command = new ChangeDestinationAddressCommand(
                request.ClusterId,
                request.DestinationId,
                request.Address);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            var updateResult = await runtimeConfigurator.UpdateProxyConfig();

            return Results.Ok(updateResult.IsSuccess);
        })
        .RequireAuthorization();
    }
}
