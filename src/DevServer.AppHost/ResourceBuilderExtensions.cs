using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DevServer.AppHost;

internal static class ResourceBuilderExtensions
{
    internal static IResourceBuilder<T> WithScalar<T>(this IResourceBuilder<T> builder)
        where T : IResourceWithEndpoints
    {
        return builder.WithOpenApiDocs("scalar-ui-docs", "Scalar API Documentation", "scalar");
    }

    private static IResourceBuilder<T> WithOpenApiDocs<T>(
        this IResourceBuilder<T> builder,
        string name,
        string displayName,
        string openApiUiPath)
        where T : IResourceWithEndpoints
    {
        return builder.WithCommand(
            name,
            displayName,
            executeCommand: async _ =>
            {
                try
                {
                    var endpoint = builder.GetEndpoint("https");

                    var url = $"{endpoint.Url}/{openApiUiPath}";

                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });


                    return new ExecuteCommandResult { Success = true };
                }
                catch (Exception ex)
                {
                    return new ExecuteCommandResult { Success = false, ErrorMessage = ex.ToString() };
                }
            },
            updateState: context => context.ResourceSnapshot.HealthStatus == HealthStatus.Healthy ?
                ResourceCommandState.Enabled : ResourceCommandState.Disabled,
            iconName: "Document",
            iconVariant: IconVariant.Filled);
    }
}