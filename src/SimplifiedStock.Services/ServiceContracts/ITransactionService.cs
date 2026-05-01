using SimplifiedStock.Services.DTO.Transcation;

namespace SimplifiedStock.Services.ServiceContracts;

/// <summary>
/// Provides operations for buying and selling stocks within a wallet.
/// </summary>
public interface ITransactionService
{
    /// <summary>
    /// Executes a buy or sell transaction for a specific stock in the given wallet.
    /// </summary>
    /// <param name="walletId">The unique identifier of the wallet.</param>
    /// <param name="stockName">The name of the stock to buy or sell.</param>
    /// <param name="request">The transaction request details.</param>
    Task BuyOrSellStock(Guid walletId, string stockName, TransactionRequest request);
}
