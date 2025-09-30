using EnvironmentGateway.Api.GatewayConfiguration.Abstractions;
using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Application.Clusters.ChangeClusterName;
using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Api.Endpoints.Clusters.ChangeClusterName;

public class ChangeClusterName : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("change-cluster-name",
                async (
                    ChangeClusterNameRequest request,
                    ICommandHandler<ChangeClusterNameCommand> commandHandler,
                    IRuntimeConfigurator runtimeConfigurator,
                    CancellationToken cancellationToken) =>
                {
                    var command = new ChangeClusterNameCommand(
                        request.ClusterId,
                        request.ClusterName);
                    
                    var result = await commandHandler.Handle(command, cancellationToken);

                    if (result.IsFailure)
                    {
                        return Results.BadRequest(result.Error);
                    }
                    
                    var updateResult = await runtimeConfigurator.UpdateProxyConfig();

                    return Results.Ok(updateResult.IsSuccess);
                }
            );
    }
}
