using SimplifiedStock.Services.DTO.Wallet;

namespace SimplifiedStock.Services.ServiceContracts;
public interface IWalletService
{
    Task<WalletResponse> GetWalletByIdAsync(Guid walletId, CancellationToken ct = default);
    Task<int> GetWalletStockQuantityAsync(Guid walletId, string stockName, CancellationToken ct = default);
}
