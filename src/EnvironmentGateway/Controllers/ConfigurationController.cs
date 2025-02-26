using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly InMemoryConfigProvider? _configurationProvider;
        private readonly InMemoryConfig _inMemoryConfig;
        private readonly IConfiguration _apiContext;

        public ConfigurationController(
            IProxyConfigProvider configurationProvider,
            InMemoryConfig inMemoryConfig,
            IConfiguration apiContext)
        {
            _configurationProvider = configurationProvider as InMemoryConfigProvider;
            _inMemoryConfig = inMemoryConfig;
            _apiContext = apiContext;
        }

        [HttpGet]
        [EndpointName("GetInfo")]
        [Route("GetInfo")]
        public IActionResult GetInfo()
        {
            var test = _apiContext.GetSection("Services")
                .GetChildren()
                .ToDictionary(x=> x.Key, x=> x.Value);
            return Ok(test);
        }

        [HttpGet]
        [EndpointName("GetConfiguration")]
        [Route("GetConfiguration")]
        public IActionResult GetConfiguration()
        {
            if (_configurationProvider == null)
            {
                return BadRequest("Invalid configuration provider.");
            }

            var proxyConfig = _configurationProvider.GetConfig();
            var reverseProxyConfig = new
            {
                Routes = proxyConfig.Routes,
                Clusters = proxyConfig.Clusters
            };
            return Ok(reverseProxyConfig);
        }

        [HttpPost]
        [EndpointName("UpdateConfiguration")]
        [Route("UpdateConfiguration/{testurl}")]
        public IActionResult UpdateConfiguration(string testurl)
        {
            // Neuen Cluster erstellen oder vorhandenen überschreiben
            var newCluster = new ClusterConfig
            {
                ClusterId = "cluster1",
                Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
                {
                    { "destination1", new DestinationConfig { Address = $"https://{testurl}" } },
                }
            };

            // Neue Route erstellen oder vorhandene überschreiben
            var newRoute = new RouteConfig
            {
                RouteId = "route1",
                ClusterId = "cluster1",
                Match = new RouteMatch
                {
                    Path = "{**catch-all}"
                }
            };

            // Konfiguration aktualisieren
            _inMemoryConfig.Clusters = new List<ClusterConfig> { newCluster };
            _inMemoryConfig.Routes = new List<RouteConfig> { newRoute };

            _configurationProvider.Update(_inMemoryConfig.Routes, _inMemoryConfig.Clusters);

            return Ok(new { message = "Konfiguration erfolgreich aktualisiert" });
        }
    }
}
 