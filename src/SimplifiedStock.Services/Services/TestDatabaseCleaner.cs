using Microsoft.EntityFrameworkCore;
using SimplifiedStock.Infrastructure.Contexts;
using SimplifiedStock.Services.ServiceContracts;

public class TestDatabaseCleaner : ITestDatabaseCleaner
{
    private readonly StockDatabaseContext _context;

    public TestDatabaseCleaner(StockDatabaseContext context)
    {
        _context = context;
    }

    public async Task ResetAsync()
    {
        await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE \"AuditLogs\" CASCADE");
        await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE \"WalletStocks\" CASCADE");
        await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE \"Wallets\" CASCADE");
        await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE \"BankStocks\" CASCADE");
    }
}