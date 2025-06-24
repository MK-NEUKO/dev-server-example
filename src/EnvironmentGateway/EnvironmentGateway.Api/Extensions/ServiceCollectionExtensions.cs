using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace EnvironmentGateway.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddOpenApiWithSecuritySchemeTransformer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOpenApi(o =>
        {
            o.AddDocumentTransformer<OpenApiSecuritySchemeTransformer>();
        });

        return services;
    }
}
