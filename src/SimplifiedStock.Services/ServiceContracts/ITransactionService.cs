using SimplifiedStock.Services.DTO.Transcation;

namespace SimplifiedStock.Services.ServiceContracts;

public interface ITransactionService
{
    Task BuyOrSellStock(Guid walletId, string stockName, TransactionRequest request);
}
