using EnvironmentGatewayApi.GatewayConfiguration.Abstractions;
using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGatewayApi.GatewayConfiguration;

public class RuntimeConfigurator(
    IProxyConfigProvider configurationProvider) 
    : IRuntimeConfigurator
{
    public void ChangeDestinationAddress(string address)
    {
        var config = configurationProvider.GetConfig();
        
    }
}