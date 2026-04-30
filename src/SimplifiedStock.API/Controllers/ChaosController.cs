using Microsoft.AspNetCore.Mvc;

namespace SimplifiedStock.API.Controllers;

[Route("chaos")]
[ApiController]
public class ChaosController : ControllerBase
{
    [HttpPost]
    public IActionResult KillInstance()
    {
        Environment.Exit(0);
        return Ok();
    }
}
