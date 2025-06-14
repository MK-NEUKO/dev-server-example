using EnvironmentGateway.Application.Abstractions.Email;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.GatewayConfigs.Events;

namespace EnvironmentGateway.Application.GatewayConfigs.CreateNewConfig;

internal sealed class NewConfigCreatedDomainEventHandler(
    IEmailService emailService,
    IGatewayConfigRepository gatewayConfigRepository)
    : IDomainEventHandler<NewConfigCreatedDomainEvent>
{
    public async Task Handle(NewConfigCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var configuration = await gatewayConfigRepository.GetByIdAsync(domainEvent.ConfigurationId, cancellationToken);

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

internal sealed class NewConfigCreatedDomainEventHandler1(
    IEmailService emailService,
    IGatewayConfigRepository gatewayConfigRepository)
    : IDomainEventHandler<NewConfigCreatedDomainEvent>
{
    public async Task Handle(NewConfigCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var configuration = await gatewayConfigRepository.GetByIdAsync(domainEvent.ConfigurationId, cancellationToken);

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