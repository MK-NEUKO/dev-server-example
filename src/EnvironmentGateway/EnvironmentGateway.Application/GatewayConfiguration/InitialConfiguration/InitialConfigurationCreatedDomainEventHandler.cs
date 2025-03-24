using EnvironmentGateway.Application.Abstractions.Email;
using EnvironmentGateway.Domain.GatewayConfiguration;
using EnvironmentGateway.Domain.GatewayConfiguration.Events;
using MediatR;

namespace EnvironmentGateway.Application.GatewayConfiguration.StartConfiguration;

internal sealed class InitialConfigurationCreatedDomainEventHandler : INotificationHandler<InitialConfigurationCreatedDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly IGatewayConfigurationRepository _gatewayConfigurationRepository;

    public InitialConfigurationCreatedDomainEventHandler(IEmailService emailService, IGatewayConfigurationRepository gatewayConfigurationRepository)
    {
        _emailService = emailService;
        _gatewayConfigurationRepository = gatewayConfigurationRepository;
    }

    public async Task Handle(InitialConfigurationCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var configuration = await _gatewayConfigurationRepository.GetByIdAsync(notification.ConfigurationId, cancellationToken);

        if (configuration is null)
        {
            return;
        }

        await _emailService.SendAsync(
            "recipient-email",
            "Initial Configuration Created",
            "Initial configuration has been created successfully.");
    }
}