using SimplifiedStock.Services.DTO.Stock;

namespace SimplifiedStock.Services.ServiceContracts;

public interface IStockService
{
    Task<IReadOnlyCollection<StockResponse>> GetAllBankStocksAsync(CancellationToken ct = default);
    Task PostBankStocksAsync(IEnumerable<StockAddRequest> request);
}
