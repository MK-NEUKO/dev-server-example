using EnvironmentGateway.Application.Abstractions.Email;
using EnvironmentGateway.Domain.GatewayConfig;
using EnvironmentGateway.Domain.GatewayConfig.Events;
using MediatR;

namespace EnvironmentGateway.Application.GatewayConfig.InitialConfig;

internal sealed class InitialConfigCreatedDomainEventHandler : INotificationHandler<InitialConfigCreatedDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly IGatewayConfigRepository _gatewayConfigRepository;

    public InitialConfigCreatedDomainEventHandler(IEmailService emailService, IGatewayConfigRepository gatewayConfigRepository)
    {
        _emailService = emailService;
        _gatewayConfigRepository = gatewayConfigRepository;
    }

    public async Task Handle(InitialConfigCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var configuration = await _gatewayConfigRepository.GetByIdAsync(notification.ConfigurationId, cancellationToken);

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