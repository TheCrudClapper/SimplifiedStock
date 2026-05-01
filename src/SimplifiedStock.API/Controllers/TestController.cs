using Microsoft.AspNetCore.Mvc;
using SimplifiedStock.Services.ServiceContracts;

namespace SimplifiedStock.API.Controllers
{
    [Route("test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestDatabaseCleaner _cleaner;

        public TestController(ITestDatabaseCleaner cleaner)
        {
            _cleaner = cleaner;
        }

        [HttpPost("reset")]
        public async Task<IActionResult> Reset()
        {
            await _cleaner.ResetAsync();
            return Ok();
        }
    }
}
