using SimplifiedStock.Services.DTO.Stock;

namespace SimplifiedStock.Services.ServiceContracts;

public interface IStockService
{
    Task<StockResponse> GetAllBankStocksAsync(CancellationToken ct = default);
    Task AddBankStocksAsync(BankStockAddRequest request);
}
