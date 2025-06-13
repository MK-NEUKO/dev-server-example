using EnvironmentGateway.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EnvironmentGateway.Application.IntegrationTests.Infrastructure;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    protected readonly IServiceScope Scope;
    protected readonly EnvironmentGatewayDbContext DbContext;
    protected readonly IntegrationTestWebAppFactory Factory;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        Factory = factory;
        Scope = factory.Services.CreateScope();

        DbContext = Scope.ServiceProvider.GetRequiredService<EnvironmentGatewayDbContext>();
    }

    public async Task InitializeTestConfigAsync()
    {
        await Factory.InitializeTestConfigAsync();
    }
}