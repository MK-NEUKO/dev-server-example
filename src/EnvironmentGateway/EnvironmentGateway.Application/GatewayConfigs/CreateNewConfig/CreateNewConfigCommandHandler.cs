using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfigs;

namespace EnvironmentGateway.Application.GatewayConfigs.CreateNewConfig;

internal sealed class CreateNewConfigCommandHandler(
    IGatewayConfigRepository gatewayConfigRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateNewConfigCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateNewConfigCommand request, CancellationToken cancellationToken)
    {
        var currentConfigGuid = await gatewayConfigRepository.GetCurrentConfigId(cancellationToken);

        if (currentConfigGuid is not null && currentConfigGuid.Value != Guid.Empty)
        {
            return currentConfigGuid.Value;
        }
        
        try
        {
            var newConfiguration = GatewayConfig.CreateNewConfig();

            gatewayConfigRepository.Add(newConfiguration);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return newConfiguration.Id;
        }
        catch (Exception)
        {
            return Result.Failure<Guid>(GatewayConfigErrors.CreateNewConfigFailed);
        }
    }
}