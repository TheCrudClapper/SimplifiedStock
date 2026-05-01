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
        _context.AuditLogs.RemoveRange(_context.AuditLogs);
        _context.WalletStocks.RemoveRange(_context.WalletStocks);
        _context.Wallets.RemoveRange(_context.Wallets);
        _context.BankStocks.RemoveRange(_context.BankStocks);
        await _context.SaveChangesAsync();
    }
}