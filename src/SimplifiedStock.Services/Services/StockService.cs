using SimplifiedStock.Services.DTO.Stock;
using SimplifiedStock.Services.ServiceContracts;

namespace SimplifiedStock.Services.Services;

public class StockService : IStockService
{
    public Task<IReadOnlyCollection<StockResponse>> GetAllBankStocksAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task PostBankStocksAsync(IEnumerable<StockAddRequest> request)
    {
        throw new NotImplementedException();
    }
}
