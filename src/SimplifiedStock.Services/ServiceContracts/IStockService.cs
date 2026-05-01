using SimplifiedStock.Services.DTO.Stock;

namespace SimplifiedStock.Services.ServiceContracts;

/// <summary>
/// Provides operations for managing and retrieving stock information.
/// </summary>
public interface IStockService
{
    /// <summary>
    /// Retrieves all bank stocks.
    /// </summary>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A response containing all bank stocks.</returns>
    Task<StockResponse> GetAllBankStocksAsync(CancellationToken ct = default);

    /// <summary>
    /// Adds new bank stocks.
    /// </summary>
    /// <param name="request">The request containing bank stock details to add.</param>
    Task AddBankStocksAsync(BankStockAddRequest request);
}
