using EnvironmentGateway.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EnvironmentGateway.Application.IntegrationTests.Infrastructure;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly EnvironmentGatewayDbContext DbContext;
    protected readonly IntegrationTestWebAppFactory Factory;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        Factory = factory;
        _scope = factory.Services.CreateScope();

        DbContext = _scope.ServiceProvider.GetRequiredService<EnvironmentGatewayDbContext>();
    }

    public async Task InitializeTestConfigAsync()
    {
        await Factory.InitializeTestConfigAsync();
    }
}