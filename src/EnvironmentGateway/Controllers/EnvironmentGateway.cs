using Microsoft.AspNetCore.Mvc;

namespace EnvironmentGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class EnvironmentGatewayController(Models.EnvironmentGateway context) : ControllerBase
{
    [HttpGet]
    [EndpointName("GetContext")]
    [Route("GetContext")]
    public IActionResult GetContext()
    {
        return Ok(context);
    }
}