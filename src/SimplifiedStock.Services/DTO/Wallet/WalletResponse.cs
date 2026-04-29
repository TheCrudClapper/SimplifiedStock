using SimplifiedStock.Services.DTO.Stock;

namespace SimplifiedStock.Services.DTO.Wallet;

public record WalletResponse(Guid id, IReadOnlyCollection<StockResponse> stocks);