using EnvironmentGateway.Application.Abstractions.Data;
using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;

internal sealed class GetStartConfigQueryHandler : IQueryHandler<GetStartConfigQuery, StartConfigResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IEnvironmentGatewayDbContext _context;

    public GetStartConfigQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory,
        IEnvironmentGatewayDbContext context)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _context = context;
    }

    public async Task<Result<StartConfigResponse>> Handle(
        GetStartConfigQuery request,
        CancellationToken cancellationToken)
    {
        var response = new StartConfigResponse();

        return response;
    }
}