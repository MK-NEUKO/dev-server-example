using EnvironmentGateway.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EnvironmentGateway.Api.FunctionalTests.Infrastructure;

public abstract class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>
{
    protected readonly HttpClient HttpClient;
    protected readonly EnvironmentGatewayDbContext DbContext;

    protected BaseFunctionalTest(FunctionalTestWebAppFactory factory)
    {
        HttpClient = factory.CreateClient();
        IServiceScope scope = factory.Services.CreateScope();
        DbContext = scope.ServiceProvider.GetRequiredService<EnvironmentGatewayDbContext>();
    }
}
