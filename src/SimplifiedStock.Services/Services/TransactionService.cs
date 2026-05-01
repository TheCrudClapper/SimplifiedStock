using Microsoft.EntityFrameworkCore;
using SimplifiedStock.Domain.Entities;
using SimplifiedStock.Domain.Entities.Enums;
using SimplifiedStock.Infrastructure.Contexts;
using SimplifiedStock.Services.DTO.Transcation;
using SimplifiedStock.Services.Exceptions;
using SimplifiedStock.Services.Mappings;
using SimplifiedStock.Services.ServiceContracts;

namespace SimplifiedStock.Services.Services;

public class TransactionService : ITransactionService
{
    private readonly StockDatabaseContext _context;
    public TransactionService(StockDatabaseContext context)
    {
        _context = context;
    }

    public async Task BuyOrSellStock(Guid walletId, string stockName, TransactionRequest request)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        var normalizedStockName = stockName.Trim().ToUpperInvariant();
        var domainOperationType = request.Type.ToDomain();

        try
        {
            //Locks stock from concurent updates
            BankStock? stock = await _context.BankStocks
                .FromSqlRaw("SELECT * FROM \"BankStocks\" WHERE \"Name\" = {0} FOR UPDATE", normalizedStockName)
                .FirstOrDefaultAsync()
                ?? throw new NotFoundException("Stock not found");

            Wallet? wallet = await _context.Wallets
                   .FirstOrDefaultAsync(w => w.Id == walletId);

            WalletStock? walletStock = await _context.WalletStocks
                   .FirstOrDefaultAsync(ws => ws.WalletId == walletId && ws.Name == normalizedStockName);

            if (domainOperationType == OperationType.Sell)
            {
                if (wallet is null)
                    throw new BusinessException("Wallet not found");

                if (walletStock is null)
                    throw new BusinessException("Stock not found in wallet");

                if (walletStock.Quantity < 1)
                    throw new BusinessException("Insufficient stock quantity");

                walletStock.Quantity -= 1;

                if (walletStock.Quantity == 0)
                    _context.WalletStocks.Remove(walletStock);

                stock.Quantity += 1;
            }
            else if(domainOperationType == OperationType.Buy)
            {
                if (stock.Quantity < 1)
                    throw new BusinessException("Insufficient stock in bank");

                if (wallet is null)
                {
                    wallet = new Wallet() { Id = walletId };
                    await _context.Wallets.AddAsync(wallet);
                }

                stock.Quantity -= 1;

                if (walletStock is null)
                {
                    await _context.WalletStocks.AddAsync(new WalletStock()
                    {
                        Name = normalizedStockName,
                        Quantity = 1,
                        WalletId = walletId
                    });
                }
                else
                {
                    walletStock.Quantity += 1;
                }
            }
            else
            {
                throw new BusinessException("Invalid operation type");
            }

            AuditLog auditLog = new AuditLog()
            {
                StockName = normalizedStockName,
                Type = domainOperationType,
                WalletId = walletId,
                CreatedAt = DateTime.UtcNow,
            };

            await _context.AuditLogs.AddAsync(auditLog);
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
