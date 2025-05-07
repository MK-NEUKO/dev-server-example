using EnvironmentGateway.Application.Abstractions.Email;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.GatewayConfigs.Events;
using MediatR;

namespace EnvironmentGateway.Application.GatewayConfigs.CreateNewConfig;

internal sealed class NewConfigCreatedDomainEventHandler(
    IEmailService emailService,
    IGatewayConfigRepository gatewayConfigRepository)
    : INotificationHandler<NewConfigCreatedDomainEvent>
{
    public async Task Handle(NewConfigCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var configuration = await gatewayConfigRepository.GetByIdAsync(notification.ConfigurationId, cancellationToken);

        if (configuration is null)
        {
            return;
        }

        await emailService.SendAsync(
            "recipient-email",
            "New Configuration Created",
            "New configuration has been created successfully.");
    }
}