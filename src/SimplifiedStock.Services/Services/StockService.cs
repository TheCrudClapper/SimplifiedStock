using Microsoft.EntityFrameworkCore;
using SimplifiedStock.Domain.Entities;
using SimplifiedStock.Infrastructure.Contexts;
using SimplifiedStock.Services.DTO.Stock;
using SimplifiedStock.Services.ServiceContracts;

namespace SimplifiedStock.Services.Services;

public class StockService : IStockService
{
    private readonly StockDatabaseContext _context;
    public StockService(StockDatabaseContext context)
        => _context = context;

    public async Task<StockResponse> GetAllBankStocksAsync(CancellationToken ct = default)
    {
        var stocks = await _context.BankStocks
            .AsNoTracking()
            .Select(bs => new StockDto(bs.Name, bs.Quantity))
            .ToListAsync(ct);

        return new StockResponse(stocks);
    }

    public async Task AddBankStocksAsync(BankStockAddRequest request)
    {
        List<BankStock> bankStocks = [];

        foreach (var stock in request.Stocks)
        {
            var bankStock = new BankStock
            {
                Name = stock.name,
                Quantity = stock.quantity,
            };

            bankStocks.Add(bankStock);
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            await _context.BankStocks.ExecuteDeleteAsync();
            _context.BankStocks.AddRange(bankStocks);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
