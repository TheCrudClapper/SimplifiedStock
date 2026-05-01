using SimplifiedStock.Services.DTO.Wallet;

namespace SimplifiedStock.Services.ServiceContracts;

/// <summary>
/// Provides operations for managing and retrieving wallet information.
/// </summary>
public interface IWalletService
{
    /// <summary>
    /// Retrieves a wallet by its unique identifier.
    /// </summary>
    /// <param name="walletId">The unique identifier of the wallet.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The wallet response if found; otherwise, null.</returns>
    Task<WalletResponse?> GetWalletByIdAsync(Guid walletId, CancellationToken ct = default);

    /// <summary>
    /// Gets the quantity of a specific stock in the wallet.
    /// </summary>
    /// <param name="walletId">The unique identifier of the wallet.</param>
    /// <param name="stockName">The name of the stock.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The quantity of the specified stock.</returns>
    Task<int> GetWalletStockQuantityAsync(Guid walletId, string stockName, CancellationToken ct = default);
}
