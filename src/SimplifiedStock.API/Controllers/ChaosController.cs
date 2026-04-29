using Microsoft.AspNetCore.Mvc;

namespace SimplifiedStock.API.Controllers;

[Route("chaos")]
[ApiController]
public class ChaosController : ControllerBase
{
    [HttpPost]
    public void KillInstance()
    {
        HttpContext.Abort();
    }
}
