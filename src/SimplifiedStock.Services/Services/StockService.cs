using Microsoft.EntityFrameworkCore;
using SimplifiedStock.Domain.Entities;
using SimplifiedStock.Infrastructure.Contexts;
using SimplifiedStock.Services.DTO.Stock;
using SimplifiedStock.Services.Exceptions;
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
            .Select(bs => new StockDto() { Name = bs.Name, Quantity = bs.Quantity })
            .ToListAsync(ct);

        return new StockResponse() { Stocks = stocks };
    }

    public async Task AddBankStocksAsync(BankStockAddRequest request)
    {
        foreach(var stockRequest in request.Stocks)
        {
            if (stockRequest.Quantity < 0)
                throw new BusinessException($"Invalid quantity for stock '{stockRequest.Name}'");

            if (string.IsNullOrWhiteSpace(stockRequest.Name))
                throw new BusinessException($"Invalid name for stock {stockRequest.Name}");
        }

        var grouped = request.Stocks
            .GroupBy(bs => bs.Name.Trim().ToUpperInvariant())
            .Select(g => new BankStock 
            { 
                Name = g.Key,
                Quantity = g.Sum(x => x.Quantity) 
            });

        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            await _context.BankStocks.ExecuteDeleteAsync();
            _context.BankStocks.AddRange(grouped);
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
