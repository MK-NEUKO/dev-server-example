using UserManager.Application.Abstractions.Messaging;

namespace UserManager.Application.Users.GetByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserResponse>;
