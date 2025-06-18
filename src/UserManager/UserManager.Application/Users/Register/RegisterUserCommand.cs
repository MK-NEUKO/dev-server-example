using UserManager.Application.Abstractions.Messaging;

namespace UserManager.Application.Users.Register;

public sealed record RegisterUserCommand(string Email, string FirstName, string LastName, string Password)
    : ICommand<Guid>;
