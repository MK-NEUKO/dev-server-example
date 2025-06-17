using Microsoft.EntityFrameworkCore;
using UserManager.Domain.Users;

namespace UserManager.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
