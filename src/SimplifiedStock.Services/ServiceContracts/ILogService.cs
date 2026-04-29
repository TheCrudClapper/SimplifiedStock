using SimplifiedStock.Services.DTO.AuditLog;

namespace SimplifiedStock.Services.ServiceContracts;
public interface ILogService
{
    Task<LogResponse> GetAllStockLogsAsync(CancellationToken ct = default);
}
