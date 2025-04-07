using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfigs;

namespace EnvironmentGateway.Application.GatewayConfigs.CreateInitialConfig;

internal sealed class CreateInitialConfigCommandHandler : ICommandHandler<CreateInitialConfigCommand>
{
    private readonly IGatewayConfigRepository _gatewayConfigRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInitialConfigCommandHandler(
        IGatewayConfigRepository gatewayConfigRepository,
        IUnitOfWork unitOfWork)
    {
        _gatewayConfigRepository = gatewayConfigRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateInitialConfigCommand request, CancellationToken cancellationToken)
    {
        if (await _gatewayConfigRepository.IsCurrentConfigExists(cancellationToken))
        {
            return Result.Success();
        }

        var configuration = GatewayConfig.CreateInitialConfiguration(request.Name);

        try
        {
            _gatewayConfigRepository.Add(configuration);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(GatewayConfigErrors.CreateInitialConfigFailed);
        }
    }
}