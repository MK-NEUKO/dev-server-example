using UserManager.Domain.Users;

namespace UserManager.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}
