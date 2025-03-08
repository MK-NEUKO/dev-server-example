using System.Diagnostics;
using System.Reflection;

namespace EnvironmentGatewayApi.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddEndpoints(Assembly.GetExecutingAssembly());

        services.AddOpenApi();

        return services;
    }
}