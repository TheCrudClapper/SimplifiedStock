using Microsoft.EntityFrameworkCore;
using SimplifiedStock.Infrastructure.Contexts;
using SimplifiedStock.Services.DTO.Stock;
using SimplifiedStock.Services.DTO.Wallet;
using SimplifiedStock.Services.ServiceContracts;

namespace SimplifiedStock.Services.Services;

public class WalletService : IWalletService
{
    private readonly StockDatabaseContext _context;
    public WalletService(StockDatabaseContext context)
        => _context = context;

    public async Task<WalletResponse?> GetWalletByIdAsync(Guid walletId, CancellationToken ct = default)
    {
        return await _context.Wallets
            .AsNoTracking()
            .Include(w => w.WalletStocks)
            .Where(w => w.Id == walletId)
            .Select(w => new WalletResponse()
            {
                Id = w.Id,
                Stocks = new StockResponse(w.WalletStocks.Select(ws => new StockDto(ws.Name, ws.Quantity)).ToList())
            })
            .FirstOrDefaultAsync(ct);
    }
    

    public async Task<int> GetWalletStockQuantityAsync(Guid walletId, string stockName, CancellationToken ct = default)
    {
        var quantity = await _context.WalletStocks
            .AsNoTracking()
            .Where(ws => ws.WalletId == walletId && ws.Name == stockName)
            .Select(ws => ws.Quantity)
            .FirstOrDefaultAsync(ct);

        return quantity;
    }
}
