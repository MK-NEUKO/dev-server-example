using EnvironmentGateway.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EnvironmentGateway.Api.FunctionalTests.Infrastructure;

public abstract class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly HttpClient HttpClient;
    protected readonly EnvironmentGatewayDbContext DbContext;

    protected BaseFunctionalTest(FunctionalTestWebAppFactory factory)
    {
        HttpClient = factory.CreateClient();
        _scope = factory.Services.CreateScope();
        DbContext = _scope.ServiceProvider.GetRequiredService<EnvironmentGatewayDbContext>();
    }
}