using EnvironmentGateway.Application.Abstractions.Email;

namespace EnvironmentGateway.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
    public Task SendAsync(string recipient, string subject, string body)
    {
        return Task.CompletedTask;
    }
}