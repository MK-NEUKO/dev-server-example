using EnvironmentGateway.Api.GatewayConfiguration.Abstractions;
using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Application.Destinations.ChangeDestinationName;

namespace EnvironmentGateway.Api.Endpoints.Destinations.ChangeDestinationName;

public class ChangeDestinationName : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("change-destination-name",
                async (
                    ChangeDestinationNameRequest request,
                    ICommandHandler<ChangeDestinationNameCommand> commandHandler,
                    IRuntimeConfigurator runtimeConfigurator,
                    CancellationToken cancellationToken) =>
                {
                    var command = new ChangeDestinationNameCommand(
                        request.ClusterId,
                        request.DestinationId,
                        request.DestinationName);
                    
                    var result = await commandHandler.Handle(command, cancellationToken);
                    
                    if (result.IsFailure)
                    {
                        return Results.BadRequest(result.Error);
                    }
                    
                    var updateResult = await runtimeConfigurator.UpdateProxyConfig();
                    
                    return Results.Ok(updateResult.IsSuccess);
                }

            )
            .RequireAuthorization();
    }
}
