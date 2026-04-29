namespace SimplifiedStock.Domain.Entities;

public class WalletStock
{
    public Guid Id { get; set; }
    public Guid WalletId { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public Wallet Wallet { get; set; } = null!;
}
