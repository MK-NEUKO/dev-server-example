using Microsoft.AspNetCore.Mvc;
using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGatewayApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReversePoxy(IProxyConfigProvider context) : ControllerBase
{
    [HttpGet]
    [EndpointName("GetReverseProxy")]
    [Route("GetReverseProxy")]
    public IActionResult GetReverseProxy()
    {
        var reverseProxy = context.GetConfig();
        return Ok(reverseProxy);
    }
}