using Microsoft.EntityFrameworkCore;
using SimplifiedStock.Infrastructure.Contexts;
using SimplifiedStock.Services.DTO.Log;
using SimplifiedStock.Services.ServiceContracts;

namespace SimplifiedStock.Services.Services;

public class LogService : ILogService
{
    private readonly StockDatabaseContext _context;
    public LogService(StockDatabaseContext context)
       => _context = context;

    public async Task<IReadOnlyCollection<StockLogResponse>> GetAllStockLogsAsync(CancellationToken ct = default)
        => await _context.AuditLogs
            .Select(l => new StockLogResponse(l.Type, l.WalletId, l.StockName))
            .ToListAsync(ct);

}
