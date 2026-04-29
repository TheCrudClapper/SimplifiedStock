namespace SimplifiedStock.Services.Entities;

public class WalletStock
{
    public Guid WalletId { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
}
