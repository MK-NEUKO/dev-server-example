using UserManager.Application.Abstractions.Messaging;

namespace UserManager.Application.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
