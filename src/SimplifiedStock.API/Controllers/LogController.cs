using Microsoft.AspNetCore.Mvc;
using SimplifiedStock.Services.DTO.AuditLog;
using SimplifiedStock.Services.ServiceContracts;

namespace SimplifiedStock.API.Controllers;

[Route("log")]
[ApiController]
public class LogController : ControllerBase
{
    private readonly ILogService _logService;
    public LogController(ILogService logService)
      => _logService = logService;

    [HttpGet]
    public async Task<ActionResult<StockLogDto>> GetAllLogs(CancellationToken ct)
    {
        var logs = await _logService.GetAllStockLogsAsync(ct);
        return Ok(logs);
    }
}
