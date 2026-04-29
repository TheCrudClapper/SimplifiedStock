using SimplifiedStock.Services.DTO.Wallet;
using SimplifiedStock.Services.ServiceContracts;

namespace SimplifiedStock.Services.Services;

public class WalletService : IWalletService
{
    public Task<WalletResponse> GetWalletByIdAsync(Guid walletId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetWalletStockQuantityAsync(Guid walletId, string stockName, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
