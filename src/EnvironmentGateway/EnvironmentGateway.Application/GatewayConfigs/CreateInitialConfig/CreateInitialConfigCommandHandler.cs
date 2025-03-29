using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfigs;

namespace EnvironmentGateway.Application.GatewayConfigs.CreateInitialConfig;

internal sealed class CreateInitialConfigCommandHandler : ICommandHandler<CreateInitialConfigCommand, Guid>
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

    public async Task<Result<Guid>> Handle(CreateInitialConfigCommand request, CancellationToken cancellationToken)
    {
        var configuration = GatewayConfig.CreateInitialConfiguration(request.Name);

        try
        {
            _gatewayConfigRepository.Add(configuration);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(configuration.Id);
        }
        catch (Exception)
        {
            return Result.Failure<Guid>(GatewayConfigErrors.CreateInitialConfigFailed);
        }
    }
}