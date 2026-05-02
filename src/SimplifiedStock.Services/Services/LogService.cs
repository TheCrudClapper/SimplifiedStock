using Microsoft.EntityFrameworkCore;
using SimplifiedStock.Infrastructure.Contexts;
using SimplifiedStock.Services.DTO.AuditLog;
using SimplifiedStock.Services.Mappings;
using SimplifiedStock.Services.ServiceContracts;

namespace SimplifiedStock.Services.Services;

public class LogService : ILogService
{
    private readonly StockDatabaseContext _context;
    public LogService(StockDatabaseContext context)
       => _context = context;

    public async Task<LogResponse> GetAllStockLogsAsync(CancellationToken ct = default)
    {
        var logs = await _context.AuditLogs
            .OrderBy(l => l.CreatedAt)
            .Select(l => new StockLogDto()
            {
                StockName = l.StockName,
                Type = l.Type.ToDto(),
                WalletId = l.WalletId,
            })
            .ToListAsync(ct);

        return new LogResponse { Log = logs };
    }
}
