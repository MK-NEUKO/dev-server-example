using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfigs;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace EnvironmentGateway.Application.GatewayConfigs.CreateInitialConfig;

internal sealed class CreateInitialConfigCommandHandler(
    IGatewayConfigRepository gatewayConfigRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateInitialConfigCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateInitialConfigCommand request, CancellationToken cancellationToken)
    {
        var currentConfigGuid = await gatewayConfigRepository.GetCurrentConfigId(cancellationToken);

        if (currentConfigGuid is not null && currentConfigGuid.Value != Guid.Empty)
        {
            return currentConfigGuid.Value;
        }

        try
        {
            var initialConfiguration = GatewayConfig.CreateInitialConfiguration();

            gatewayConfigRepository.Add(initialConfiguration);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return initialConfiguration.Id;
        }
        catch (Exception)
        {
            return Result.Failure<Guid>(GatewayConfigErrors.CreateInitialConfigFailed);
        }
    }
}