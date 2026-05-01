using SimplifiedStock.Services.DTO.AuditLog;

namespace SimplifiedStock.Services.ServiceContracts;

/// <summary>
/// Provides operations for retrieving audit logs related to stocks.
/// </summary>
public interface ILogService
{
    /// <summary>
    /// Retrieves all stock-related audit logs.
    /// </summary>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A response containing all stock logs.</returns>
    Task<LogResponse> GetAllStockLogsAsync(CancellationToken ct = default);
}
