using EnvironmentGateway.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentGateway.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<EnvironmentGatewayDbContext>();

        var connstr = dbContext.Database.GetDbConnection().ConnectionString;

        dbContext.Database.Migrate();
    }
}