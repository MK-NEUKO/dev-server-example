using Microsoft.EntityFrameworkCore;
using SharedKernel;
using UserManager.Application.Abstractions.Authentication;
using UserManager.Application.Abstractions.Data;
using UserManager.Application.Abstractions.Messaging;
using UserManager.Domain.Users;

namespace UserManager.Application.Users.Login;

internal sealed class LoginUserCommandHandler(
    IApplicationDbContext context
    /*IPasswordHasher passwordHasher*/) 
    : ICommandHandler<LoginUserCommand, string>
{
    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        //bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        //if (!verified)
        //{
        //    return Result.Failure<string>(UserErrors.NotFoundByEmail);
        //}

        string token = "token";
        //string token = tokenProvider.Create(user);

        return token;
    }
}
