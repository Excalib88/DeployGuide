using Microsoft.AspNetCore.Mvc;

namespace DeployGuide.Controllers;

[ApiController]
[Route("test")]
public class TestController: ControllerBase
{
    [HttpGet]
    public IActionResult Test()
    {
        return Ok("Test: Ok");
    }
}