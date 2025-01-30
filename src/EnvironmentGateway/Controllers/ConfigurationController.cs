using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EnvironmentGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConfigurationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetConfiguration()
        {
            var reverseProxyConfig = _configuration.GetSection("ReverseProxy").GetChildren();
            return Ok(reverseProxyConfig);
        }
    }
}
