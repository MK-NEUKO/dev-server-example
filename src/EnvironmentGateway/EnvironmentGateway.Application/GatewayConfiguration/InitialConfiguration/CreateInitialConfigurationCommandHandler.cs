using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfiguration;

namespace EnvironmentGateway.Application.GatewayConfiguration.StartConfiguration;

internal sealed class CreateInitialConfigurationCommandHandler : ICommandHandler<CreateInitialConfigurationCommand, Guid>
{
    private readonly IGatewayConfigurationRepository _gatewayConfigurationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInitialConfigurationCommandHandler(
        IGatewayConfigurationRepository gatewayConfigurationRepository,
        IUnitOfWork unitOfWork)
    {
        _gatewayConfigurationRepository = gatewayConfigurationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateInitialConfigurationCommand request, CancellationToken cancellationToken)
    {
        var initialConfigurationName = new Name("InitialConfiguration");
        var configuration =
            Domain.GatewayConfiguration.GatewayConfiguration.CreateInitialConfiguration(initialConfigurationName);

        try
        {
            _gatewayConfigurationRepository.Add(configuration);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(configuration.Id);
        }
        catch (Exception)
        {
            return Result.Failure<Guid>(GatewayConfigurationErrors.CreateInitialConfigurationFailed);
        }
    }
}