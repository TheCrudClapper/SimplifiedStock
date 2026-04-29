using SimplifiedStock.Services.Entities.Enums;
using System.Reflection.Emit;

namespace SimplifiedStock.Services.Entities;

public class AuditLog
{
    public int Id { get; set; }
    public Guid WalletId {  get; set; }
    public OperationType Type { get; set; }
    public string StockName { get; set; } = null!;
}
