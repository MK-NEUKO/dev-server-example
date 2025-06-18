using UserManager.Application.Abstractions.Messaging;

namespace UserManager.Application.Users.Login;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<string>;
