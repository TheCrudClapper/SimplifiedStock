namespace SimplifiedStock.Domain.Entities;

public class Wallet
{
    public Guid Id { get; set; }
    public ICollection<WalletStock> WalletStocks { get; set; } = [];
}
