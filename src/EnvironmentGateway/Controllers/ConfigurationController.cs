using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController(IProxyConfigProvider configurationProvider) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetConfiguration()
        {
            var proxyConfig = configurationProvider.GetConfig();
            var reverseProxyConfig = new
            {
                Routes = proxyConfig.Routes,
                Clusters = proxyConfig.Clusters
            };
            return Ok(reverseProxyConfig);
        }
    }
}
