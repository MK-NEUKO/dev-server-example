using EnvironmentGateway.Application.Destinations.UpdateDestination;
using EnvironmentGateway.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EnvironmentGateway.Api.Endpoints.ChangeConfig;

public class ChangeDestination : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("change-destination", async (
            ChangeDestinationRequest request,
            ISender sender,
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

            return Results.Ok();
        });
    }
}