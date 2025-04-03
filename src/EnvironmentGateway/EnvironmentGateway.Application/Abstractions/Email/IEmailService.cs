namespace EnvironmentGateway.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsync(string recipient, string subject, string body);
}