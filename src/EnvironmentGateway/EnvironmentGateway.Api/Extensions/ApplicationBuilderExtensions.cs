using EnvironmentGateway.Api.Middleware;
using EnvironmentGateway.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace EnvironmentGateway.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseScalarWithUi(this WebApplication app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
        
        return app;
    }
}
