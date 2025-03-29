using EnvironmentGateway.Application.Abstractions.Data;
using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;

internal sealed class GetStartConfigQueryHandler : IQueryHandler<GetStartConfigQuery, StartConfigResponse>
{
    private readonly IEnvironmentGatewayDbContext _context;

    public GetStartConfigQueryHandler(
        IEnvironmentGatewayDbContext context)
    {
        _context = context;
    }

    public async Task<Result<StartConfigResponse>> Handle(
        GetStartConfigQuery request,
        CancellationToken cancellationToken)
    {
        var gatewayConfigSummery = await _context
            .Database
            .SqlQuery<GatewayConfigSummery>($"""
                                             SELECT 
                                                "Id",
                                                "IsCurrentConfig"
                                             FROM
                                                "GatewayConfigs"
                                             """)
            .ToListAsync();

        var response = new StartConfigResponse();
        return response;
    }

    private sealed record GatewayConfigSummery(
        Guid Id,
        bool IsCurrentConfig);
}