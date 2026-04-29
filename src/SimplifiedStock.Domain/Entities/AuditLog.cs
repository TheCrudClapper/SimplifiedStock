using SimplifiedStock.Domain.Entities.Enums;

namespace SimplifiedStock.Domain.Entities;

public class AuditLog
{
    public int Id { get; set; }
    public Guid WalletId {  get; set; }
    public OperationType Type { get; set; }
    public string StockName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
