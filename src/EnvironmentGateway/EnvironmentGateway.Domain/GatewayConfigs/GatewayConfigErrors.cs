using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.GatewayConfigs;

public static class GatewayConfigErrors
{
    public static readonly Error CreateNewConfigFailed = new(
        "CreateNewConfigFailed", 
        "Failed to create new configuration.");
    
    public static readonly Error CurrentConfigNotFound = new(
        "CurrentConfigNotFound", 
        "No valid gateway configuration found in the database. Either no gatewayConfig exists, or none is marked as isCurrentConfig = true.");


}
