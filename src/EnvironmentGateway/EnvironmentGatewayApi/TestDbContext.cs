using Microsoft.EntityFrameworkCore;

namespace EnvironmentGatewayApi;

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    }
}