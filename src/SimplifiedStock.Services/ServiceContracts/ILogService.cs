using SimplifiedStock.Services.DTO.Log;

namespace SimplifiedStock.Services.ServiceContracts;
public interface ILogService
{
    Task<IReadOnlyCollection<StockLogResponse>> GetAllStockLogsAsync(CancellationToken ct = default);
}
